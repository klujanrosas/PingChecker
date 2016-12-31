using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PingChecker
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        PrivateFontCollection private_fonts = new PrivateFontCollection();
        public RadForm1()
        {
            InitializeComponent();

            ThemeResolutionService.ApplicationThemeName = "VisualStudio2012Dark";
            // specify embedded resource name
            string resource = "PingChecker.Hoonburguer.ttf";

            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);

            // create a buffer to read in to
            byte[] fontdata = new byte[fontStream.Length];

            // read the font data from the resource
            fontStream.Read(fontdata, 0, (int)fontStream.Length);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);

            // pass the font to the font collection
            private_fonts.AddMemoryFont(data, (int)fontStream.Length);

            // close the resource stream
            fontStream.Close();

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(data);

            radLabel1.UseCompatibleTextRendering = true;
            radLabel1.Font = new Font(private_fonts.Families[0], 20);
            radLabel2.UseCompatibleTextRendering = true;
            radLabel2.Font = new Font(private_fonts.Families[0], 20);
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
