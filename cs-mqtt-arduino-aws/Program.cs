namespace cs_mqtt_arduino_aws
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start.");

            var serial = new SerialCommunication("COM4", 9600, OnReceived);




            for (; ; )
            {
                var cmd = Console.ReadLine();
                if (cmd == "exit") { break; }
            }
            Console.WriteLine("End.");
        }

        private static void OnReceived(string data)
        {
            Console.Write($"OnReceived[{data}]");
        }
    }
}