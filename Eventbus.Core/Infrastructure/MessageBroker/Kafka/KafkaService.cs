using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Eventbus.Core.Domain.Entity;
using Eventbus.Core.Infrastructure.MesssageBroker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Core.Infrastructure.MessageBroker.Kafka
{
    public class KafkaService : IMessageBroker
    {

        private readonly IAdminClient _kafkaAdminClient;

        public KafkaService(string bootstrapServers) {
            _kafkaAdminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();
        }

        public async Task AddTopic(Topic topic)
        {
            using (var adminClient = _kafkaAdminClient)
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = topic.Name, ReplicationFactor = 1, NumPartitions = 1 } });
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }
        }

        public Task<IEnumerable<Topic>> GetTopics()
        {
            throw new NotImplementedException();
        }
    }
}
