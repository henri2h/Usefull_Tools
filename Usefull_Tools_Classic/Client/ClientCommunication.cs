using Shared;
using Shared.Client.Errors;
using System.IO;
using System.Net.Sockets;

namespace TCPCommunication.Client
{
    public class ClientCommunication : CommunicationTemplate
    {
        public TcpClient client;

        public int ReceiveTimeout = 500;

        public string Available;
        public bool AutoFlush { get { return sw.AutoFlush; } set { sw.AutoFlush = value; } }
        public bool readConnectionResponse = true;

        // because Network stream didn't exist in universal apps
        NetworkStream s;
        public ClientCommunication(string ip, int port = 2055)
        {
            client = new TcpClient(ip, port);

            //    client.ReceiveTimeout = 500;
            s = client.GetStream();
            sr = new StreamReader(s);
            sw = new StreamWriter(s);

            sw.AutoFlush = true;


            //only if we connect to a server wich respond "@OK" when the client connect
            if (readConnectionResponse)
            {
                string inputOk = readLine();
                if (inputOk != "@OK")
                {
                    System.Diagnostics.Debug.WriteLine("Connection error");
                    throw new ConnectionError(ConnectionErrorReason.InvalidResponse);

                }
                else { System.Diagnostics.Debug.WriteLine("Connected"); }
            }
        }

       

        public void Close()
        {
            s.Close();
            client.Close();
        }

    }

}
