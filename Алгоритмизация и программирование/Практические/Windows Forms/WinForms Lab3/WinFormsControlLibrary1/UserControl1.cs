namespace WinFormsControlLibrary1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public bool TimeEnabled
        {
            get { return timer1.Enabled; }
            set { timer1.Enabled = value; }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
