using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games
{
    public partial class Form1 : Form
    {
        private List<Button> buttons;
        private int firstClickedIndex = -1;
        private int secondClickedIndex = -1;
        private bool canClick = true;
        private int player1Score = 0; // Player 1 score
        private int player2Score = 0; // Player 2 score
        private int currentPlayer = 1; // Track current player (1 or 2)

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeButtons()
        {
            var values = new List<string> { "1", "1", "2", "2", "3", "3", "4", "4", "5", "5",
        "6", "6", "7", "7", "8", "8", "9", "9", "10", "10"};

            Random random = new Random();
            values = values.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Tag = values[i]; 
                buttons[i].Text = ""; // Clear button text
                buttons[i].BackColor = SystemColors.ButtonFace;
                buttons[i].Click += btn_Click;
            }
        }


        private void InitializeGame()
        {
            buttons = new List<Button>() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10,
        btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20 };

            InitializeButtons();

            player1Score = 0; // Reset Player 1 score
            player2Score = 0; // Reset Player 2 score
            currentPlayer = 1; // Reset to Player 1
            UpdateScoreDisplay();
        }


        private async void btn_Click(object sender, EventArgs e)
        {
            if (!canClick) return;

            Button clickedButton = sender as Button;

            if (clickedButton.Text != "")
                return;

            clickedButton.Text = clickedButton.Tag.ToString();
            clickedButton.BackColor = Color.Red;

            if (firstClickedIndex == -1)
            {
                firstClickedIndex = buttons.IndexOf(clickedButton);
            }
            else
            {
                secondClickedIndex = buttons.IndexOf(clickedButton);
                canClick = false;

                if (buttons[firstClickedIndex].Tag.ToString() == buttons[secondClickedIndex].Tag.ToString())
                {
                    if (currentPlayer == 1)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }

                    CheckForGameCompletion();
                }
                else
                {
                    await Task.Delay(1000);
                    buttons[firstClickedIndex].Text = "";
                    buttons[secondClickedIndex].Text = "";
                    buttons[firstClickedIndex].BackColor = SystemColors.ButtonFace;
                    buttons[secondClickedIndex].BackColor = SystemColors.ButtonFace;
                }

                currentPlayer = currentPlayer == 1 ? 2 : 1;
                firstClickedIndex = -1;
                secondClickedIndex = -1;
                canClick = true;

                UpdateScoreDisplay();
            }
        }

        private void CheckForGameCompletion()
        {
            if (buttons.All(b => b.Text != ""))
            {
                string winnerMessage;
                if (player1Score > player2Score)
                {
                    winnerMessage = "Congratulations Player 1, you won!";
                }
                else if (player2Score > player1Score)
                {
                    winnerMessage = "Congratulations Player 2, you won!";
                }
                else
                {
                    winnerMessage = "It's a tie!";
                }

                MessageBox.Show($"{winnerMessage}\nPlayer 1: {player1Score}, Player 2: {player2Score}.");

                InitializeGame();
            }
        }

        private void UpdateScoreDisplay()
        {
            lblCurrentPlayer.Text = "Player " + currentPlayer;
            lblPlayer1Score.Text = "Player 1 Score: " + player1Score;
            lblPlayer2Score.Text = "Player 2 Score: " + player2Score;
        }

    }
}
