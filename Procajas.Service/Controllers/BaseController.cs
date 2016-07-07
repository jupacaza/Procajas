using Procajas.Service.Settings;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    [Authorize]
    public abstract class BaseController : ApiController
    {
        protected ServiceSettings serviceSettings;

        protected BaseController()
        {
            serviceSettings = ServiceSettings.Instance;
        }
    }
}