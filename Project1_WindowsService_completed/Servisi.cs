using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_WindowsService;

namespace Project_WindowsService
{
    //duhet te injektojme disa varesi
    public class Servisi
    {
        private readonly ILogger<Servisi> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Servisi(IServiceProvider serviceProvider) //ctor
        {
            _serviceProvider = serviceProvider;
            _configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            _logger = _serviceProvider.GetRequiredService<ILogger<Servisi>>();
        }

        public async Task Fillo()
        {
            _logger.LogInformation("Servisi startoi");
            //6.E  thirr Planifikuesin
            await Planifikuesi();
        }

        public void Ndalo()
        {
            _logger.LogInformation("Servisi ndaloi");
        }

        async Task Planifikuesi()
        {
            if (!int.TryParse(_configuration["FrekuencaVezhgimit"], out var frekuenca))
                frekuenca = 2; //vlera default nese deshton konvertimi

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = new VezhguesiJobFactory(_serviceProvider);

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<Vezhgues>().WithIdentity("Vezhguesi fajllave").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("frekuenca e Vezhguesit te fajllave")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(frekuenca).RepeatForever())
                .Build();

            //7. Krijon kodin me siper dhe ne dund ben ScheduleJob, por cfare Job. Job-in Vezhgues
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
