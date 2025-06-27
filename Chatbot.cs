using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoePart3PROG6221_ST10456604
{
    public partial class Chatbot : Form
    {
        public Chatbot()
        {
            InitializeComponent();
        }

        // This method gets called whenever the text in richTextBox1 changes
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Example: Automatically scroll to the bottom when new text is added
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

            // Optional: You could update a label to show number of characters
            // e.g., lblCharCount.Text = $"Character count: {richTextBox1.Text.Length}";

            // Or, log changes for debugging
            // Console.WriteLine($"RichTextBox content changed. Length: {richTextBox1.Text.Length}");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add your code here if needed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Example: Clear the chat history
            richTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Placeholder for sending message logic
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: handle cell clicks
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Placeholder for additional feature
        }
    }
}