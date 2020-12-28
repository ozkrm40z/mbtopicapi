using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eventbus.Core.Domain.Entity;

namespace Eventbus.Core.Infrastructure.MesssageBroker
{
    public interface IMessageBroker
    {
        Task AddTopic(Topic topic);

        Task<IEnumerable<Topic>> GetTopics();

    }
}
