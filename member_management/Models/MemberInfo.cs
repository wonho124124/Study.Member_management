using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace member_management.Models
{
    public class MemberInfo : BindableBase
    {
        private string _memberName;
        public string MemberName
        {
            get { return _memberName; }
            set { SetProperty(ref _memberName, value); }
        }

        private string _memberID;
        public string MemberID
        {
            get { return _memberID; }
            set { SetProperty(ref _memberID, value); }
        }

        private string _memberSex;
        public string MemberSex
        {
            get { return _memberSex; }
            set { SetProperty(ref _memberSex, value); }
        }

        private int _memberAge;
        public int MemberAge
        {
            get { return _memberAge; }
            set { SetProperty(ref _memberAge, value); }
        }
    }
}
