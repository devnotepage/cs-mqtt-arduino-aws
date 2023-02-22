namespace cs_mqtt_arduino_aws
{
    /// <summary>
    /// MQTT通信
    /// </summary>
    internal class MqttCommunication : IDisposable
    {
        private Action<string> _onReceived;
        internal MqttCommunication(string port, int speed, Action<string> onReceived)
        {
            _onReceived = onReceived;
            Task.Run(() =>
            {
                for (; ; )
                {
                    _onReceived(Receive());
                }
            });
        }
        internal void Send(string data)
        {
        }
        private string Receive()
        {
            return string.Empty;
        }
        public void Dispose()
        {
        }
    }
}
