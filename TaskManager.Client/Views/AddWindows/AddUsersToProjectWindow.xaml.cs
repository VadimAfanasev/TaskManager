using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.Client.ViewModels;
using TaskManager.Common.Models;

namespace TaskManager.Client.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddUsersToProjectWindow.xaml
    /// </summary>
    public partial class AddUsersToProjectWindow : Window
    {
        public AddUsersToProjectWindow()
        {
            InitializeComponent();
        }
        public void LisListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (ProjectsPageViewModel)DataContext;

            foreach (UserModel user in e.RemovedItems)
                viewModel.SelectedUsersForProject.Remove(user);

            foreach (UserModel user in e.AddedItems)
                viewModel.SelectedUsersForProject.Add(user);
        }
    }
}
