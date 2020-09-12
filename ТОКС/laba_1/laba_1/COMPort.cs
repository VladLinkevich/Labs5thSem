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
                _serialPort.Write(msg);

                return true;
            } catch (Exception ex)
            {
                DisplayData(ex.Message);
                return false;
            }
        }

        [STAThread]
        private void DisplayData(string msg)
        {
            _displayWindow.Invoke(new EventHandler(delegate
            {
                //_displayWindow.Text = string.Empty;
                _displayWindow.AppendText(msg);
                _displayWindow.ScrollToCaret();
            }));
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                    string msg = _serialPort.ReadExisting();
                    //display the data to the user
                    DisplayData(msg + "\r\n");               
        }
    }
}
