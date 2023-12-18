using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Utility;
using System.Net;

namespace ProjectManagementSystem.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult Results(Result result)
        {
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode 
            };
        }
    }
}
