using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PingChecker
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();

            ThemeResolutionService.ApplicationThemeName = "VisualStudio2012Dark";
        }

        private async void radButton1_Click(object sender, EventArgs e)
        {
            radLabel1.Text = "Pinging...";
            string result = await Task.Run(() => GetAveragegMs("104.160.136.3"));
            radLabel1.Text = $"{result}ms";
        }
        private async void radButton2_Click(object sender, EventArgs e)
        {
            radLabel2.Text = "Pinging...";
            string result = await Task.Run(() => GetAveragegMs("iad.valve.net"));
            radLabel2.Text = $"{result}ms";
        }

        public string GetAveragegMs(string address) 
        {
            int avg = 0;
            using (Ping ping = new Ping())
            {
                try
                {
                   
                        for (int i = 0; i < 10; i++)
                        {
                            PingReply reply = ping.Send(address);

                            if (reply.Status == IPStatus.Success)
                            {
                                avg += (int)reply.RoundtripTime;
                            }
                        }


                    return (avg / 10).ToString();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


    }
}
