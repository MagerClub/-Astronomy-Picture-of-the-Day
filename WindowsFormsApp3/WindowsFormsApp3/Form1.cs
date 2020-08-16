using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        DateTime today = DateTime.Today;
        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
            getdatanow();
            label4.Hide();
            int date = int.Parse(today.ToString("dd"));
            string datetime = today.ToString("yyyy-MM-" + date.ToString());
            label5.Text = "Date : " + datetime;
        }

        public string get_dateyesterday()
        {
            int date = int.Parse(today.AddDays(-2).ToString("dd"));
            string datetime = today.ToString("yyyy-MM-" + date.ToString());
            return datetime;
        }

        public string get_datetime()
        {
            int date = int.Parse(today.AddDays(-1).ToString("dd"));
            string datetime = today.ToString("yyyy-MM-" + date.ToString());
            return datetime;
        }

        public string getdatanow()
        {
            string datetime = get_datetime();
            var client = new RestClient("https://api.nasa.gov/planetary/apod?hd=True&api_key=DEMO_KEY&date="+datetime);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            dynamic data = JObject.Parse(response.Content);
            string url = data.url;
            string explain = data.explanation;
            string title = data.title;
            pictureBox1.Load(url);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Text = explain;
            label6.Text = title;
            return title;
        }

        public string getdatapast()
        {
            string datetime = get_dateyesterday();
            var client = new RestClient("https://api.nasa.gov/planetary/apod?hd=True&api_key=DEMO_KEY&date=" + datetime);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            dynamic data = JObject.Parse(response.Content);
            string url = data.url;
            string explain = data.explanation;
            string title = data.title;
            pictureBox1.Load(url);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Text = explain;
            label6.Text = title;
            return title;
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.LightBlue;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(6, 55, 152);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.BackColor = Color.LightBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(6, 55, 152);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Hide();
            getdatapast();
            label4.Show();
            int date = int.Parse(today.AddDays(-1).ToString("dd"));
            string datetime = today.ToString("yyyy-MM-" + date.ToString());
            label5.Text = "Date : "+datetime;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.BackColor = Color.LightBlue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.BackColor = Color.FromArgb(6, 55, 152);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Hide();
            getdatanow();
            label3.Show();
            int date = int.Parse(today.ToString("dd"));
            string datetime = today.ToString("yyyy-MM-" + date.ToString());
            label5.Text = "Date : "+datetime;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
