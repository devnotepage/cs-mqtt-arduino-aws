namespace cs_mqtt_arduino_aws
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start.");
            // シリアル通信開始
            var serial = new SerialCommunication("COM4", 9600, OnReceived);
            // コマンド入力待機
            for (; ; )
            {
                var cmd = Console.ReadLine();
                if (cmd == "exit") { break; }
                serial.Send(cmd);
            }
            Console.WriteLine("End.");
        }
        private static void OnReceived(string data)
        {
            Console.WriteLine($"OnReceived[{data}]");
        }
    }
}