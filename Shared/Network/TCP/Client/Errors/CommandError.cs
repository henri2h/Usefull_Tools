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
    public class CommandError : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        public CommandError(CommandErrors reason) : base(String.Format("Unexpected error hapend in the client : {0}", reason)) { }
        /// <summary>
        /// 
        /// </summary>
        public enum CommandErrors
        {
            /// <summary>
            /// 
            /// </summary>
            NotACommand,
            /// <summary>
            /// 
            /// </summary>
            IsNull
        }
    }
}
