using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Middleware.Business.Interfaces;

namespace Middleware.Business
{
    /// <summary>
    /// Temperature Provider - Handles all the business logic
    /// </summary>
    public class TemperatureProvider : ITemperatureProvider
    {
        #region Private Members
        private readonly ILogger<TemperatureProvider> loggingProvider;
        #endregion

        public TemperatureProvider(ILogger<TemperatureProvider> loggingProvider)
        {
            this.loggingProvider=loggingProvider;
        }

        /// <summary>
        /// Converts Temperature from Farenheit to Celsius
        /// </summary>
        /// <param name="temperatureInFarenheit"></param>
        /// <returns></returns>
        public Task<double> ConvertTemperature(double temperatureInFarenheit)
        {
            loggingProvider.LogInformation($"Temperature in Farenheit: {temperatureInFarenheit}");
            double temperatureInCelsius = (temperatureInFarenheit - 32) * 5 / 9;
            loggingProvider.LogInformation($"Temperature in Celsius: {temperatureInCelsius}");
            return Task.FromResult(temperatureInCelsius);
        }

       
    }
}
