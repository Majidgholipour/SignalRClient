using Client.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Config
{
    public static class Utility
    {


        public static ServerConfig Config
        {
            get
            {
                var xx=JsonConvertor.ReadFromJson(@"..\..\ServerConfiguration.json");
                return xx;
            }
        }
    }
}
