Project Overview
TMDB2 is a Movie Database project created using ASP.NET Core and C#. It also connects to a MySQL database instance for managing user information and favorites.

Installation guide below. If you want a more readable one, use [this](https://docs.google.com/document/d/1qmZWIEUON13g7kcZD8L7IIXY-Hbt6ZvKBWTeay2oL5c/edit?usp=sharing)

Table of Contents

Project Overview

Prerequisites

Clone the Repository

Set Up MySQL Database

Configure Visual Studio Project

Running the Project

FAQ



Prerequisites
Ensure you have the following software installed on your system:

Microsoft Visual Studio Community 2022

[Download: Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)

MySQL Community Server and Workbench

[MySQL Server: Download MySQL Community Server](https://dev.mysql.com/downloads/mysql/)

[MySQL Workbench: Download MySQL Workbench](https://dev.mysql.com/downloads/workbench/)

.NET Core SDK

[Install from Download .NET](https://dotnet.microsoft.com/download)

Clone the Repository
Open your terminal (Command Prompt, PowerShell, Git Bash, etc.) and clone the repository:
bash
Copy code
git clone https://github.com/YourUsername/TMDB2.git


Navigate into the project directory:
bash
Copy code
cd TMDB2



Set Up MySQL Database
Install MySQL Server if it's not already installed, using the installer from MySQL's official site.
Open MySQL Workbench and create a new schema for the project:
sql
Copy code
CREATE DATABASE tmdb_users;


Import the Dump File:
Open MySQL Workbench.
Select your tmdb_users schema from the left sidebar.
Click on Server > Data Import.
Select Import from Self-Contained File, and browse for the Dump20240827.sql file located in your project directory.
Choose the tmdb_users schema as the target schema.
Click Start Import.
Once imported, you should see the tables populated in the tmdb_users schema.

Configure Visual Studio Project
Open the Project:
Open Visual Studio Community 2022.
Go to File > Open > Project/Solution.
Select the TMDB2.sln file from the cloned project directory.
Configure appsettings.json: In appsettings.json or appsettings.Development.json, update the connection string to match your MySQL database configuration. Look for the following block in the file:
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=tmdb_users;User=root;Password=YourPassword;"
}
Replace YourPassword with the actual MySQL password for the root user.
Restore NuGet Packages: In Visual Studio, right-click on the Solution in the Solution Explorer and select Restore NuGet Packages to ensure all required dependencies are installed.
Build the Project:
Go to Build > Build Solution (or press Ctrl + Shift + B) to build the project and resolve any dependencies.

Running the Project
Run MySQL Server: Ensure your MySQL server is running on localhost (port 3306).
Run the Project:
In Visual Studio, click the Run button (green triangle) or press F5.
This should start the project, and your browser will automatically open to the application.

FAQ
Why is my MySQL connection failing?
Ensure that MySQL Server is running and that the connection string in appsettings.json matches your MySQL credentials.
How do I reset my MySQL database?
You can drop and recreate the tmdb_users schema or re-import the Dump20240827.sql file following the instructions under Set Up MySQL Database.

