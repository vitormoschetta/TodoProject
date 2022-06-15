using Microsoft.AspNetCore.Mvc;
using Todo.Application.Commands.Responses;
using Todo.Application.Enums;

namespace Todo.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected ApiBaseController()
        {
        }

        [NonAction]
        protected ActionResult StatusCodeResponse(GenericResponse genericResponse)
        {
            return genericResponse.StatusResponse switch
            {
                EStatusResponse.Ok => Ok(genericResponse),
                EStatusResponse.BadRequest => BadRequest(genericResponse),
                EStatusResponse.NotFound => NotFound(genericResponse),
                EStatusResponse.InternalServerError => StatusCode(500, genericResponse),
                _ => StatusCode(500, genericResponse)
            };
        }
    }
}