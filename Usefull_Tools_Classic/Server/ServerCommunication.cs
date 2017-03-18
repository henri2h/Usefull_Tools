using System.Collections.Generic;

namespace TCPCommunication.server
{
    // This class manage the connection between all the clients
    public class ServerCommunication
    {
        public class ServerInformations
        {
            public int numberOfClient { get; set; }
            public int portNumber { get; set; }
        }

        private ServerInformations sInfo = new ServerInformations();
        public ServerInformations informations
        {
            get
            {
                sInfo.numberOfClient = clientNumber;
                return sInfo;
            }
        }


        int clientNumber = 0;
        public serverSockethandeler serv;

        public List<clientListener> serverClients;


        public delegate void newClient(clientListener client);
        public event newClient NewClientConnected;

        public ServerCommunication(int port = 2060)
        {
            sInfo.portNumber = port;
            // creating the server
            serverClients = new List<clientListener>();
            serv = new serverSockethandeler(port);

            serv.new_tcp_client += server_new_tcp_client;
            serv.Listen();

        }

        void server_new_tcp_client(System.Net.Sockets.TcpClient tcpclient)
        {
            // creating the client connection
            //   clientConnector cl = new clientConnector(tcpclient, clientNumber);
            clientListener cl = new clientListener(tcpclient, clientNumber);
            serverClients.Add(cl);
            clientNumber++;
            NewClientConnected?.Invoke(cl);

        }

        public void writeAll(string text)
        {
            foreach (clientListener cl in serverClients) { cl.write(text); }
        }


        public List<clientListener> listClient() { return serverClients; }

        // when we have to close the server
        public void close()
        {
            System.Diagnostics.Debug.WriteLine("Closing");
            foreach (clientListener cl in serverClients) { cl.close(); }
            serv.Stop();
        }

    }
}
