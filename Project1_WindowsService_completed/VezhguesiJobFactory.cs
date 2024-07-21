using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Project_WindowsService
{
    public class VezhguesiJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public VezhguesiJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<Vezhgues>();
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
