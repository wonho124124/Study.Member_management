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
        string path = "D:\\test\\memberlist.json";

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

        private int _memberAge;
        public int MemberAge
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
            SelectedMember = null;

            ReadMemberList();
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
                        member.MemberAge = int.Parse(jsonMember["나이"].ToString());
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
                    new JProperty("나이", member.MemberAge)
                );
                JsonMemberList.Add(m);
            }
            File.WriteAllText(path, JsonMemberList.ToString());
        }

        private void AddCmd()
        {
            try
            {
                if (!(this.MemberName == null) && !(this.MemberID == null) && !(this.MemberSex == null) && !(this.MemberAge == 0))
                {
                    MemberInfo infos = new MemberInfo();
                    infos.MemberName = this.MemberName;
                    infos.MemberID = this.MemberID;
                    infos.MemberSex = this.MemberSex;
                    infos.MemberAge = Convert.ToInt16(this.MemberAge);

                    MemberInfoList.Add(infos);

                    /*this.MemberName = null;
                    this.MemberID = null;
                    this.MemberSex = null;
                    this.MemberAge = 0;*/
                }
                else { MessageBox.Show("입력 X"); }
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
                /*
                int SelectedIndex = MemberInfoList.IndexOf(SelectedMember);
                SelectedMember.MemberName = this.MemberName;
                SelectedMember.MemberID = this.MemberID;
                SelectedMember.MemberSex = this.MemberSex;
                SelectedMember.MemberAge = this.MemberAge;
                */
                AmendMemberInfoWindow modifyWindow = new AmendMemberInfoWindow(SelectedMember);
                modifyWindow.ShowDialog();
            }
        }

        private async void SaveCmd()
        {
            /*
            using StreamWriter file = new(path);
            foreach (MemberInfo member in MemberInfoList)
            {
                await file.WriteAsync(member.MemberName.ToString() + " ");
                await file.WriteAsync(member.MemberID.ToString() + " ");
                await file.WriteAsync(member.MemberSex.ToString() + " ");
                await file.WriteAsync(member.MemberAge.ToString() + " ");
                await file.WriteAsync("\n");
            }
            */
            JArray JsonMemberList = new JArray();
            foreach (MemberInfo member in MemberInfoList)
            {
                JObject m = new JObject(
                    new JProperty("이름", member.MemberName),
                    new JProperty("ID", member.MemberID),
                    new JProperty("성별", member.MemberSex),
                    new JProperty("나이", member.MemberAge)
                );
                JsonMemberList.Add(m);
            }
            // string stringMemberList = JsonConvert.SerializeObject(JsonMemberList, Formatting.Indented);
            // File.WriteAllText(path, stringMemberList);
            File.WriteAllText(path, JsonMemberList.ToString());
        }
    }
}
