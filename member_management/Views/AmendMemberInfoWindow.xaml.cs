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

        public AmendMemberInfoWindow(MemberInfo member)
        {
            InitializeComponent();
            _vmAmend = this.DataContext as AmendMemberInfoWindowViewModel;
            //SelectedMember = member;
            _vmAmend.OriginalInfo = member;
            

            OriginalInfoView(member);
        }
        public void OriginalInfoView(MemberInfo member)
        {
            MemberNameTextBlock.Text = "이름 : " + member.MemberName;
            MemberIDTextBlock.Text = "ID : " + member.MemberID;
            MemberSexTextBlock.Text = "성별 : " + member.MemberSex;
            MemberAgeTextBlock.Text = "나이 : " + member.MemberAge;

            MemberNameTextBox.Text = member.MemberName;
            MemberIDTextBox.Text = member.MemberID;
            MemberSexTextBox.Text = member.MemberSex;
            MemberAgeTextBox.Text = member.MemberAge;
        }
        private void InputFilterNum(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void InputFilterText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+"); // 숫자 입력 불가
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_vmAmend.SaveCmd())
            {
                this.Close();
            };   
        }
    }
}
