using Microsoft.Extensions.Logging;

namespace Project_WindowsService
{
    public class ProgramMenaxheri : IProgramMenaxheri
    {
        private readonly ILogger<ProgramMenaxheri> _logger;
        private readonly IEmailService _emailService;
        private readonly IFajllManipuluesi _fileManipuluesi;

        public ProgramMenaxheri(ILogger<ProgramMenaxheri> logger, IEmailService emailService, IFajllManipuluesi fileManipuluesi)
        {
            _logger = logger;
            _emailService = emailService;
            _fileManipuluesi = fileManipuluesi;
        }

        public async Task BejPunen()
        {
            try
            {
                foreach (var item in _fileManipuluesi.LexoPermbajtjen())
                {
                    //Dergo Emailin
                    await _emailService.SendEmailAsync(new EmailData { Body = item, Subject = "test", ToEmail = "robydatja@live.com", ToName = "Berti" });
                }

                _fileManipuluesi.Fshijfajllat();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Ndodhi gabim...");
                throw;
            }
        }
    }
}
