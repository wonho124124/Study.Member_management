using member_management.ViewModels;
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
        MainWindowViewModel viewModel = null;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = this.DataContext as MainWindowViewModel;
        }
        private void TextBoxClear()
        {
            MemberNameTextBox.Text = null;
            MemberIDTextBox.Text = null;
            // MemberSexTextBox.Text = null;
            MemberAgeTextBox.Text = null;
        }
        private void InputFilterNum(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // 숫자만 입력
            e.Handled = regex.IsMatch(e.Text);
        }

        private void InputFilterText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+"); // 숫자 입력 불가
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.AddCmd())
            {
                TextBoxClear();
            }
        }

        #region TextBox_Focus

        private void MemberNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MemberNameHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void MemberNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MemberNameTextBox.Text))
            {
                MemberNameHintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MemberNameHintTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void MemberIDTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MemberIDHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void MemberIDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MemberIDTextBox.Text))
            {
                MemberIDHintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MemberIDHintTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        /*private void MemberSexTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MemberSexHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void MemberSexTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MemberSexTextBox.Text))
            {
                MemberSexHintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MemberSexHintTextBlock.Visibility = Visibility.Collapsed;
            }
        }*/

        private void MemberAgeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MemberAgeHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void MemberAgeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MemberAgeTextBox.Text))
            {
                MemberAgeHintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MemberAgeHintTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}
