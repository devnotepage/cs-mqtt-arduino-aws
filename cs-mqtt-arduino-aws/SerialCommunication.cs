using System.IO.Ports;

namespace cs_mqtt_arduino_aws
{
    /// <summary>
    /// シリアル通信
    /// </summary>
    internal class SerialCommunication
    {
        /// <summary>
        /// シリアルポート
        /// </summary>
        private SerialPort _serialPort;
        /// <summary>
        /// 受信時のアクション
        /// </summary>
        private Action<string> _onReceived;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="port">ポート名</param>
        /// <param name="speed">ポート速度</param>
        /// <param name="onReceived">受信時のアクション</param>
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
        /// <summary>
        /// データ送信
        /// </summary>
        /// <param name="data">送信文字列</param>
        internal void Send(string data)
        {
            _serialPort.WriteLine(data);
        }
        /// <summary>
        /// データ受信
        /// </summary>
        /// <returns>受信文字列</returns>
        private string Receive()
        {
            return _serialPort.ReadLine();
        }
    }
}
