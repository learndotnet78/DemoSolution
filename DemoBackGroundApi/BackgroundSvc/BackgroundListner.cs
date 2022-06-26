namespace DemoBackGroundApi.BackgroundSvc
{
    public class BackgroundListner : BackgroundService
    {
        private readonly ILogger<BackgroundListner> _logger;

        public BackgroundListner(ILogger<BackgroundListner> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var timer1 = new Timer(DoWork1, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

                var timer2 = new Timer(DoWork2, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(20));

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("BackgroundListner running at {Time}", DateTime.Now);
                    await Task.Delay(TimeSpan.FromSeconds(45), stoppingToken);
                }
            }
            catch
            {
                await Task.Delay(TimeSpan.FromSeconds(5),stoppingToken);
            }

        }

        private async void DoWork1(object? state)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            _logger.LogInformation("DoWork1 called at {Time}", DateTime.Now);
        }

        private async void DoWork2(object? state)
        {
            await Task.Delay(TimeSpan.FromSeconds(20));
            _logger.LogInformation("DoWork2 called at {Time}", DateTime.Now);
        }

    }
}
