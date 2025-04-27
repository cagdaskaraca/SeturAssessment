using Confluent.Kafka;

public class KafkaProducer
{
    private readonly string _bootstrapServers = "kafka:9092"; // Docker networkü üzerinden Kafka servisine bağlanılır

    public async Task PublishAsync(string topic, string message)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
}