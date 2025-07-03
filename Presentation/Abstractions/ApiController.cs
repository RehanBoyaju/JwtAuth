using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    public abstract class ApiController:ControllerBase
    {
        protected readonly ISender Sender;
        protected ApiController(ISender sender) { 
            Sender = sender;
        }

        protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status
            };

            //TODO: Address this problemDetails
            //if (errors is not null)
            //    problemDetails.Extensions[nameof(errors)] = errors;

            return problemDetails;
        }
     }
}
