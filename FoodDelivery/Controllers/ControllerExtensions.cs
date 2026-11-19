using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    public static class ControllerExtensions
    {
        public static ActionResult ToActionResult(this ControllerBase controller, Reply reply)
        {
            if (reply.IsFailure) return controller.ToProblemDetails(reply.Error);
            return controller.NoContent();
        }

        public static ActionResult ToActionResult<T>(this ControllerBase controller, Reply<T> reply)
        {
            if (reply.IsFailure) return controller.ToProblemDetails(reply.Error);
            return controller.Ok(reply.Value);
        }

        public static ActionResult<T> ToCreatedAtActionResult<T>(this ControllerBase controller, Reply<T> reply, string actionName, object? routeValues)
        {
            if (reply.IsFailure) return controller.ToProblemDetails(reply.Error);

            return controller.CreatedAtAction(
                actionName: actionName,
                controllerName: null,
                routeValues: routeValues,
                value: reply.Value);
        }

        private static ObjectResult ToProblemDetails(this ControllerBase controller, ErrorCore error)
        {
            int statusCode;

            switch (error.Type)
            {
                case ErrorType.NotFound:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case ErrorType.Validation:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case ErrorType.Conflict:
                    statusCode = StatusCodes.Status409Conflict;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return controller.Problem(
                statusCode: statusCode,
                detail: error.Message
                );
        }
    }
}
