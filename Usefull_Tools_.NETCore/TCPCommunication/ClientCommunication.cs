using Shared.Client.Errors;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace UniversalTCPClientLibrary.TCPCommunication.Client
{
    public class ClientCommunication : Shared.CommunicationTemplate
    {


        public StreamSocket socket;
        public int ReceiveTimeout = 500;


        public string Available;
        public bool readConnectionResponse = true;


        HostName hostName;
        int port;

        public ClientCommunication(string ip, int portIn)
        {
            hostName = new HostName(ip);
            port = portIn;
            socket = new StreamSocket();
            socket.Control.NoDelay = false;
            connect();
        }

        public bool connect()
        {
            try
            {
                // Connect to the server
                Task.Run(async () => { await socket.ConnectAsync(hostName, port.ToString()); }).Wait();
                sr = new StreamReader(socket.InputStream.AsStreamForRead());
                sw = new StreamWriter(socket.OutputStream.AsStreamForWrite());

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
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Connected");
                        return true;
                    }
                }

            }
            catch (Exception exception)
            {

                exception.Source = "ClientCommunication.creation";
               Shared.ErrorManager.logError(exception);
                throw;

            }
            return false;
        }


        public void Close()
        {
            sr.Dispose();
            socket.Dispose();
        }

    }

}
