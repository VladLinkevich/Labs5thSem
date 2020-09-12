using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace laba_1
{
    public partial class Form1 : Form
    {
     
        COMPort _port;
        public Form1()
        {
            InitializeComponent();

            COMList.SelectedIndex = 0;
            SpeedList.SelectedIndex = 5;

            _port = new COMPort();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_port.IsConnect == false)
            {
                string namePort = COMList.Text;
                _port.BaundRate = SpeedList.Text;
                _port.DisplayWindow = OutputTextBox;
                if (_port.OpenPort(namePort))
                {
                    ConnectButton.Text = "Disconnect";
                    SpeedList.Enabled = false;
                    COMList.Enabled = false;
                }
            }
            else
            {
                if (_port.ClosePort())
                {
                    ConnectButton.Text = "Connect";
                    SpeedList.Enabled = true;
                    COMList.Enabled = true;
                }
                
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            _port.WriteData(InputTextBox.Text);
            InputTextBox.Text = string.Empty;
        }
    }


}
