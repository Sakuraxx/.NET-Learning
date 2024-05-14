namespace ConsoleDIDemo
{
    internal sealed class ServiceLifetimeReporter
    {
        private readonly IExampleTransientService transientService;
        private readonly IExampleScopedService scopedService;
        private readonly IExampleSingletonService singletonService;

        public ServiceLifetimeReporter(
            IExampleTransientService transientService,
            IExampleScopedService scopedService,
            IExampleSingletonService singletonService)
        {
            this.transientService = transientService;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
        }

        public void ReportServiceLifetimeDetails(string lifetimeDetails)
        {
            Console.WriteLine(lifetimeDetails);

            LogService(transientService, "Always different");
            LogService(scopedService, "Changes only with lifetime");
            LogService(singletonService, "Always the same");
        }

        private static void LogService<T>(T service, string message)
            where T : IReportServiceLifetime
        {
            Console.WriteLine($"   {typeof(T).Name}: {service.Id} ({message})");
        }
    }
}
