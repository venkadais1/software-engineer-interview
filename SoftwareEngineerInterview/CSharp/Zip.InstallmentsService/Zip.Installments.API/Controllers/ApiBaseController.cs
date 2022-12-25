using Microsoft.AspNetCore.Mvc;

namespace Zip.Installments.API.Controllers
{
    /// <summary>
    ///     The api base controller to setup api's
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]//Routting with api versioning
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
    }
}
