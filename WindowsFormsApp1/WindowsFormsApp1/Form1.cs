using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool goLeft = false;
        bool goRight = false;
        bool jumping = false;
        bool isGameOver = false;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 3;
        int enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_tick(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 50;
            }

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    if ((string)control.Tag == "Platform")
                    {
                        if (player.Bounds.IntersectsWith(control.Bounds))
                        {
                            force = 8;
                            player.Top = control.Top - player.Height;

                            if ((string)control.Name == "horizontalPlatform" && goLeft == false || (string)control.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }

                        control.BringToFront();
                    }
                }

                if ((string)control.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds) && control.Visible == true)
                    {
                        control.Visible = false;
                        score++;
                    }
                }


                if ((string)control.Tag == "enemy")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        gameTimer.Stop();
                        isGameOver = true;
                        lblScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!";
                        MessageBox.Show("Press Enter to Restart Game", "GAME OVER");
                    }
                }
            }

            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 195 || verticalPlatform.Top > 581)
            {
                verticalSpeed = -verticalSpeed;
            }


            enemyOne.Left += enemyOneSpeed;

            if (enemyOne.Left < pictureBox4.Left || enemyOne.Left + enemyOne.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }


            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
                MessageBox.Show("Press Enter to Restart Game", "GAME OVER");
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 26)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = "Score: " + score + Environment.NewLine + "Your quest is completed!";
                MessageBox.Show("Congratulations!! You Won the Game!", "YOU WON");
            }
            else if (player.Bounds.IntersectsWith(door.Bounds) && score != 26)
            {
                isGameOver = false;
                lblScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                Restart();
            }
        }

        private void Restart()
        {
            goLeft = goRight = jumping = isGameOver = false;
            score = 0;

            lblScore.Text = "Score: " + score;

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && !control.Visible)
                {
                    control.Visible = true;
                }
            }

            //it will reset positon of player, platform and enemies

            player.Left = 12;
            player.Top = 604;

            enemyOne.Left = 299;
            enemyTwo.Left = 333;

            horizontalPlatform.Left = 247;
            verticalPlatform.Top = 505;

            gameTimer.Start();
        }
    }
}