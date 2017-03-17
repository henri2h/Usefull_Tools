using System;
using System.IO;
using System.Text;

namespace Usefull_Tools
{
    /// <summary>
    /// Manage errors
    /// </summary>
    public class ErrorHandeler
    {
        
        /// <summary>
        /// Print and save the details of an error
        /// </summary>
        /// <param name="ex"></param>
        public static void printOut(Exception ex)
        {
            string err = getErrorString(ex);
            Console.WriteLine("[Errors : " + DateTime.Now.ToString() + "] : " + err);
            string tempFile = Files.getTempFile(".err");

            File.AppendAllText(tempFile, err);
        }
        
        //  files.saveFile(".err", err + Environment.NewLine + tempFile);}
        /// <summary>
        /// Return the string representation of an error
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string getErrorString(Exception ex)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("================");
            output.AppendLine(" New error : ");
            output.AppendLine("================");
            output.AppendLine("Date : " + DateTime.Now.ToLongTimeString());
            output.AppendLine("Message : " + ex.Message);
            output.AppendLine("Inner exception : " + ex.InnerException);
            output.AppendLine("Source : " + ex.Source);
            output.AppendLine("Data : " + ex.Data);
            output.AppendLine("Stack Trace : " + ex.StackTrace);
            output.AppendLine();
            return output.ToString();
        }

        /// <summary>
        /// Just save the content of an error
        /// </summary>
        /// <param name="ex"></param>
        public static void saveOut(Exception ex)
        {
            //just to save without printing the error
            string err = getErrorString(ex);
            string tempFile = Files.getTempFile(".err");

            File.AppendAllText(tempFile, err);
            //  files.saveFile(".err", err + Environment.NewLine + tempFile);
        }
    }
}
