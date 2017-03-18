using Shared;
using System;
using System.IO;
using System.Net.Sockets;

namespace TCPCommunication.server
{
    public class clientListener : CommunicationTemplate
    {
        TcpClient tcpclient;

        public int clientNumb = 0;
        public bool isServerEnabled = false;

        public bool automaticReading = true;

        public clientListener(TcpClient cp, int clientNumber)
        {
            tcpclient = cp;
            System.Diagnostics.Debug.WriteLine(clientNumber + " connected!");

            // stream
            NetworkStream s = tcpclient.GetStream();
            sr = new StreamReader(s);
            sw = new StreamWriter(s);

            sw.AutoFlush = true;

            clientNumb = clientNumber;
            writeCommand("OK");
            isServerEnabled = true;
        }

        public delegate void ClientClosing();
        public event ClientClosing clientClosing;



        public void close()
        {
            try
            {
                isServerEnabled = false;
                clientClosing?.Invoke();

                tcpclient.Close();
                System.Diagnostics.Debug.WriteLine("Client " + clientNumb + " is disconnected");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error on closing : " + clientNumb);
                System.Diagnostics.Debug.WriteLine(ErrorManager.getErrorString(ex));
            }

        }

        ~clientListener()
        {
            System.Diagnostics.Debug.WriteLine("Destructing the client listener for : " + clientNumb);
            close();
        }
    }
}
