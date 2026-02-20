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
    /// Логика взаимодействия для AgentInputDialog.xaml
    /// </summary>
    public partial class AgentInputDialog : Window
    {
        public Agent Agent { get; private set; }

        public AgentInputDialog(Agent existingAgent = null)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;

            if (existingAgent != null)
            {
                Title = "Редактирование агента";
                Agent = existingAgent;
                tbLastName.Text = existingAgent.LastName;
                tbFirstName.Text = existingAgent.FirstName;
                tbMiddleName.Text = existingAgent.MiddleName;
                tbDealShare.Text = existingAgent.DealShare?.ToString();
            }
            else
            {
                Title = "Новый агент";
                Agent = new Agent();
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

            Agent.LastName = tbLastName.Text.Trim();
            Agent.FirstName = tbFirstName.Text.Trim();
            Agent.MiddleName = string.IsNullOrWhiteSpace(tbMiddleName.Text) ? null : tbMiddleName.Text.Trim();

            if (byte.TryParse(tbDealShare.Text, out byte share) && share <= 100)
                Agent.DealShare = share;
            else if (!string.IsNullOrWhiteSpace(tbDealShare.Text))
            {
                MessageBox.Show("Доля сделок должна быть числом от 0 до 100!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
                Agent.DealShare = null;

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

       
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}