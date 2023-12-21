using System.Threading.Tasks;

namespace Middleware.Business.Interfaces
{
    public interface ITemperatureProvider
    {
        Task<double> ConvertTemperature(double temperatureInFarenheit);
    }
}