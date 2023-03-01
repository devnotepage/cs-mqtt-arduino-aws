using Amazon;
using Amazon.IoT;
using Amazon.IotData;
using Amazon.IotData.Model;
using Amazon.Runtime;
using System.Text;

namespace cs_mqtt_arduino_aws
{
    /// <summary>
    /// MQTT通信
    /// </summary>
    internal class MqttCommunication : IDisposable
    {
        private AmazonIotDataClient _client;
        private string _topic;
        private Action<string> _onReceived;
        internal MqttCommunication(string server, string topic, Action<string> onReceived)
        {
            string awsAccessKeyId = string.Empty;
            string awsSecretAccessKey = string.Empty;
            string fileString = File.ReadAllText(@"D:/D/AWS/AccessKeys1.csv");
            foreach (var line in fileString.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n'))
            {
                foreach (var (field, index) in line.Split(',').Select((field, index) => (field, index)))
                {
                    if (string.IsNullOrWhiteSpace(field)) { continue; }
                    if (index == 0) { awsAccessKeyId = field; }
                    if (index == 1) { awsSecretAccessKey = field; }
                }
            }
            var config = new AmazonIotDataConfig();
            config.RegionEndpoint = RegionEndpoint.USEast1;
            config.ServiceURL = server;
            _client = new AmazonIotDataClient(awsAccessKeyId, awsSecretAccessKey, config);
            _topic = topic;
            _onReceived = onReceived;
        }
        public void Send(string data)
        {
            var request = new PublishRequest();
            request.Topic = _topic;
            request.Qos = 1;
            request.Payload = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var response1 = _client.PublishAsync(request).Result;
            Console.WriteLine("HttpStatusCode:" + response1.HttpStatusCode);
            Console.WriteLine("ContentLength:" + response1.ContentLength);
            Console.WriteLine("RequestId:" + response1.ResponseMetadata.RequestId);
        }
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
