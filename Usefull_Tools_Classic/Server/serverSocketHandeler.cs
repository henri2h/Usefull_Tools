using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TCPCommunication.server
{
    public class serverSockethandeler
    {
        private TcpListener tcpListener;
        public List<TcpClient> connected_clients;

        public delegate void _new_client(TcpClient tcpclient);
        public event _new_client new_tcp_client;

        // creator
        public serverSockethandeler(int port = 2060) { start(port); }

        // start
        public void start(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            connected_clients = new List<TcpClient>();
        }

        public void Listen()
        {
            tcpListener.Start();
            set_listen();
        }

        private void set_listen() { tcpListener.BeginAcceptTcpClient(new AsyncCallback(_on_new_client), null); }

        private void _on_new_client(IAsyncResult _async_client)
        {
            TcpClient _tcp_cl = tcpListener.EndAcceptTcpClient(_async_client);
            new_tcp_client?.Invoke(_tcp_cl);
            set_listen();
        }

        public void Stop() { tcpListener.Stop(); }


    }
}
