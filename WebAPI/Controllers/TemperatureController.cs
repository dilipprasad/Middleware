using Microsoft.AspNetCore.Mvc;
using Middleware.Business.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Middleware.API.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag("Handles Operations realted to Temperature conversion")]
public class TemperatureController : ControllerBase
{

#region Private Members
    private readonly ILogger<TemperatureController> logger;
    private readonly ITemperatureProvider temperatureProvider;
#endregion

    public TemperatureController(ILogger<TemperatureController> logger, ITemperatureProvider temperatureProvider)
    {
        this.logger = logger;
        this.temperatureProvider = temperatureProvider;
    }


    /// <summary>
    /// Converts Temperature from Farenheit to Celsius
    /// </summary>
    /// <param name="temperature">Temperate in Farenheit</param>
    /// <returns></returns>
    [HttpGet(Name = "ConvertTemperature/{temperature}")]
    [SwaggerOperation(Description = "Converts Temperature from Farenheit to Celsius")]
    [SwaggerResponse(200, "Successful operation", typeof(string))]
    [SwaggerResponse(400, "Bad request", typeof(string))]
    [SwaggerResponse(500, "Internal server error", typeof(string))]
    public async Task<ActionResult> ConvertTemperature(double temperature)
    {
        //Validation for temperature if it is in Farenheit
        if (!(await IsValidFahrenheit(temperature)))
        {
            return BadRequest("Temperature is not in Farenheit");
        }
        try
        {
            var result = this.temperatureProvider.ConvertTemperature(temperature);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return Problem("There was a problem converting the temperature");
        }

    }

#region Private Methods
    private async Task<bool> IsValidFahrenheit(double temperature)
    {
        // Set your lower and upper limits for Fahrenheit temperature
        double lowerLimitFahrenheit = -459.67; // Absolute zero in Fahrenheit
        double upperLimitFahrenheit = 1000.0;  // Set your upper limit as needed

        return await Task.Run(() => lowerLimitFahrenheit <= temperature && temperature <= upperLimitFahrenheit);
    }
#endregion
}
