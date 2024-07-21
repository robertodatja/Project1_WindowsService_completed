using Quartz;

namespace Project_WindowsService
{
    [DisallowConcurrentExecution]
    public class Vezhgues : IJob
    {
        private readonly IProgramMenaxheri _manageri;
        public Vezhgues(IProgramMenaxheri manageri)
        {
            _manageri = manageri;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //8. E ben nje pune
            await _manageri.BejPunen();
        }
    }
}
