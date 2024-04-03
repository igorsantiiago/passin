using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Application.DTOs.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = context.Exception is PassInException;
        if (result)
        {
            HandleProjectException(context);
        } else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if(context.Exception is NotFoundException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if(context.Exception is ValidationErrorException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if (context.Exception is ConflictException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Result = new ConflictObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if (context.Exception is ForbiddenException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Result = new ObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(context.Exception.Message));
    }
}
