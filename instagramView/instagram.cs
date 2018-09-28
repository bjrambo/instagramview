using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace instagramView
{
    public partial class instagram : Form
    {
        private static InstagramClient mInstagramClient;

        public instagram()
        {
            InitializeComponent();
            mInstagramClient = new InstagramClient(webBrowser1);
        }


        private void instagram_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstagramClient.GetData();
        }
    }
}
