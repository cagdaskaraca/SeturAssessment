using Confluent.Kafka;
using ReportService.Models;
using SeturAssessment.Data;

namespace ReportService.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _bootstrapServers = "kafka:9092";
        private readonly string _topic = "location-created";

        public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "report-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var locationData = consumeResult.Message.Value;

                    // Create a new scope and resolve the ReportDbContext
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ReportDbContext>();

                        var report = new ReportModel
                        {
                            Id = Guid.NewGuid(),
                            Location = locationData,
                            CreatedAt = DateTime.UtcNow
                        };

                        dbContext.Reports.Add(report);
                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception and log it.
                    Console.WriteLine($"Kafka Consume Error: {ex.Message}");
                }
            }
        }
    }
}
