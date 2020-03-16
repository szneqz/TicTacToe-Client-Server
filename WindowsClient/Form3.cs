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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 formList = new Form2();
            formList.ShowDialog();
            this.Close();
        }

        private void AcceptAddButton_Click(object sender, EventArgs e)
        {
            byte[] msg = Encoding.ASCII.GetBytes("m" + serverNameBox.Text + "\0");
            byte[] recv = GameManager.sendData(msg);
            String strRecv = System.Text.Encoding.UTF8.GetString(recv).TrimEnd('\0');
            if (strRecv[0] == '1')
            {
                System.Console.WriteLine("4");
                this.Hide();
                Form4 formGame = new Form4('X', "");
                formGame.Text = serverNameBox.Text;
                formGame.ShowDialog();
                this.Close();
            }
        }
    }
}
