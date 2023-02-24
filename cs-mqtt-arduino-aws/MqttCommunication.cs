using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace cs_mqtt_arduino_aws
{
    /// <summary>
    /// MQTT通信
    /// </summary>
    internal class MqttCommunication : IDisposable
    {
        private MqttFactory _mqttFactory;
        private IMqttClient _mqttClient;
        private MqttClientOptions _mqttClientOptions;
        private MqttClientSubscribeOptions _mqttSubscribeOptions;
        private string _topic;
        private Action<string> _onReceived;
        internal MqttCommunication(string server, string topic, Action<string> onReceived)
        {
            _topic = topic;
            _onReceived = onReceived;
            _mqttFactory = new MqttFactory();
            _mqttClient = _mqttFactory.CreateMqttClient();
            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("Received application message.");
                Console.WriteLine(e.ToString());
                _onReceived(e.ToString());
                return Task.CompletedTask;
            };
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(server)
                .Build();
            _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
            _mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f =>
                    {
                        f.WithTopic(_topic);
                    })
                .Build();
            _mqttClient.SubscribeAsync(_mqttSubscribeOptions, CancellationToken.None);
        }
        public void Send(string data)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(_topic)
                .WithPayload(data)
                .Build();
            _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            Console.WriteLine("MQTT application message is published.");
        }
        public void Dispose()
        {
            _mqttClient.DisconnectAsync();
            _mqttClient.Dispose();
        }
    }
}
