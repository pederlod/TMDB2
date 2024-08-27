using System.Threading.Tasks;

namespace TMDB2.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderViewComponentToStringAsync(string componentName, object arguments = null);
    }
}
