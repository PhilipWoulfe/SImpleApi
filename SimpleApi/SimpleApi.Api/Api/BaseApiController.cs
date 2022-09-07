using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
