using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PSE.Core.Responses;
using System.Collections.Generic;
using System.Linq;

namespace PSE.WebAPI.Core.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in erros)
        {
            AddErrorInProcess(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddErrorInProcess(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ResponseErrorResult response)
    {
        ResponseHasErrors(response);

        return CustomResponse();
    }

    protected bool ResponseHasErrors(ResponseErrorResult response)
    {
        if (response == null || !response.Errors.Messages.Any()) return false;

        foreach (var mensagem in response.Errors.Messages)
        {
            AddErrorInProcess(mensagem);
        }

        return true;
    }

    protected bool ValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddErrorInProcess(string error)
    {
        Errors.Add(error);
    }

    protected void ClearErrorsProcess()
    {
        Errors.Clear();
    }
}
