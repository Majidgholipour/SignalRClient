using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ConnectionResult
    {
        public IHubProxy proxy { get; set; }
        public string Messages { get; set; }
        public ConnectionState connectionState { get; set; }
    }
    public class ConnectionRepo
    {
        public ConnectionResult AddConnectionResult(IHubProxy proxy, ConnectionState state, string Messages)
        {
            ConnectionResult connection = new ConnectionResult()
            {
                proxy = proxy,
                connectionState = state,
                Messages = Messages

            };
            return connection;
        }
    }
}
