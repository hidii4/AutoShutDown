using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AutoShutDown
{
    public partial class Autoshutdown : Form
    {
        public Autoshutdown()
        {
            InitializeComponent();
        }

        private int sec = 0;
        private int min = 0;
        private int a = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            int value;

            if (int.TryParse(s, out value))
            {
                sec = (value * 60);
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
                
                textBox1.Enabled = false;
                progressBar1.Enabled = true;
                progressBar1.Maximum = sec;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                label1.Text = "Nieprawidłowe dane wejściowe";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            min = sec / 60;
            int temp = (sec-- % 60);
            if(temp < 10)
            {
                label1.Text = "Pozostały czas: " + min.ToString() + ":0" + temp.ToString();
            }
            else
            {
                label1.Text = "Pozostały czas: " + min.ToString() + ":" + temp.ToString();
            }
            
            if(sec == 0)
            {
                timer1.Stop();
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }

            progressBar1.Value = a++;
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = false;
            button3.Text = "Pause";
            textBox1.Enabled = true;
            label1.Text = "Pozostały czas: ";
            button2.Enabled = false;
            progressBar1.Enabled = false;
            progressBar1.Value = 0;
            a = 0;
            min = 0;
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(button3.Text == "Pause")
            {
                timer1.Stop();
                progressBar1.Enabled = false;
                button3.Text = "Resume";
            }
            else
            {
                timer1.Start();
                progressBar1.Enabled = true;
                button3.Text = "Pause";
            }
            
        }
    }
}
