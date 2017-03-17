using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usefull_Tools.Network.TCP.Client.Errors
{
    public class ClientError : Exception
    {
        public ClientError(string reason) : base(String.Format("Unexpected error hapend in the client : {0}", reason)) { }
    }
}
