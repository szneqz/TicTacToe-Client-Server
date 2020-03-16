using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class mainForm : Form
    {
        public void setErrorText(string errorMsg)
        {
            errorLabel.Text = errorMsg;
        }
        public void proceed()
        {
            if (nicknameBox.Text == "")
                nicknameBox.Text = "defaultName";
            GameManager.FdID = GameManager.sendData(Encoding.ASCII.GetBytes("c" + nicknameBox.Text + "\0"));
            this.Hide();
            Form2 formList = new Form2();
            formList.ShowDialog();
            this.Close();
        }
        public mainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GameManager.init(this, addrBox.Text);
        }
    }
}
