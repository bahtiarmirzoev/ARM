using System.Reflection;
using ARM.Common.Exceptions;
using ARM.Core.Dtos.Read;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ARM.Presentation.Controllers;

[ApiController]
[Route("api")]

public class ExecuteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExecuteController(IMediator mediator) => _mediator = mediator;
    

    [HttpPost("b")]
    public async Task<IActionResult> Execute([FromBody] BatchRequestDto request)
    {
        var assembly = Assembly.Load("ARM.RequestPipeline");
        var results = new List<object>();
        var successCount = 0;
        var failureCount = 0;

        foreach (var item in request.Requests)
        {
            var commandType = assembly.GetTypes().FirstOrDefault(t => t.Name == item.Action);
            if (commandType is null)
            {
                results.Add(new
                {
                    success = false,
                    Error = $"anf"
                });
                failureCount++;
                continue;
            }

            var commandJson = item.Parameters.ToString();
            var command = JsonConvert.DeserializeObject(commandJson, commandType);

            if (command is null)
            {
                results.Add(new
                {
                    success = false,
                    Error = "Invalid parameters format"
                });
                failureCount++;
                continue;
            }

            try
            {
                var result = await _mediator.Send(command);
                results.Add(new
                {
                    success = true,
                    Result = result
                });
                successCount++;
            }
            catch (AppException ex)
            {
                results.Add(new
                {
                    success = false,
                    Error = ex.Message,
                    Code = ex.ExceptionType
                });
                failureCount++;
            }
            catch (Exception ex)
            {
                results.Add(new
                {
                    success = false,
                    Error = ex.Message
                });
                failureCount++;
            }
        }

        if (successCount == 0)
            return BadRequest(results);

        if (failureCount > 0)
            return StatusCode(StatusCodes.Status207MultiStatus, results);

        return Ok(results);
    }

}