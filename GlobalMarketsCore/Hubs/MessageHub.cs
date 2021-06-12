using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.Hubs
{
    public class MessageHub: Hub
    {
        public Task Send(string message)
        {
            
            return Clients.All.InvokeAsync("Send", message);
        }

        public void myFunction()
        {
            Clients.All.clientListener($"welcome to { Context.ConnectionId} </hr>");
        }
    }
}
