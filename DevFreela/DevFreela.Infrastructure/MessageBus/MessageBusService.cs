using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;

        public MessageBusService(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public void Publish(string queue, byte[] message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Garantir que a fila esteja criada
                    channel.QueueDeclare(
                        queue: queue, 
                        durable: false,  // quando reiniciar o servidor os dados da fila serão apagados
                        exclusive: false, // quando "true", Permite apenas uma conexão e quando ela for finalizada vai excluir os dados
                        autoDelete: false, // Quando "true", permite várias conexões e quando terminarem os dados da fila são apagados
                        arguments: null
                        );

                    channel.BasicPublish(
                        exchange: "", // responsável por rotear as mensagens
                        routingKey: queue,
                        basicProperties: null,
                        body: message // corpo da mensagem pelo array de bytes
                        );
                        
                }
            }
        }
    }
}
