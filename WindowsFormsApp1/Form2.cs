using System;
using System.IO;
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
    public partial class Form2 : Form
    {
        private int width = 600, heigh = 400;
        private int snakeX = 1, snakeY = 0;
        private int fruitX, fruitY;
        private int score = 0;
        private int sizeOfSides = 20;
        PictureBox fruit = new PictureBox();
        PictureBox[] tail = new PictureBox[500];  
        Timer time = new Timer();

        public Form2()
        {
            InitializeComponent();
            BackgroundImage = Properties.Resources.back;
            DrawMap(1, 1);
            DrawMap(581, 381);

            time.Tick += new EventHandler(MoveSnake);
            time.Interval = 300;
            time.Start();
            label1.Text = "Score: " + score;
            tail[0] = new PictureBox();
            tail[0].Location = new Point(width / 2, heigh / 2);
            tail[0].Size = new Size(sizeOfSides, sizeOfSides);
            tail[0].BackColor = Color.Transparent;
            tail[0].BackgroundImage = Properties.Resources.head_right;
            tail[0].BackgroundImageLayout = ImageLayout.Zoom;

            this.Controls.Add(tail[0]);

            DrawFruit();
            EatFruit();
           

            this.KeyDown += new KeyEventHandler(Direction);
        }

        private void MoveSnake(object sender, EventArgs e)//update
        {
            EndGame();
            EatFruit();
            MoveSnake2();
        }

        private void MoveSnake2()
        {
            for (int i = score; i >= 1; i--)
            {
                tail[i].Location = tail[i - 1].Location;
            }
            tail[0].Location = new Point(tail[0].Location.X + (snakeX * sizeOfSides), tail[0].Location.Y + (snakeY * sizeOfSides));
           EatSnake();
        }

        private void DrawFruit()
        {
            fruit.BackColor = Color.Transparent;
            Random rndX = new Random(); Random rndY = new Random();
            fruitX = rndX.Next(1, 28) * sizeOfSides; fruitY = rndX.Next(1, 18) * sizeOfSides;
            fruit.Location = new Point(fruitX, fruitY);
            fruit.Size = new Size(20, 20);
           // Random 

            fruit.BackgroundImage = Properties.Resources.apple;
            fruit.BackgroundImageLayout = ImageLayout.Zoom;
            this.Controls.Add(fruit);
        }

        void EatFruit()
        {
            if (tail[0].Location.X == fruitX && tail[0].Location.Y == fruitY)
            {
                score += 1;
                label1.Text = "Score: " + score;
                tail[score] = new PictureBox();
                tail[score].Location = new Point(tail[score - 1].Location.X - 20 * snakeX, tail[score - 1].Location.Y - 20 * snakeY);
                tail[score].Size = new Size(sizeOfSides, sizeOfSides);
                tail[score].BackColor = Color.Transparent;
                tail[score].BackgroundImage = Properties.Resources.boby1;
                tail[score].BackgroundImageLayout = ImageLayout.Zoom;
                this.Controls.Add(tail[score]);
                DrawFruit();
                if (score % 3 == 0){
                    if (time.Interval > 51) { time.Interval -= 50; }
                }
            }
        }

        private void EatSnake()
        {
            for (int i = 2; i < score; i++)
            {
                if (tail[0].Location == tail[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(tail[j]);
                    score = score - (score - i + 1);
                    label1.Text = "Score: " + score;
                }
            }
        }

        private void GameOverForm()
        {
            Hide();
            Form5 form5 = new Form5();
            form5.Size = new Size(450, 350);
            form5.StartPosition = FormStartPosition.CenterScreen;
            form5.ShowDialog();
        }

        private void WriteCurrentScore(int score)
        {
            string path = "Properties.Resources.CurrentScore";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(score);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DrawMap(int startPointWidth, int startPointHeigh)
        {
            //horizontal
            PictureBox widthBorder = new PictureBox();
            widthBorder.BackColor = Color.SandyBrown;
            widthBorder.Size = new Size(600, 20);
            widthBorder.Location = new Point(1, startPointHeigh);
            this.Controls.Add(widthBorder);

            //vertical
            PictureBox heighBorder = new PictureBox();
            heighBorder.BackColor = Color.SandyBrown;
            heighBorder.Size = new Size(20, 400);
            heighBorder.Location = new Point(startPointWidth, 1);
            this.Controls.Add(heighBorder);
        }

        private void EndGame()
        {
            if(tail[0].Location.X <  sizeOfSides-5)
            {
                time.Stop();
                WriteCurrentScore(score);
                GameOverForm(); 
            }
            if(tail[0].Location.X>= width - sizeOfSides+20)
            {
                time.Stop();
                WriteCurrentScore(score);
                GameOverForm();
            }
            if(tail[0].Location.Y< sizeOfSides - 20)
            {
                time.Stop();
                WriteCurrentScore(score);
                GameOverForm();
            }
            if(tail[0].Location.Y>= heigh - sizeOfSides+10)
            {
                time.Stop();
                WriteCurrentScore(score);
                GameOverForm();
            }
        }

        private void Direction(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        tail[0].BackgroundImage = Properties.Resources.head_up;
                        tail[0].BackgroundImageLayout = ImageLayout.Zoom;
                        snakeY = -1;
                        snakeX = 0;
                        //tail[0].BackgroundImageChanged();
                        break;
                    }
                case Keys.Right:
                    {
                        tail[0].BackgroundImage = Properties.Resources.head_right;
                        tail[0].BackgroundImageLayout = ImageLayout.Zoom;
                        snakeY = 0;
                        snakeX = 1;
                        break;
                    }
                case Keys.Left:
                    {
                        tail[0].BackgroundImage = Properties.Resources.head_left;
                        tail[0].BackgroundImageLayout = ImageLayout.Zoom;
                        snakeY = 0;
                        snakeX = -1;
                        break;
                    }
                case Keys.Down:
                    {
                        tail[0].BackgroundImage = Properties.Resources.head_down;
                        tail[0].BackgroundImageLayout = ImageLayout.Zoom;
                        snakeY = 1;
                        snakeX = 0;
                        break;
                    }
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
                case Keys.Back:
                    {
                        Hide();
                        Form1 form1 = new Form1();
                        form1.Size = new Size(615, 440);
                        form1.StartPosition = FormStartPosition.CenterScreen;
                        form1.ShowDialog();
                        break;
                    }
            }
        }
    }
}
