using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReportService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using SeturAssessment.Data.ReportService.Data;

namespace ReportService.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ReportDbContext _dbContext;
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(ReportDbContext dbContext, ILogger<KafkaConsumerService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                GroupId = "report-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("location-created");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var locationData = consumeResult.Message.Value;

                    var report = new ReportModel
                    {
                        Id = Guid.NewGuid(),
                        Location = locationData,
                        CreatedAt = DateTime.UtcNow
                    };

                    _dbContext.Reports.Add(report);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Report created for {locationData}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error consuming Kafka message: {ex.Message}");
                }
            }
        }
    }
}
