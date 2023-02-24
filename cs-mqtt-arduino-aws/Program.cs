namespace cs_mqtt_arduino_aws
{
    internal class Program
    {
        /// <summary>
        /// アプリケーション起動時に最初に実行されます。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Start.");
            // シリアル通信(WindowsPC<->Arduino)
            using var serial = new SerialCommunication("COM4", 9600, OnSerialReceived);
            // MQTT通信(WindowsPC<->AWS)
            using var mqtt = new MqttCommunication("broker.hivemq.com", "topic", OnMqttReceived);
            mqtt.Send("Start.");
            // コマンド入力待機
            for (; ; )
            {
                var cmd = Console.ReadLine() ?? string.Empty;
                if (cmd == "exit") { break; }
                serial.Send(cmd);
            }
            mqtt.Send("End.");
            Console.WriteLine("End.");
        }
        /// <summary>
        /// シリアル通信でデータ受信時に実行されます。
        /// </summary>
        /// <param name="data">受信データ</param>
        private static void OnSerialReceived(string data)
        {
            Console.WriteLine($"OnSerialReceived[{data}]");
        }
        /// <summary>
        /// MQTT通信でデータ受信時に実行されます。
        /// </summary>
        /// <param name="data">受信データ</param>
        private static void OnMqttReceived(string data)
        {
            Console.WriteLine($"OnMqttReceived[{data}]");
        }
    }
}
