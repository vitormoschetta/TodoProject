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
                EOutputType.IntegrationError => BadRequest(commandResponse),
                _ => StatusCode(500, commandResponse.Message),
            };
        }

        private object MapTo(CommandResponse commandResponse)
        {
            return new
            {
                success = commandResponse.Success,
                data = commandResponse.Data,
                message = commandResponse.Message
            };
        }     
    }
}