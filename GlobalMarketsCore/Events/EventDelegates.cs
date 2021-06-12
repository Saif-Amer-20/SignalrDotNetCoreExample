using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.Events
{
    public class EventDelegates
    {
        public delegate void ClientAddedEventHandler(object sender, ClientAddedEventArgs e);
    }
}
