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
            var serial = new SerialCommunication("COM4", 9600, OnSerialReceived);
            // MQTT通信(WindowsPC<->AWS)




            // コマンド入力待機
            for (; ; )
            {
                var cmd = Console.ReadLine();
                if (cmd == "exit") { break; }
                serial.Send(cmd);
            }
            Console.WriteLine("End.");
        }
        /// <summary>
        /// シリアル通信でデータ受信に実行されます。
        /// </summary>
        /// <param name="data"></param>
        private static void OnSerialReceived(string data)
        {
            Console.WriteLine($"OnReceived[{data}]");
        }
    }
}