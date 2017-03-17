using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usefull_Tools.Network.TCP.Client.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionError : System.Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        public ConnectionError(ConnectionErrorReason reason) : base(String.Format("Unexpected error hapend in TCP Client Communication : {0}", reason)) { }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum ConnectionErrorReason
    {
        /// <summary>
        /// 
        /// </summary>
        ServerNotResponding,
        /// <summary>
        /// 
        /// </summary>
        ConnectionRefused,
        /// <summary>
        /// 
        /// </summary>
        InvalidResponse
    }
}
