using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universall_Tools_Test_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Usefull_Tools.Logger.AppName = "Universall_Tools_Test_CLI";

            Usefull_Tools.Logger.logMain("Hello...");
            Usefull_Tools.Logger.logMain("Cool...");

            try
            {
                int te = 0;
                int ze = 7 / te;
            }
            catch (Exception ex)
            {
                Usefull_Tools.Logger.logErrors("Error in Main", ex);
            }

            Usefull_Tools.Logger.logMain("Ended ...");
            Console.ReadLine();
        }
    }
}
