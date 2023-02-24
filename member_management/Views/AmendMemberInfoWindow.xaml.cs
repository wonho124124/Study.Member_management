using member_management.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using member_management.Models;

namespace member_management.Views
{
    /// <summary>
    /// Interaction logic for AmendMemberInfoWindow.xaml
    /// </summary>
    public partial class AmendMemberInfoWindow : Window
    {
        MemberInfo SelectedMember = new MemberInfo();
        AmendMemberInfoWindowViewModel _vmAmend = null;
        private void TextBoxClear()
        {
            MemberNameTextBox.Text = null;
            MemberIDTextBox.Text = null;
            MemberSexTextBox.Text = null;
            MemberAgeTextBox.Text = "0";
        }

        public AmendMemberInfoWindow(MemberInfo member)
        {
            InitializeComponent();
            _vmAmend = this.DataContext as AmendMemberInfoWindowViewModel;
            _vmAmend.OriginalInfo = member;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AmendInfo_Click(object sender, RoutedEventArgs e)
        {
            TextBoxClear();
        }
    }
}
