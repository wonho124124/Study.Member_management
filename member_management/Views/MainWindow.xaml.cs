using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace member_management.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBoxClear()
        {
            MemberNameTextBox.Text = null;
            MemberIDTextBox.Text = null;
            MemberSexTextBox.Text = null;
            MemberAgeTextBox.Text = "0";
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxClear();
        }

        private void AmendButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxClear();
        }
    }
}
