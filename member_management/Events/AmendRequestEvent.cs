using member_management.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace member_management.Events
{
    public class AmendRequestEvent : PubSubEvent<AmendRequestEventHandler>
    {
    }
    public class AmendRequestEventHandler
    {
        public MemberInfo OriginalInfo { get; set; }
    }
}
