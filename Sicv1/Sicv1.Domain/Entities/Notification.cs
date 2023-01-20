using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Entities
{
    public class Tokens
    {
        public int id{ get; set; }
        public string token{ get; set; }
    }

    public class Notification
	{
        public string title { get; set; }
        public string body { get; set; }
        public string typeToggle { get; set; }
        public List<Tokens> data { get; set; }
        public int CREATED_USER { get; set; }
        public string segments { get; set; }
    }
    public class ExpoPush
    {
        public string title { get; set; }
        public string body { get; set; }
        public string priority { get; set; }
        public string sound { get; set; }
        public string channelId { get; set; }
        public bool _displayInForeground { get; set; }
        //public List<string> to { get; set; }
        public string to { get; set; }

        public ExpoPush() {
            title = string.Empty;
            body = string.Empty;
            priority = string.Empty;
            sound = string.Empty;
            channelId = string.Empty;
            _displayInForeground = true;
            to = string.Empty;
            //to = new List<string>();
        }
    }

    public class ExpoPushList
    {
        public List<ExpoPush> turns { get; set; }
    }
}
