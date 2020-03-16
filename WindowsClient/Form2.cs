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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void refreshList()
        {
            byte[] msg = Encoding.ASCII.GetBytes("r");
            String rcvMsg = System.Text.Encoding.UTF8.GetString(GameManager.sendData(msg)).TrimEnd('\0');
            String[] rcvMsgList = rcvMsg.Split('\n');
            serverList.Items.Clear();
            for (int i = 0; i < rcvMsgList.Length - 1; i++)
            {
                serverList.Items.Add(rcvMsgList[i]);
            }
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            refreshList();
            if (serverList.Items.Count < 16)
            {
                this.Hide();
                Form3 formServerMake = new Form3();
                formServerMake.ShowDialog();
                this.Close();
            }
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            object selItem;
            if ((selItem = serverList.SelectedItem) != null)
            {
                String nr = serverList.GetItemText(serverList.SelectedItem);
                String nr2 = "j" + nr[0] + nr[1];
                byte[] nrByte = Encoding.ASCII.GetBytes(nr2);
                byte[] resp = GameManager.sendData(nrByte);
                String respStr = System.Text.Encoding.UTF8.GetString(resp).TrimEnd('\0');
                if (respStr[0] == '1')
                {
                    this.Hide();
                    Form4 formGame = new Form4('O', respStr.Substring(1));
                    formGame.ShowDialog();
                    this.Close();
                }
            }
        }

        private void ExitServerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            refreshList();
        }
    }
}
