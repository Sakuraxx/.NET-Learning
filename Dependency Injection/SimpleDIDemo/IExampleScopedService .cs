using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDIDemo
{
    public interface IExampleScopedService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Scoped;
    }
}
