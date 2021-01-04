using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppAPITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string url = string.Format("http://localhost:52063/RestService/add/{0}/{1}", textBox1.Text, textBox2.Text);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://httpbin.org/get");
            req.Method = "GET";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            for (int i = 0; i < res.Headers.Count; i++)
            {
                textBox4.Text += res.Headers[i] + "\r\n";
            }

            Stream s = res.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            string content = sr.ReadToEnd();

            textBox4.Text += "\r\n\r\n-----------\r\n\r\n\r\n";
            textBox4.Text += content;

            Regex reg = new Regex("^\"(?<value>.*)\"$");
            Match match = reg.Match(content);

            textBox3.Text = match.Groups["value"].Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string postData = string.Format("{{\"value1\":\"{0}\", \"value2\":\"{1}\"}}", textBox1.Text, textBox2.Text);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:52063/RestService/addi");
            req.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            req.ContentType = "application/json";
            req.ContentLength = byteArray.Length;

            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            for (int i = 0; i < res.Headers.Count; i++)
            {
                textBox4.Text += res.Headers[i] + "\r\n";
            }
            textBox4.Text += "\r\n\r\n-----------\r\n\r\n\r\n";

            Stream s = res.GetResponseStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AddStringInverseResponse));
            AddStringInverseResponse ar = (AddStringInverseResponse)serializer.ReadObject(s);
            textBox3.Text = ar.result + " : " + ar.resultid.ToString();

        }
    }
}
