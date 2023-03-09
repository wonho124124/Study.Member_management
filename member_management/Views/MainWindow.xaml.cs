using member_management.ViewModels;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace member_management.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // 마지막 클릭 그리드 뷰 컬럼 헤더
        private GridViewColumnHeader lastClickedGridViewColumnHeader = null;
        // 마지막 리스트 정렬 방향
        private ListSortDirection lastListSortDirection = ListSortDirection.Ascending;


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
            MemberSexComboBox.SelectedIndex = -1;
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


        #region SortList
        private void gridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader clickedGridViewColumnHeader = e.OriginalSource as GridViewColumnHeader;

            ListSortDirection listSortDirection;

            if (clickedGridViewColumnHeader != null)
            {
                if (clickedGridViewColumnHeader.Role != GridViewColumnHeaderRole.Padding)
                {
                    // 마지막으로 클릭한 그리브 뷰 컬럼 헤더가 아닌 경우
                    if (clickedGridViewColumnHeader != this.lastClickedGridViewColumnHeader)
                    {
                        listSortDirection = ListSortDirection.Ascending;
                    }
                    else // 마지막으로 클릭한 그리드 뷰 컬럼 헤더인 경우
                    {
                        if (this.lastListSortDirection == ListSortDirection.Ascending)
                        {
                            listSortDirection = ListSortDirection.Descending;
                        }
                        else
                        {
                            listSortDirection = ListSortDirection.Ascending;
                        }
                    }

                    string header = ((System.Windows.Data.Binding)clickedGridViewColumnHeader.Column.DisplayMemberBinding).Path.Path;
                    // string header = clickedGridViewColumnHeader.Column.Header as string;

                    Sort(header, listSortDirection);

                    this.lastClickedGridViewColumnHeader = clickedGridViewColumnHeader;
                    this.lastListSortDirection = listSortDirection;
                }
            }
        }

        // 정렬하기
        // <param name="header">헤더</param>
        // <param name="listSortDirection">리스트 정렬 방향</param>
        private void Sort(string header, ListSortDirection listSortDirection)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.MemberListView.ItemsSource);
            collectionView.SortDescriptions.Clear();
            SortDescription sortDescription = new SortDescription(header, listSortDirection);
            collectionView.SortDescriptions.Add(sortDescription);
            collectionView.Refresh();
        }
        #endregion
    }
}
