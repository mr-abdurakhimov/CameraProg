using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace consumer2._0
{
    public partial class Win_shower : Form
    {
        public Win_shower()
        {
            InitializeComponent();
        }

        private async void Win_shower_Load(object sender, EventArgs e)
        {
            var port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            var client = new UdpClient(port);
            while (true)
            {
                var data = await client.ReceiveAsync();
                Console.WriteLine(data);
                using (var ms = new MemoryStream(data.Buffer))
                {
                    Console.WriteLine($"{ms.Length}");
                    pictureBox1.Image = new Bitmap(ms);
                }
                Text = $"Bytes received: {data.Buffer.Length * sizeof(byte)}";
            }
        }
    }
}
