using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Add_Task : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private string pendingTaskTitle = null;
        private string pendingTaskDescription = null;

        public Add_Task()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string userInput = Input.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            AppendChat("User", userInput);
            HandleUserInput(userInput);
            Input.Clear();
        }

        private void HandleUserInput(string inputText)
        {
            if (inputText.StartsWith("Add Task -", StringComparison.OrdinalIgnoreCase))
            {
                // Extract task title after "Add Task -"
                pendingTaskTitle = inputText.Substring(9).Trim(); // Changed from 10 to 9
                pendingTaskDescription = $"Cybersecurity Task: {pendingTaskTitle}";
                AppendChat("Bot", $"Task added with the description \"{pendingTaskDescription}\". Would you like a reminder?");
            }
            else if (pendingTaskTitle != null && inputText.IndexOf("yes", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int days = ExtractDays(inputText);
                DateTime? reminderDate = days > 0 ? DateTime.Now.AddDays(days) : (DateTime?)null;

                tasks.Add(new TaskItem
                {
                    Title = pendingTaskTitle,
                    Description = pendingTaskDescription,
                    ReminderDate = reminderDate,
                });

                AppendChat("Bot", days > 0
                    ? $"Got it! I will remind you in {days} day(s)."
                    : "Task added without a reminder.");

                pendingTaskTitle = null;
                pendingTaskDescription = null;
            }
            else if (inputText.StartsWith("Show tasks", StringComparison.OrdinalIgnoreCase))
            {
                string filter = inputText.Length > 10
                    ? inputText.Substring(10).Trim().ToLower()
                    : "all";

                var filtered = tasks.AsEnumerable();

                if (filter == "completed")
                    filtered = filtered.Where(t => t.Completed);
                else if (filter == "pending")
                    filtered = filtered.Where(t => !t.Completed);

                if (!filtered.Any())
                {
                    AppendChat("Bot", "No task available for the filter");
                    return;
                }

                string Response = "Here are your tasks:\r\n";
                int i = 1;
                foreach (var t in filtered)
                {
                    Response += $"{i++}. {t.Title} - {t.Description}"
                        + (t.ReminderDate.HasValue ? $" (Reminder: {t.ReminderDate.Value:d})" : "")
                        + (t.Completed ? " [Completed]" : "")
                        + "\r\n";
                }
                AppendChat("Bot", Response);
            }
            else if (inputText.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
            {
                var parts = inputText.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts.Last(), out int d) && d >= 1 && d <= tasks.Count)
                {
                    tasks.RemoveAt(d - 1);
                    AppendChat("Bot", $"Task {d} deleted");
                }
                else
                {
                    AppendChat("Bot", "Invalid task number");
                }
            }
            else if (inputText.StartsWith("Complete", StringComparison.OrdinalIgnoreCase))
            {
                var parts = inputText.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts.Last(), out int c) && c >= 1 && c <= tasks.Count)
                {
                    tasks[c - 1].Completed = true;
                    AppendChat("Bot", $"Task {c} marked as completed");
                }
                else
                {
                    AppendChat("Bot", "Invalid task number");
                }
            }
            else if (inputText.Equals("Help", StringComparison.OrdinalIgnoreCase))
            {
                AppendChat("Bot", "Here are the commands you can use:\r\n" +
                    "- Add task - [task name]\r\n" +
                    "- Yes [optional: In X days]\r\n" +
                    "- Show tasks [all|completed|pending]\r\n" +
                    "- Delete [task number]\r\n" +
                    "- Complete [task number]");
            }
            else
            {
                AppendChat("Bot", "Sorry, I didn't understand that. Try 'Add task -', 'Show tasks', 'Delete 1', or 'Complete 1'.");
            }
        }

        private int ExtractDays(string inputText)
        {
            var words = inputText.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (int.TryParse(words[i], out int n) && words[i + 1].ToLower().Contains("day"))
                {
                    return n;
                }
            }
            return 0;
        }

        private void AppendChat(string sender, string message)
        {
            txtChat.AppendText($"{sender}: {message}\r\n");
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool Completed { get; set; }
    }
}