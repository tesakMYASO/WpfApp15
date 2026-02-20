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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ClientInputDialog.xaml
    /// </summary>
    public partial class ClientInputDialog : Window
    {
        public Client Client { get; private set; }

        public ClientInputDialog(Client existingClient = null)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;

            if (existingClient != null)
            {
                Title = "Редактирование клиента";
                Client = existingClient;
                tbLastName.Text = existingClient.LastName;
                tbFirstName.Text = existingClient.FirstName;
                tbMiddleName.Text = existingClient.MiddleName;
                tbPhone.Text = existingClient.Phone;
                tbEmail.Text = existingClient.Email;
            }
            else
            {
                Title = "Новый клиент";
                Client = new Client();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
 
            if (string.IsNullOrWhiteSpace(tbLastName.Text) || string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show(" Фамилия и Имя обязательны для заполнения!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (!string.IsNullOrWhiteSpace(tbEmail.Text) && !IsValidEmail(tbEmail.Text))
            {
                MessageBox.Show("Введите корректный адрес электронной почты!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client.LastName = tbLastName.Text.Trim();
            Client.FirstName = tbFirstName.Text.Trim();
            Client.MiddleName = string.IsNullOrWhiteSpace(tbMiddleName.Text) ? null : tbMiddleName.Text.Trim();
            Client.Phone = string.IsNullOrWhiteSpace(tbPhone.Text) ? null : tbPhone.Text.Trim();
            Client.Email = string.IsNullOrWhiteSpace(tbEmail.Text) ? null : tbEmail.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}