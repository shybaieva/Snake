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
    public partial class Form5 : Form
    {
        private int currentScore, highScore; 

        public Form5()
        {
            InitializeComponent();
            ReadCurrenrScore();
            CompareScores();
        }

        private void ReadCurrenrScore()
        {
            string pathCurrentScore = "Properties.Resources.CurrentScore";
            try
            {
                using (StreamReader sr = new StreamReader(pathCurrentScore))
                {
                    currentScore = int.Parse( sr.ReadToEnd());
                    label2.Text = "Your score: " + currentScore;
                    
                }
                File.WriteAllText(pathCurrentScore, "0");
            }
            catch(Exception e)
            {
                label2.Text = e.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 form2 = new Form2();
            form2.Size = new Size(615, 440);
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            form1.Size = new Size(615, 440);
            form1.StartPosition = FormStartPosition.CenterScreen;
            form1.ShowDialog();
        }

        private void CompareScores()
        {
           string pathHighScore = "Properties.Resources.HighScore" ;
            try
            {
                using (StreamReader sr = new StreamReader(pathHighScore))
                {
                    highScore = int.Parse(sr.ReadToEnd());
                    sr.Close();
                }

            }
            catch (Exception e)
            {
                label3.Text = e.Message;
            }
            if (currentScore > highScore)
            {
                  highScore = currentScore;
                  File.WriteAllText(pathHighScore, "");
                  using(StreamWriter sw = new StreamWriter(pathHighScore))
                  {
                       sw.WriteLine(highScore);
                  }
            }
            label3.Text = "High score: " + highScore;
            
        }
    }
}
