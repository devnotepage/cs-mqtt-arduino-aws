using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_mqtt_arduino_aws
{
    /// <summary>
    /// シリアル通信
    /// </summary>
    internal class SerialCommunication
    {
        private SerialPort _serialPort;
        private Action<string> _onReceived;
        internal SerialCommunication(string port, int speed, Action<string> onReceived)
        {
            _serialPort = new SerialPort(port, speed);
            _serialPort.Open();
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
            _serialPort.WriteLine(data);
        }
        private string Receive()
        {
            return _serialPort.ReadLine();
        }
    }
}
