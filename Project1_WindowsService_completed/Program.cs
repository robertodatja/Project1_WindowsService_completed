
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Project_WindowsService;
using Project_WindowsService;
using Quartz.Logging;
using Topshelf;

IConfiguration configFile = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();

//1. I krijuam Serviset
var services = new ServiceCollection();
services
    .AddLogging(log => { log.ClearProviders(); log.AddNLog(); })  //i krijon vete logFactory logProvider
    .AddSingleton(configFile)
    .AddScoped<Vezhgues>()
    .AddScoped<IFajllManipuluesi, FajllManipuluesi>()
    .AddScoped<IProgramMenaxheri, ProgramMenaxheri>()
    .AddScoped<IEmailService, EmailService>()
    ;

//BuildServiceProvider() e krijon Servisin per injektim
using (var serviceProvider = services.BuildServiceProvider())
{
    HostFactory.Run(ser =>
    {
        //2. HostFactory eshte vete Service Host e cila e krijon Servisin
        ser.SetServiceName("ProjektiPare");
        ser.SetDisplayName("ProjektiPare");
        ser.SetDescription("Projekti i pare ne dot net shqip");

        //3. E thirrim Servisin ketu
        ser.Service<Servisi>(s =>
        {
            //4. E krijojme kete instance Servisi me kete(serviceProvider) injektim 
            s.ConstructUsing(_ => new Servisi(serviceProvider));
            //5. E thirr metoden Fillo()
            s.WhenStarted(async ss => await ss.Fillo());
            s.WhenStopped(ss => ss.Ndalo());
        });
    });
}

