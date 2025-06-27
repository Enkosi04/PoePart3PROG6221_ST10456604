using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace WindowsFormsApp1
{
    public partial class Play : Form
    {
        private int currentQuestion = 0;
        private int score = 0;

        private List<Question> questions;
        public Play()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }
        private void LoadQuestions()
        {
            questions = new List<Question>
            {
            new Question("What is phishing?",
                new string[]  {"Using fake emails or messages ", "To trick people ", "revealing sensitive information","Which is personal"}, 2 ),

           new Question("What is a strong Password?",
               new string[] { "Unique complex", "Combination of Characters", "numbers", "and symbols" },   1),

           new Question("Why is the two factor aunthentication (2FA) important",
               new string[] {"Adds", "an extra security layer ", "with a second verification form","to make things safer" },0),

           new Question("What is malware?",
                new string[]  {"Software", "designed to harm", "exploit computer systens  ", "viruses,Trojans, ransomware" }, 1 ),

           new Question("How to protect device from malware?",
                new string[]  {"Phishing", "Using fake emails or messages ", "To trick people ", "revealing sensitive information" }, 3 ),

          new Question("What is VPN?",
                new string[]  {"Install anti-virus software", "avoid suspicious downloads", " And update", "OS regularly " }, 2 ),

          new Question("Why should you be catious with public wifi?",
                new string[]  {"Public wifi network", "unsecured network", "that make it easier for hackers", "to intercept data "  }, 1 ),

          new Question("What is social engineering?",
                new string[]  {"A type of attack", "that manipulates people", "into revealing sensitive information ", "or performing certain action" }, 0 ),

          new Question("How often should you update your software?",
                new string[]  {"Regular updates" ,"Are crucial", "As they often include", "security patches and bug fixes " }, 1 ),

         new Question("What should you do if you suspect cyber attack ?",
                new string[]  {"Disconnect from the internet", "Report the incident to relevant authorities ", "And seek professional help ","Dont delete any files it will help" }, 1 ),



};

        }
        private void DisplayQuestion()
        {
            if (currentQuestion >= questions.Count)
            {
                MessageBox.Show($"Quiz Completed!\nYour Score: {score}/{questions.Count}", "Result");
                return;
            }

            var q = questions[currentQuestion];
            label1.Text = q.Text;

            radioButton1.Text = q.Options[0];
            radioButton2.Text = q.Options[1];
            radioButton3.Text = q.Options[2];
            radioButton4.Text = q.Options[3];

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selected = -1;
            if (radioButton1.Checked) selected = 0;
            else if (radioButton2.Checked) selected = 1;
            else if (radioButton3.Checked) selected = 2;
            else if (radioButton4.Checked) selected = 3;

            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;

            }
            if (selected == questions[currentQuestion].CorrectIndex)
            {
                score++;
            }
            currentQuestion++;
            DisplayQuestion();
        }
    }

    public class Question
    {
        public string Text;
        public string[] Options;
        public int CorrectIndex;

        public Question(string text, string[] options, int correctIndex)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
        }


        private void btnNext_Click_1(Object sender, EventArgs e)
        {

        }
    }
}