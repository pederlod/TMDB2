using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TMDB2.Models;
using TMDB2.Data;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

public class UserController : Controller
{
    private readonly MyDbContext _context;

    public UserController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult CreateUser()
    {

        Console.WriteLine("CreateUser() was called without parameters");

        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        Console.WriteLine("Attempting to create user.  USER ID: Auto Incremented, USER NAME: " + user.UserName + ", USER PASSWORD: " + user.Password);

        // Generate salt, and hash password

        byte[] salt = GenerateSalt();

        user.Salt = salt;

        user.Password = HashPasswordWithSalt(user.Password, salt);



        // Clear ModelState errors related to the salt if any were added during binding
        ModelState.Clear();

        // Manually validate the model again after setting the salt and hashed password
        TryValidateModel(user);

        Console.WriteLine("Is ModelStateUpdated?.  USER ID: Auto Incremented, USER NAME: " + user.UserName + ", USER PASSWORD: " + user.Password + ", USER SALT: " + user.Salt);

        if (ModelState.IsValid)
        {
            Console.WriteLine("USER ModelState IS VALID");

            try
            {
                // Add the new user to the database context
                _context.Users.Add(user);

                // Save changes to the database
                _context.SaveChanges();

                Console.WriteLine("USER ID: " + user.Iduser + ", USER NAME: " + user.UserName + "USER PASSWORD: " + user.Password);

                // Return to the Login action
                return RedirectToAction("Login");
            }
            catch (MySqlException ex) // Catch MySQL-specific exceptions
            {
                // Check if the exception is a duplicate entry error
                if (ex.Number == 1062) // MySQL error code for duplicate entry
                {
                    ModelState.AddModelError("UserName", "This username is already taken. Please choose another one.");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while creating the user. Please try again.");
                }
            }
            catch (Exception ex) // Catch other potential errors
            {
                ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("USER ModelState IS INVALID");

            // Log validation errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                }
            }
        }

        return View("CreateUser"); // Return to the same view with validation errors
    }

    public IActionResult Login()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Login(User user)
    {
        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
        {
            // If either username or password is empty, display an error message
            ModelState.AddModelError(string.Empty, "Fill both fields to log in....");
            return View(user);
        }

        // Retrieve the user from the database by username
        var dbUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

        if (dbUser == null)
        {
            // If the username does not exist, display an error message
            ModelState.AddModelError(string.Empty, "Username not found.");
            return View(user);
        }

        // Hash the input password with the salt retrieved from the database
        string hashedInputPassword = HashPasswordWithSalt(user.Password, dbUser.Salt);

        if (dbUser.Password != hashedInputPassword)
        {
            // If the password does not match, display an error message
            ModelState.AddModelError(string.Empty, "Incorrect password");
            return View(user);
        }

        try
        {
            // Create a list of claims that includes the User ID and UserName
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Iduser.ToString()), // Add User ID as a claim
            new Claim(ClaimTypes.Name, dbUser.UserName)
        };

            // Create a ClaimsIdentity with the list of claims
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Set authentication properties (e.g., whether the authentication is persistent)
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            // Sign in the user by creating a cookie with the claims
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirect to the "MyLists" action
            return RedirectToAction("MyLists");
        }
        catch (Exception)
        {
            // If an unexpected error occurs, display a generic error message
            ModelState.AddModelError(string.Empty, "Something went wrong. Please try again.");
            return View(user);
        }
    }




    //Testing user creation
    public IActionResult CreateTestUser()
    {
        // Create a new User object with the provided values

        byte[] salt = GenerateSalt();

        string unhashedPassword = "Passord";

        var user = new User
        {
            UserName = "DeleteLater",

            Salt = salt,

            Password = HashPasswordWithSalt(unhashedPassword, salt)

        };
        Console.WriteLine("USER ID: " + "auto incremented" + ", USER NAME: " + user.UserName + ", USER PASSWORD(Hashed): " + user.Password + ", HASH: " + user.Salt);
        // Add the user to the DbSet and save changes
        _context.Users.Add(user);
        _context.SaveChanges();  // This will generate the appropriate SQL and execute it against the database

        Console.WriteLine("USER ID: " + user.Iduser + ", USER NAME: " + user.UserName + "USER PASSWORD(Hashed): " + user.Password + ", HASH: " + user.Salt);
        //
        return Content("User created successfully");
    }

    public IActionResult MyLists()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }

        // Load and return user's lists
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }


    public static byte[] GenerateSalt()
    {

        int saltLength = 16; // 16 bytes = 128 bits

        byte[] salt = new byte[saltLength];

        // Use RandomNumberGenerator to fill the array with cryptographically secure random bytes
        RandomNumberGenerator.Fill(salt);

        return salt;
    }


public static string HashPasswordWithSalt(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            // Combine the password and salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);

            // Compute the hash
            byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);

            // Return the hash as a base64 string
            return Convert.ToBase64String(hashBytes);
        }
    }

}