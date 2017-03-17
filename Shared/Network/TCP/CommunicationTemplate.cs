using System;
using System.IO;
using System.Text;
using Usefull_Tools.Network.TCP.Client.Errors;

namespace Usefull_Tools.Network.TCP
{
    public class CommunicationTemplate
    {

        public StreamReader sr;
        public StreamWriter sw;

        public void writeLine(string text) { sw.WriteLine(text); }



        public void writeCommand(string text) { sw.WriteLine("@" + text); }

        public void write(string text)
        {
            sw.WriteLine(text);
        }

        public string readLine() { return sr.ReadLine(); }
        public string readCommand()
        {
            string command = this.readLine();
            if (command != null)
            {
                if (command[0] == '@') { return command.Remove(0, 1); }
                else { throw new CommandError(CommandError.CommandErrors.NotACommand); }
            }

            System.Diagnostics.Debug.WriteLine("error on recieving command : " + command);
            return command;
        }
        public string readStringWithLenght()
        {
            UTF8Encoding encod = new UTF8Encoding();
            string msg = encod.GetString(readDataWithLenght());
            return msg;
        }
        public byte[] readDataWithLenght()
        {
            byte[] buffer = ReadNBytes(4);
            int msgLenght = BitConverter.ToInt32(buffer, 0);
            buffer = ReadNBytes(msgLenght);
            return buffer;
        }

        public byte[] ReadNBytes(int lenght)
        {
            byte[] buffer = new byte[lenght];
            int bytesRead = 0;
            int chunk;
            while (bytesRead < lenght)
            {

                chunk = sr.BaseStream.Read(buffer, bytesRead, buffer.Length - bytesRead);
                if (chunk == 0) { throw new Exception("Unespected disconnect"); }
                bytesRead += chunk;
            }
            return buffer;
        }
        public void writeDataWithLenght(byte[] data)
        {
            byte[] intBytes = BitConverter.GetBytes(data.Length);
            if (intBytes.Length != 4) { System.Diagnostics.Debug.WriteLine("Size : " + intBytes.Length); }
            sw.BaseStream.Write(intBytes, 0, intBytes.Length);
            sw.BaseStream.Write(data, 0, data.Length);
            sw.BaseStream.Flush();
        }
        public void writeStringWithLenght(string message)
        {
            UTF8Encoding encod = new UTF8Encoding();
            writeDataWithLenght(encod.GetBytes(message));
        }

    }
}
