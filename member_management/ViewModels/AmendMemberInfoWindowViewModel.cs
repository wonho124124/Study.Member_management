using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using member_management.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace member_management.ViewModels
{
    public class AmendMemberInfoWindowViewModel : BindableBase
    {
        private string _amendMemberInfoTitle = "회원 정보 수정";
        public string AmendMemberInfotTitle
        {
            get { return _amendMemberInfoTitle; }
            set { _amendMemberInfoTitle = value; }
        }

        #region Bind
        private string _amendName;
        public string AmendName
        {
            get { return _amendName; }
            set { _amendName = value; }
        }
        private string _amendID;
        public string AmendID
        {
            get { return _amendID; }
            set { _amendID = value; }
        }
        private ObservableCollection<string> _amendSexList;
        public ObservableCollection<string> AmendSexList
        {
            get { return _amendSexList; }
            set { _amendSexList = value; }
        }
        private string _amendSex;
        public string AmendSex
        {
            get { return _amendSex; }
            set { _amendSex = value; }
        }
        private string _amendAge;
        public string AmendAge
        {
            get { return _amendAge; }
            set { _amendAge = value; }
        }

        private MemberInfo _originalInfo;
        public MemberInfo OriginalInfo
        {
            get { return _originalInfo; }
            set { _originalInfo = value; }
        }
        #endregion

        //public ICommand SaveCommand { get; set; }

        public AmendMemberInfoWindowViewModel()
        {
            //SaveCommand = new DelegateCommand(SaveCmd);
            AmendSexList = new ObservableCollection<string>()
            {
                new string("남자"),
                new string("여자")
            };
        }

        public bool SaveCmd()
        {
            bool isSaveSuccess = false;
            try
            {
                if (AmendName == null) { MessageBox.Show("이름을 입력하세요."); }
                else if (AmendID == null) { MessageBox.Show("ID를 입력하세요."); }
                else if (AmendSex == null) { MessageBox.Show("성별을 입력하세요."); }
                else if (AmendAge == null) { MessageBox.Show("나이를 입력하세요."); }
                else
                {
                    OriginalInfo.MemberName = AmendName;
                    OriginalInfo.MemberID = AmendID;
                    OriginalInfo.MemberSex = AmendSex;
                    OriginalInfo.MemberAge = AmendAge;
                    isSaveSuccess = true;
                }
                return isSaveSuccess;
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
