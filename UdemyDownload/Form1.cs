using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;    

namespace UdemyDownload
{
    public partial class Form1 : Form
    {
        bool t = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Session ss = new Session("phantanhiepnt@gmail.com", "01694424958");
            ss.Login();
            textBox1.Text = ss.test;
            richTextBox1.Text = ss.get_csrf_token();
        }
    }
}
