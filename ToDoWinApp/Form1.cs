using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace ToDoWinApp
{
    public partial class Form1 : Form
    {
        private List<ToDoItem> tasks = new List<ToDoItem>();
        private string filePath = "tasks.json";

        public Form1()
        {
            InitializeComponent();
            LoadTasks();
            UpdateList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTask.Text))
            {
                tasks.Add(new ToDoItem { Title = txtTask.Text, IsCompleted = false });
                SaveTasks();
                UpdateList();
                txtTask.Clear();
                lblStatus.Text = "Task Added!";
            }
            else
            {
                lblStatus.Text = "Enter a task!";
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (listTasks.SelectedIndex != -1)
            {
                tasks[listTasks.SelectedIndex].IsCompleted = true;
                SaveTasks();
                UpdateList();
                lblStatus.Text = "Task Completed!";
            }
            else
            {
                lblStatus.Text = "Select a task!";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listTasks.SelectedIndex != -1)
            {
                tasks.RemoveAt(listTasks.SelectedIndex);
                SaveTasks();
                UpdateList();
                lblStatus.Text = "Task Deleted!";
            }
            else
            {
                lblStatus.Text = "Select a task!";
            }
        }

        private void UpdateList()
        {
            listTasks.Items.Clear();
            foreach (var task in tasks)
            {
                string displayText = task.IsCompleted ? $"✔️ {task.Title ?? "Untitled"}" : (task.Title ?? "Untitled");
                listTasks.Items.Add(displayText);
            }
        }

        private void SaveTasks()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks, options));
        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                tasks = JsonSerializer.Deserialize<List<ToDoItem>>(File.ReadAllText(filePath));
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }
    }

    public class ToDoItem
    {
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
