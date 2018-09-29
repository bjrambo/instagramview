using System;
using System.Windows.Forms;

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
