using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
// using System.Reflection; ??
using System.Windows.Input;
using System.Windows;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static member_management.Models.MemberInfo;
using member_management.Views;
using member_management.Models;

namespace member_management.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"memberlist.json");

        private string _memberManagementTitle = "회원";
        public string MemberManagementTitle
        {
            get { return _memberManagementTitle; }
            set { _memberManagementTitle = value; }
        }
            
        private ObservableCollection<MemberInfo> _memberInfoList;
        public ObservableCollection<MemberInfo> MemberInfoList
        {
            get { return _memberInfoList; }
            set { SetProperty(ref _memberInfoList, value); }
        }

        private MemberInfo _selectedMember;
        public MemberInfo SelectedMember
        {
            get { return _selectedMember; }
            set { SetProperty(ref _selectedMember, value); }
        }


        #region Bind
        private string _memberName;
        public string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; }
        }

        private string _memberID;
        public string MemberID
        {
            get { return _memberID; }
            set { _memberID = value; }
        }

        private string _memberSex;
        public string MemberSex
        {
            get { return _memberSex; }
            set { _memberSex = value; }
        }

        private string _memberAge;
        public string MemberAge
        {
            get { return _memberAge; }
            set { _memberAge = value; }
        }
        #endregion

        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AmendCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion


        public MainWindowViewModel()
        {
            AddCommand = new DelegateCommand(AddCmd);
            DeleteCommand = new DelegateCommand(DeleteCmd);
            AmendCommand = new DelegateCommand(AmendCmd);
            SaveCommand = new DelegateCommand(SaveCmd);
            CloseCommand = new DelegateCommand(CloseCmd);

            MemberInfoList = new ObservableCollection<MemberInfo>();

            ReadMemberList();
            //if(!File.Exists(path))
            //{
            //    File.Create(path);
            //}
        }

        private void ReadMemberList()
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JArray jsonMembers = (JArray)JToken.ReadFrom(reader);

                    foreach (JObject jsonMember in jsonMembers)
                    {
                        MemberInfo member = new MemberInfo();
                        member.MemberName = jsonMember["이름"].ToString();
                        member.MemberID = jsonMember["ID"].ToString();
                        member.MemberSex = jsonMember["성별"].ToString();
                        member.MemberAge = jsonMember["나이"].ToString();
                        MemberInfoList.Add(member);
                    }
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }
        }

        private void CloseCmd()
        {
            JArray JsonMemberList = new JArray();
            foreach (MemberInfo member in MemberInfoList)
            {
                JObject m = new JObject(
                    new JProperty("이름", member.MemberName),
                    new JProperty("ID", member.MemberID),
                    new JProperty("성별", member.MemberSex),
                    new JProperty("나이", Convert.ToInt16(member.MemberAge))
                );
                JsonMemberList.Add(m);
            }
            File.WriteAllText(path, JsonMemberList.ToString());
        }

        private void AddCmd()
        {
            try
            {
                if(MemberName == null) { MessageBox.Show("이름을 입력하세요."); }
                else if(MemberID == null) { MessageBox.Show("ID를 입력하세요."); }
                else if(MemberSex == null) { MessageBox.Show("성별을 입력하세요."); }
                else if(MemberAge == null) { MessageBox.Show("나이를 입력하세요."); }
                else
                {
                    MemberInfo infos = new MemberInfo();
                    infos.MemberName = this.MemberName;
                    infos.MemberID = this.MemberID;
                    infos.MemberSex = this.MemberSex;
                    infos.MemberAge = this.MemberAge;

                    MemberInfoList.Add(infos);
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }
        }

        private void DeleteCmd()
        {
            if (MemberInfoList.Count == 0 || SelectedMember == null) { return; }
            else
            {
                int SelectedIndex = MemberInfoList.IndexOf(SelectedMember);
                MemberInfoList.RemoveAt(SelectedIndex);
            }
            SelectedMember = null;
        }

        private void AmendCmd()
        {
            if (MemberInfoList.Count == 0 || SelectedMember == null) { return; }
            else
            {
                AmendMemberInfoWindow modifyWindow = new AmendMemberInfoWindow(SelectedMember);
                modifyWindow.ShowDialog();
            }
        }

        private async void SaveCmd()
        {
            JArray JsonMemberList = new JArray();
            foreach (MemberInfo member in MemberInfoList)
            {
                JObject m = new JObject(
                    new JProperty("이름", member.MemberName),
                    new JProperty("ID", member.MemberID),
                    new JProperty("성별", member.MemberSex),
                    new JProperty("나이", Convert.ToInt16(member.MemberAge))
                );
                JsonMemberList.Add(m);
            }
            // string stringMemberList = JsonConvert.SerializeObject(JsonMemberList, Formatting.Indented);
            // File.WriteAllText(path, stringMemberList);
            File.WriteAllText(path, JsonMemberList.ToString());
        }
    }
}
