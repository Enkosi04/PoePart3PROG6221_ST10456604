using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PoePart3PROG6221_ST10456604
{
    public class ChatbotForm : Form
    {
        // Controls
        private RichTextBox richTextBox1;
        private TextBox txtUserInput;
        private Button btnSend;
        private Button btnTasks;
        private Button btnQuiz;
        private DataGridView dgvTasks;

        // Log of actions for summary
        private List<string> actionsLog = new List<string>();

        public ChatbotForm()
        {
            // Form properties
            this.Text = "Chatbot Application";
            this.Size = new System.Drawing.Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeControls();
        }

        private void InitializeControls()
        {
            // RichTextBox for chat log
            richTextBox1 = new RichTextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(460, 200),
                ReadOnly = true
            };
            this.Controls.Add(richTextBox1);

            // TextBox for user input
            txtUserInput = new TextBox
            {
                Location = new System.Drawing.Point(10, 220),
                Size = new System.Drawing.Size(370, 30)
            };
            this.Controls.Add(txtUserInput);

            // Send button
            btnSend = new Button
            {
                Text = "Send",
                Location = new System.Drawing.Point(390, 220),
                Size = new System.Drawing.Size(80, 30)
            };
            btnSend.Click += BtnSend_Click;
            this.Controls.Add(btnSend);

            // Manage Tasks button
            btnTasks = new Button
            {
                Text = "Manage Tasks",
                Location = new System.Drawing.Point(10, 260),
                Size = new System.Drawing.Size(120, 30)
            };
            btnTasks.Click += BtnTasks_Click;
            this.Controls.Add(btnTasks);

            // Start Quiz button
            btnQuiz = new Button
            {
                Text = "Start Quiz",
                Location = new System.Drawing.Point(140, 260),
                Size = new System.Drawing.Size(120, 30)
            };
            btnQuiz.Click += BtnQuiz_Click;
            this.Controls.Add(btnQuiz);

            // DataGridView for tasks
            dgvTasks = new DataGridView
            {
                Location = new System.Drawing.Point(10, 300),
                Size = new System.Drawing.Size(460, 250),
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            dgvTasks.Columns.Add("Task", "Task");
            this.Controls.Add(dgvTasks);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
                return;

            AppendChatLog("User: " + userInput);
            string response = GenerateResponse(userInput);
            AppendChatLog("Bot: " + response);

            // Add to actions log
            actionsLog.Add($"User: {userInput}");
            actionsLog.Add($"Bot: {response}");

            txtUserInput.Clear();


            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void AppendChatLog(string message)
        {
            richTextBox1.AppendText(message + Environment.NewLine);
        }

        private string GenerateResponse(string input)
        {
            string lower = input.ToLower();

            // Reminder detection
            if (lower.Contains("remind") || lower.Contains("reminder") || lower.Contains("alert"))
            {
                string actionDetail = ExtractActionDetails(input, new string[] { "to", "that" });
                if (string.IsNullOrEmpty(actionDetail))
                    actionDetail = "your task";

                string reminderDate = DateTime.Now.AddDays(1).ToShortDateString();
                string actionStr = $"Reminder: '{CapitalizeFirstLetter(actionDetail)}' on {reminderDate}";
                actionsLog.Add(actionStr);
                return $"Reminder set for '{CapitalizeFirstLetter(actionDetail)}' on {reminderDate}.";
            }

            // Add task detection
            if (lower.Contains("task") || lower.Contains("add") || lower.Contains("create"))
            {
                string taskDetail = ExtractActionDetails(input, new string[] { "to", "for", "about" });
                if (string.IsNullOrEmpty(taskDetail))
                    taskDetail = "your task";

                AddTask(taskDetail);
                actionsLog.Add($"Task: '{CapitalizeFirstLetter(taskDetail)}'");
                return $"Task added: '{CapitalizeFirstLetter(taskDetail)}'. Would you like to set a reminder for this task?";
            }

            // Quiz detection
            if (lower.Contains("quiz") || lower.Contains("question"))
            {
                return "Starting a quiz... (not implemented yet)";
            }

            // Security questions
            if (lower.Contains("password"))
            {
                return "It seems you're asking about passwords. Remember to keep them secure!";
            }
            if (lower.Contains("phishing"))
            {
                return "Be cautious of phishing attempts. Always verify the source.";
            }

            // Summary request
            if (lower.Contains("what have you done") || lower.Contains("summary"))
            {
                return GetActionsSummary();
            }

            return "I'm sorry, I didn't understand that. Could you please rephrase?";
        }

        private string ExtractActionDetails(string input, string[] triggerWords)
        {
            foreach (var word in triggerWords)
            {
                string pattern = $"\\b{word}\\b\\s*(.*)";
                var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string detail = match.Groups[1].Value.Trim();
                    detail = Regex.Replace(detail, @"^(please|the|a|an|my|your)\b", "", RegexOptions.IgnoreCase).Trim();
                    return detail;
                }
            }
            return string.Empty;
        }

        private void AddTask(string task)
        {
            dgvTasks.Rows.Add(task);
        }

        private string GetActionsSummary()
        {
            if (actionsLog.Count == 0)
                return "No actions performed yet.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Actions Summary:");
            for (int i = 0; i < actionsLog.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {actionsLog[i]}");
            }
            return sb.ToString();
        }

        private string CapitalizeFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        private void BtnTasks_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetTasks(), "Tasks List");
        }

        private void BtnQuiz_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quiz feature coming soon!", "Quiz");
        }


        private string GetTasks()
        {
            if (dgvTasks.Rows.Count == 0)
                return "No tasks available.";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current Tasks:");
            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                if (row.IsNewRow) continue;
                var taskCell = row.Cells["Task"].Value;
                if (taskCell != null)
                    sb.AppendLine("- " + taskCell.ToString());
            }
            return sb.ToString();
        }

    }

}