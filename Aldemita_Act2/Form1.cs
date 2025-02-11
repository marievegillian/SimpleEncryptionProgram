using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aldemita_Act2
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1;
        private string macAddress;

        public Form1()
        {            
            InitializeComponent();
            macAddress = GetMacAddress()?.ToLower();
        }

        private string GetMacAddress()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            return networkInterfaces
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                int[] temp = new int[fs.Length];

                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = fs.ReadByte();
                }
                fs.Close();

                fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate);
                int macLength = macAddress.Length;
                for (int y = 0; y < macLength; y++)
                {                    
                    fs.WriteByte((byte)(temp[y] + macAddress.ElementAt(y)));
                }
                fs.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                int[] temp = new int[fs.Length];

                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = fs.ReadByte();
                }
                fs.Close();

                fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate);
                int macLength = macAddress.Length;
                for (int y = 0; y < macLength; y++)
                {
                    fs.WriteByte((byte)(temp[y] - macAddress.ElementAt(y)));
                }
                fs.Close();
            }
        }
    }
}
