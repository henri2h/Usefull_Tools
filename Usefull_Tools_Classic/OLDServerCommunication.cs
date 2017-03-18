using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPCommunicationOLD
{
    public abstract class ServerCommunication
    {
        public int totalNumber = 0;
        public int numb = 0;

        public TcpListener listener;
        const int LIMIT = 5; //5 concurrent clients
        public int port = 2060;

        public NetworkStream s;
        public StreamReader sr;
        public StreamWriter sw;

        public void startService(int limit = LIMIT)
        {

            string localComputerName = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress input in localIPs)
            {
                Console.WriteLine(input.ToString());
            }
            listener = TcpListener.Create(port);
            listener.Start();

            for (int i = 0; i < LIMIT; i++)
            {
                Thread t = new Thread(new ThreadStart(Service));
                t.Start();
                Console.WriteLine(t.Name + " " + t.ThreadState);
                totalNumber++;
            }
        }








    }
    public abstract class ServerClient
    {
        public void Service()
        {
            try
            {
                Socket soc = listener.AcceptSocket();
                soc.NoDelay = true;

                // creating the streams
                s = new NetworkStream(soc);
                sr = new StreamReader(s, Encoding.UTF8);
                sw = new StreamWriter(s, Encoding.UTF8);

                sw.AutoFlush = true; // enable automatic flushing


                // start to read the client commands
                display("Numb : " + numb.ToString());
                display("Total number : " + totalNumber.ToString());

                annalize();

                s.Close();
                soc.Close();

                // this is just to call a method to display a message to the client
                // it can be overdiden
                exit();
            }
            catch (Exception ex)
            {
                logError(ex);
            }
        }

        // to override
        public abstract void display(string text);
        public abstract void annalize();
        public abstract void exit();

        void logError(Exception ex)
        {
            bool save = false;
            string dir = System.Environment.CurrentDirectory + "\\error.txt";

            while (!save)
            {
                int numb = 0;
                if (File.Exists(dir))
                {
                    numb++;
                    dir = System.Environment.CurrentDirectory + "\\error" + numb.ToString() + ".txt";
                }
                else
                {
                    StringBuilder output = new StringBuilder();
                    output.AppendLine("Message : " + ex.Message);
                    output.AppendLine("Inner exception" + ex.InnerException);
                    output.AppendLine("Source : " + ex.Source);
                    output.AppendLine("target Site : " + ex.TargetSite);
                    output.AppendLine("Stack Trace : " + ex.StackTrace);
                    try
                    {
                        File.WriteAllText(dir, output.ToString());
                        save = true;
                    }
                    catch
                    {
                        File.WriteAllText("C:\\out.txt", "We don't have write acces to write here" + output.ToString());
                        save = true;
                    }
                }
            }
        }

        // read and write commands
        public string readCommand()
        {
            string command = sr.ReadLine();
            if (command != null)
            {
                if (command[0] == '@')
                {
                    return command;
                }
            }

            System.Diagnostics.Debug.WriteLine("error on recieving command : " + command);
            return command;
        }
        public void writeCommand(string value) { sw.WriteLine("@" + value); }

        public string readLine() { return sr.ReadLine(); }
        public void writeLine(string value) { sw.WriteLine(value); }

        public void sendSuccess() { writeCommand("OK"); }
        public void sendError(string error)
        {
            writeCommand("KO");
            writeLine(error);
        }


        // end of the functions
    }
}
