using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_1
{
    class COMPort
    {
        private const string STARTBIT = "01111110";
        private string _baudRate;

        private SerialPort _serialPort;
        private TextBox _displayWindow;

        public string BaundRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public TextBox DisplayWindow
        {
            get { return _displayWindow; }
            set { _displayWindow = value; }
        }

        public bool IsConnect
        {
            get { return _serialPort.IsOpen; }
        }

        public COMPort()
        {
            Init();
        }

        public void Init()
        {
            _serialPort = new SerialPort();
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
        }

        public bool OpenPort(string portName)
        {
            try
            {
                if (_serialPort.IsOpen == true) { ClosePort(); }

                _serialPort.PortName = portName;
                _serialPort.BaudRate = int.Parse(BaundRate);
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;

                _serialPort.Open();

                DisplayData("Port opened at " + DateTime.Now + "\r\n");

                return true;
            }
            catch (Exception ex)
            {
                DisplayData(ex.Message);
                return false;
            }
        }

        public bool ClosePort()
        {
            try
            {
                _serialPort.Close();
                return true;
            }
            catch (Exception ex)
            {
                DisplayData("Close port: " + ex.Message);
                return false;
            }
        }

        public bool WriteData(string msg)
        {
            try
            {
                if (_serialPort.IsOpen != true) { _serialPort.Open(); }
                _serialPort.Write(Serialize(msg));

                return true;
            } catch (Exception ex)
            {
                DisplayData(ex.Message);
                return false;
            }
        }

        private string Serialize(string msg)
        {
            string result = string.Empty;

            foreach (char letter in msg){
                for (int i = sizeof(char) * 8 - 1; i >= 0; i--)
                {
                    if ((letter & (1 << i)) != 0) { result += '1'; }
                    else { result += '0'; }
                }
            }

            int rep = 0;
            for (int i = 0; i < result.Length; ++i)
            {
                if (result[i] == '1')
                {
                    rep++;
                    if (rep == 5) {
                        result = result.Insert(i+1, "0");
                        ++i;
                    }
                }
                else { rep = 0; }
            }

            result = result.Insert(0, STARTBIT);
            result += STARTBIT;

            return result;
        }

        private string Unserialize(string msg)
        {
            string b_msg = msg.Substring(8, msg.Length - 16);
            string result = string.Empty;

            int rep = 0;
            for (int i = 0; i < b_msg.Length; ++i)
            {
                if (b_msg[i] == '1')
                {
                    rep++;
                    if (rep == 5)
                    {
                        b_msg = b_msg.Substring(0, i+1) + b_msg.Substring(i+2, b_msg.Length - (i + 2));
                    }
                }
                else { rep = 0; }
            }

            for (int i = 0, end = b_msg.Length / 16; i < end; ++i)
            {
                char buffer = (char)Convert.ToByte(b_msg.Substring(16 * i, 16), 2); ;
                result += buffer;
            }

            return result;
        }

        private void DisplayData(string msg)
        {
            _displayWindow.Invoke(new EventHandler(delegate
            {
                _displayWindow.AppendText(msg);
                _displayWindow.ScrollToCaret();
            }));
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string msg = Unserialize(_serialPort.ReadExisting());
            DisplayData(msg + "\r\n");               
        }
    }
}
