using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace MauiApp1
{
    public partial class ToDoList : ContentPage
    {
        List<TaskItem> tasks;

        public ToDoList()
        {
            InitializeComponent();

            tasks = new List<TaskItem>();

            ToDoListView.ItemsSource = tasks;
        }

        private void AddNewItemButton_Clicked(object sender, EventArgs e)
        {
            tasks.Add(new TaskItem { TaskName = "Yeni G�rev" });
            ToDoListView.ItemsSource = null;
            ToDoListView.ItemsSource = tasks;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string jsonTasks = JsonSerializer.Serialize(tasks);
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");
                await File.WriteAllTextAsync(filePath, jsonTasks);

                await DisplayAlert("Bilgi", "Dosya ba�ar�yla kaydedildi.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Dosya kaydedilirken bir hata olu�tu: {ex.Message}", "OK");
            }
        }

        public class TaskItem
        {
            public string TaskName { get; set; }
            public bool IsCompleted { get; set; }
        }

        private async void EditItem_Tapped(object sender, EventArgs e)
        {
            var image = (Image)sender;
            var task = (TaskItem)image.BindingContext;

            string newTaskName = await DisplayPromptAsync("D�zenleme", "Yeni g�rev ad�:", initialValue: task.TaskName);

            if (!string.IsNullOrEmpty(newTaskName))
            {
                task.TaskName = newTaskName;
                ToDoListView.ItemsSource = null;
                ToDoListView.ItemsSource = tasks;
            }
        }

        private async void DeleteItem_Tapped(object sender, EventArgs e)
        {
            var image = (Image)sender;
            var task = (TaskItem)image.BindingContext;

            bool answer = await DisplayAlert("Silme ��lemi", "Bu g�revi silmek istedi�inize emin misiniz?", "Evet", "Hay�r");

            if (answer)
            {
                tasks.Remove(task);
                ToDoListView.ItemsSource = null;
                ToDoListView.ItemsSource = tasks;
            }
        }
    }
}
