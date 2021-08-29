using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperProga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyValue == (char)Keys.Enter) button1_Click(button1, null); };
        }

        void button1_Click(object sender, EventArgs e)
        {
            string line = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString($"http://ipwhois.app/xml/{textBox1.Text}");
            Match match = Regex.Match(line, "<country>(.*?)</country>(.*?)<region>(.*?)</region>(.*?)<city>(.*?)</city>(.*?)<timezone_gmt>(.*?)</timezone_gmt>");
            Match matchloc = Regex.Match(line, "<latitude>(.*?)</latitude>(.*?)<longitude>(.*?)</longitude>");
            label5.Text = match.Groups[1].Value;
            label6.Text = match.Groups[3].Value;
            label7.Text = match.Groups[5].Value;
            label8.Text = match.Groups[7].Value;
            string latitude = matchloc.Groups[1].Value.ToString();
            string longitude = matchloc.Groups[3].Value.ToString();
            webBrowser1.Url = new Uri($"https://www.google.com/maps/search/?api=1&query={latitude},{longitude}");
        }
    } 
}

