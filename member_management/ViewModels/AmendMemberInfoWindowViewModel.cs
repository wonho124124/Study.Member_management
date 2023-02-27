using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using member_management.Models;

namespace member_management.ViewModels
{
    public class AmendMemberInfoWindowViewModel : BindableBase
    {
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
        private string _amendSex;
        public string AmendSex
        {
            get { return _amendSex; }
            set { _amendSex = value; }
        }
        private int _amendAge;
        public int AmendAge
        {
            get { return _amendAge; }
            set { _amendAge = value; }
        }

        private MemberInfo _originalInfo;
        public MemberInfo OriginalInfo
        {
            get { return _originalInfo; }
            set { SetProperty(ref _originalInfo, value); }
        }

        #endregion

        public ICommand SaveCommand { get; set; }

        public AmendMemberInfoWindowViewModel()
        {
            SaveCommand = new DelegateCommand(SaveCmd);
        }

        private void SaveCmd()
        {
            try
            {
                if (AmendName == null) { MessageBox.Show("이름을 입력하세요."); }
                else if (AmendID == null) { MessageBox.Show("ID를 입력하세요."); }
                else if (AmendSex == null) { MessageBox.Show("성별을 입력하세요."); }
                else if (AmendAge == 0) { MessageBox.Show("나이를 입력하세요."); }
                else
                {
                    OriginalInfo.MemberName = AmendName;
                    OriginalInfo.MemberID = AmendID;
                    OriginalInfo.MemberSex = AmendSex;
                    OriginalInfo.MemberAge = Convert.ToInt16(AmendAge);
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }
        }
    }
}
