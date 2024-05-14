using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDIDemo
{
    public interface IReportServiceLifetime
    {
        Guid Id { get; }

        ServiceLifetime Lifetime { get; }
    }
}
