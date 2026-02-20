using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ModelsDB;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDBInListView();
            LoadDBInListView1();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            return;
        }
        void LoadDBInListView()
        {

            using (User37Context _db = new User37Context())
            {

                int selectedIndex = listView1.SelectedIndex;

                _db.Agents.Load();
                listView1.ItemsSource = _db.Agents.ToList();

                if (selectedIndex != -1)
                {
                    if (selectedIndex >= listView1.Items.Count)
                        selectedIndex--;


                    listView1.SelectedIndex = selectedIndex;


                    listView1.ScrollIntoView(listView1.SelectedItem);
                }

                listView1.Focus();
            }
        }
        void LoadDBInListView1()
        {

            using (User37Context _db = new User37Context())
            {

                int selectedIndex = listView2.SelectedIndex;

                _db.Clients.Load();
                listView2.ItemsSource = _db.Clients.ToList();

                if (selectedIndex != -1)
                {
                    if (selectedIndex >= listView2.Items.Count)
                        selectedIndex--;


                    listView2.SelectedIndex = selectedIndex;


                    listView2.ScrollIntoView(listView2.SelectedItem);
                }

                listView2.Focus();
            }
        }
        private void btnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AgentInputDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var _db = new User37Context())
                    {
                        _db.Agents.Add(dialog.Agent);
                        _db.SaveChanges();
                    }
                    LoadDBInListView();
                    MessageBox.Show(" Агент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnEditAgent_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem is not Agent selectedAgent)
            {
                MessageBox.Show(" Выберите агента для редактирования!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new AgentInputDialog(selectedAgent);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var _db = new User37Context())
                    {
                        var entity = _db.Agents.Find(dialog.Agent.Id);
                        if (entity != null)
                        {
                            entity.FirstName = dialog.Agent.FirstName;
                            entity.MiddleName = dialog.Agent.MiddleName;
                            entity.LastName = dialog.Agent.LastName;
                            entity.DealShare = dialog.Agent.DealShare;
                            _db.SaveChanges();
                        }
                    }
                    LoadDBInListView();
                    MessageBox.Show(" Данные обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem is not Agent selectedAgent)
            {
                MessageBox.Show(" Выберите агента для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $" Удалить агента:\n{selectedAgent.LastName} {selectedAgent.FirstName}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var _db = new User37Context())
                    {
                        var entity = _db.Agents.Find(selectedAgent.Id);
                        if (entity != null)
                        {
                            _db.Agents.Remove(entity);
                            _db.SaveChanges();
                        }
                    }
                    LoadDBInListView(); MessageBox.Show(" Агент удалён!", "Успех",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ClientInputDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var _db = new User37Context())
                    {
                        _db.Clients.Add(dialog.Client);
                        _db.SaveChanges();
                    }
                    LoadDBInListView();
                    MessageBox.Show(" Клиент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {

        }
            }

        }
   

