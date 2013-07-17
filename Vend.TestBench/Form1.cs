using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using System.IO.Ports;

namespace Vend.TestBench
{
    using System.Text;

    using PrintDriver;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CommPortComboBox.DataSource = SerialPort.GetPortNames();

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var serialPort = new SerialPort())
                {
                    ////serialPort.PortName = this.CommPortComboBox.Text;
                    ////serialPort.BaudRate = int.Parse(this.BaudRateComboBox.Text);
                    ////serialPort.Parity = Parity.None;
                    ////serialPort.DataBits = 8;
                    ////serialPort.StopBits = System.IO.Ports.StopBits.One;

                    ////serialPort.Open();
                    byte[] data = new byte[] { 27, 112, 0, 25, 250 };
                    
                    ////serialPort.Write(data, 0, data.Length);
                    ////System.Threading.Thread.Sleep(10);
                    ////serialPort.WriteLine(Encoding.ASCII.GetString(data));
                    ////System.Threading.Thread.Sleep(10);
                    ////serialPort.Close();
                    
                    PrintThroughDriver.SendStringToPrinter("POS58", Encoding.ASCII.GetString(data));
                    MessageBox.Show("Message Sent");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
