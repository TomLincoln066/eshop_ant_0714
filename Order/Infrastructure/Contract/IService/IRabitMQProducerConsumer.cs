using ApplicationCore.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IRabitMQProducerConsumer
    {
        public void SendOrderMessage<T>(T message);
        public IEnumerable<OrderViewModel> ReadMessage(ILogger logger);
    }
}
