using member_management.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace member_management.Events
{
    public class AmendSuccessEvent : PubSubEvent<AmendSuccessEventHandler>
    {
    }
    public class AmendSuccessEventHandler
    {
        public MemberInfo AmendInfo { get; set; }
    }
}
