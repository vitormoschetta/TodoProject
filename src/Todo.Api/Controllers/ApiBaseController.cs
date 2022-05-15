using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands.Responses;
using Todo.Domain.Enums;

namespace Todo.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected ApiBaseController()
        {
        }

        [NonAction]
        protected ActionResult CustomResponse(CommandResponse commandResponse)
        {
            return commandResponse.OutputType switch
            {
                EOutputType.Success => Ok(commandResponse),
                EOutputType.InvalidInput => BadRequest(commandResponse),
                EOutputType.BusinessValidation => BadRequest(commandResponse),
                EOutputType.NotFound => NotFound(commandResponse),                
                _ => StatusCode(500, commandResponse.Message),
            };
        }
    }
}