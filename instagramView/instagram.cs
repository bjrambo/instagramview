using System;
using System.Windows.Forms;

namespace instagramView
{
    public partial class instagram : Form
    {
        private static InstagramClient mInstagramClient;
        public static string INSERT_TEXT = null;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            INSERT_TEXT = textBox1.Text;
        }
    }
}
