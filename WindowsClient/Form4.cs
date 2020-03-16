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
    public partial class Form4 : Form
    {
        public Form4(char symbol, String nickname)
        {
            InitializeComponent();
            actualSymbol = symbol;
            symbolLabel.Text = "Twój symbol to: " + actualSymbol.ToString();
            oponentLabel.Text = "Twój oponent to: " + nickname;
            if(symbol == 'O')
            {
                gameButton00.Enabled = false;
                gameButton01.Enabled = false;
                gameButton02.Enabled = false;
                gameButton10.Enabled = false;
                gameButton11.Enabled = false;
                gameButton12.Enabled = false;
                gameButton20.Enabled = false;
                gameButton21.Enabled = false;
                gameButton22.Enabled = false;
                moveInfoLabel.Text = "Ruch przeciwnika!";
                moveInfoLabel.ForeColor = Color.Red;
            }
        }

        private char[] listXO = {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
        public char actualSymbol = ' ';
        private char winner;
        Timer MyTimer;

        private void restartGame()
        {
            gameButton00.Enabled = true;
            gameButton01.Enabled = true;
            gameButton02.Enabled = true;
            gameButton10.Enabled = true;
            gameButton11.Enabled = true;
            gameButton12.Enabled = true;
            gameButton20.Enabled = true;
            gameButton21.Enabled = true;
            gameButton22.Enabled = true;
            gameButton00.Text = " ";
            gameButton01.Text = " ";
            gameButton02.Text = " ";
            gameButton10.Text = " ";
            gameButton11.Text = " ";
            gameButton12.Text = " ";
            gameButton20.Text = " ";
            gameButton21.Text = " ";
            gameButton22.Text = " ";
            for(int i = 0; i < 9; i++)
            {
                listXO[i] = ' ';
            }
        }
        private void checkWin(bool isSending)
        {
            winner = 'N';
            int tmp = 0;
            for (int i = 0; i < 9; i++)
            {
                if (listXO[i] == ' ')
                    tmp++;
            }

            if (tmp == 9)
                winner = ' ';

            for (int i = 0; i < 3; i++)
            {   //sprawdzanie pionow i poziomow
                if ((listXO[i] == 'X' && listXO[i + 3] == 'X' && listXO[i + 6] == 'X') ||
                    (listXO[i * 3] == 'X' && listXO[i * 3 + 1] == 'X' && listXO[i * 3 + 2] == 'X'))
                    winner = 'X';
                if ((listXO[i] == 'O' && listXO[i + 3] == 'O' && listXO[i + 6] == 'O') ||
                    (listXO[i * 3] == 'O' && listXO[i * 3 + 1] == 'O' && listXO[i * 3 + 2] == 'O'))
                    winner = 'O';
            }
            //sprawdzanie przekatnych
            if ((listXO[0] == 'X' && listXO[4] == 'X' && listXO[8] == 'X') ||
                (listXO[2] == 'X' && listXO[4] == 'X' && listXO[6] == 'X'))
                winner = 'X';
            if ((listXO[0] == 'O' && listXO[4] == 'O' && listXO[8] == 'O') ||
                (listXO[2] == 'O' && listXO[4] == 'O' && listXO[6] == 'O'))
                winner = 'O';

            if (winner == 'X' || winner == 'O')
            {
                moveInfoLabel.Text = "Wygrywa gracz " + winner.ToString(); //+ nickname;
                gameButton00.Enabled = false;
                gameButton01.Enabled = false;
                gameButton02.Enabled = false;
                gameButton10.Enabled = false;
                gameButton11.Enabled = false;
                gameButton12.Enabled = false;
                gameButton20.Enabled = false;
                gameButton21.Enabled = false;
                gameButton22.Enabled = false;

                if (actualSymbol == 'X')
                    restartButton.Enabled = true;

                if(isSending)
                    GameManager.sendData(Encoding.ASCII.GetBytes("w"));
            }

            if(winner == ' ')
            {
                moveInfoLabel.Text = "Remis!";
                if (actualSymbol == 'X')
                    restartButton.Enabled = true;

                if (isSending)
                    GameManager.sendData(Encoding.ASCII.GetBytes("w"));
            }
        }

        private void zmianaRuchu(bool isMine)
        {
            if(isMine)
            {
                moveInfoLabel.Text = "Twój ruch!";
                moveInfoLabel.ForeColor = Color.ForestGreen;
                if (listXO[0] == ' ')
                    gameButton00.Enabled = true;
                if (listXO[1] == ' ')
                    gameButton01.Enabled = true;
                if (listXO[2] == ' ')
                    gameButton02.Enabled = true;
                if (listXO[3] == ' ')
                    gameButton10.Enabled = true;
                if (listXO[4] == ' ')
                    gameButton11.Enabled = true;
                if (listXO[5] == ' ')
                    gameButton12.Enabled = true;
                if (listXO[6] == ' ')
                    gameButton20.Enabled = true;
                if (listXO[7] == ' ')
                    gameButton21.Enabled = true;
                if (listXO[8] == ' ')
                    gameButton22.Enabled = true;

                gameButton00.Text = listXO[0].ToString();
                gameButton01.Text = listXO[1].ToString();
                gameButton02.Text = listXO[2].ToString();
                gameButton10.Text = listXO[3].ToString();
                gameButton11.Text = listXO[4].ToString();
                gameButton12.Text = listXO[5].ToString();
                gameButton20.Text = listXO[6].ToString();
                gameButton21.Text = listXO[7].ToString();
                gameButton22.Text = listXO[8].ToString();
            }
            else
            {
                moveInfoLabel.Text = "Ruch przeciwnika!";
                moveInfoLabel.ForeColor = Color.Red;
                gameButton00.Enabled = false;
                gameButton01.Enabled = false;
                gameButton02.Enabled = false;
                gameButton10.Enabled = false;
                gameButton11.Enabled = false;
                gameButton12.Enabled = false;
                gameButton20.Enabled = false;
                gameButton21.Enabled = false;
                gameButton22.Enabled = false;
            }
        }

        private void sendMovData()
        {
            String mov = "";
            if (actualSymbol == 'O')
                mov = "X";
            else
                mov = "O";
            GameManager.sendData(Encoding.ASCII.GetBytes("q" + listXO[0] + listXO[1] + listXO[2] + listXO[3] + listXO[4] + listXO[5] + listXO[6] + listXO[7] + listXO[8] + mov));
        }

        private void GameButton22_Click(object sender, EventArgs e)
        {
            gameButton22.Text = actualSymbol.ToString();
            listXO[8] = actualSymbol;
            gameButton22.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton00_Click(object sender, EventArgs e)
        {
            gameButton00.Text = actualSymbol.ToString();
            listXO[0] = actualSymbol;
            gameButton00.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton10_Click(object sender, EventArgs e)
        {
            gameButton10.Text = actualSymbol.ToString();
            listXO[3] = actualSymbol;
            gameButton10.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton20_Click(object sender, EventArgs e)
        {
            gameButton20.Text = actualSymbol.ToString();
            listXO[6] = actualSymbol;
            gameButton20.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton21_Click(object sender, EventArgs e)
        {
            gameButton21.Text = actualSymbol.ToString();
            listXO[7] = actualSymbol;
            gameButton21.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton11_Click(object sender, EventArgs e)
        {
            gameButton11.Text = actualSymbol.ToString();
            listXO[4] = actualSymbol;
            gameButton11.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton12_Click(object sender, EventArgs e)
        {
            gameButton12.Text = actualSymbol.ToString();
            listXO[5] = actualSymbol;
            gameButton12.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton02_Click(object sender, EventArgs e)
        {
            gameButton02.Text = actualSymbol.ToString();
            listXO[2] = actualSymbol;
            gameButton02.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void GameButton01_Click(object sender, EventArgs e)
        {
            gameButton01.Text = actualSymbol.ToString();
            listXO[1] = actualSymbol;
            gameButton01.Enabled = false;
            zmianaRuchu(false);
            sendMovData();
            checkWin(true);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            MyTimer.Stop();
            GameManager.sendData(Encoding.ASCII.GetBytes("h"));
            this.Hide();
            Form2 formList = new Form2();
            formList.ShowDialog();
            formList.refreshList();
            this.Close();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            //jezeli gra zakonczy sie i zalozyciel postanowi zresetowac gre to jest ona resetowana
            restartGame();
            sendMovData();
            GameManager.sendData(Encoding.ASCII.GetBytes("u"));
            restartButton.Enabled = false;
        }

        private void SymbolLabel_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            MyTimer = new Timer();
            MyTimer.Interval = (1000); // 1 sekunda
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            //Tu pytam serwer o stan rozgrywki
            byte[] recv = GameManager.sendData(Encoding.ASCII.GetBytes("g"));
            String recvStr = System.Text.Encoding.UTF8.GetString(recv).TrimEnd('\0');
            if (recvStr[0] == '0')
            {
                MyTimer.Stop();
                this.Hide();
                Form2 formList = new Form2();
                formList.ShowDialog();
                formList.refreshList();
                this.Close();
            }
            if(recvStr[0] == '1')
            {
                for(int i = 0; i < 9; i++)
                listXO[i] = recvStr[i + 1];
                if (recvStr[10] == actualSymbol)
                    zmianaRuchu(true);
                else
                    zmianaRuchu(false);
                oponentLabel.Text = "Twój oponent to: " + recvStr.Substring(11);
            }
            if(recvStr[0] == '2')
            {
                for (int i = 0; i < 9; i++)
                    listXO[i] = recvStr[i + 1];

                gameButton00.Text = listXO[0].ToString();
                gameButton01.Text = listXO[1].ToString();
                gameButton02.Text = listXO[2].ToString();
                gameButton10.Text = listXO[3].ToString();
                gameButton11.Text = listXO[4].ToString();
                gameButton12.Text = listXO[5].ToString();
                gameButton20.Text = listXO[6].ToString();
                gameButton21.Text = listXO[7].ToString();
                gameButton22.Text = listXO[8].ToString();

                checkWin(false);
                if(winner != ' ')
                    moveInfoLabel.Text = "Wygrywa gracz " + winner.ToString();
                else
                    moveInfoLabel.Text = "Remis!";
            }
            if(recvStr[0] == '3')
            {
                for (int i = 0; i < 9; i++)
                    listXO[i] = ' ';
                zmianaRuchu(true);
                moveInfoLabel.Text = "Oczekiwanie na oponenta";
                moveInfoLabel.ForeColor = Color.Red;
                gameButton00.Enabled = false;
                gameButton01.Enabled = false;
                gameButton02.Enabled = false;
                gameButton10.Enabled = false;
                gameButton11.Enabled = false;
                gameButton12.Enabled = false;
                gameButton20.Enabled = false;
                gameButton21.Enabled = false;
                gameButton22.Enabled = false;
            }
        }
    }
}
