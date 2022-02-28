using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Game.Multiplayer
{
    public class NetworkManager : MonoBehaviour
    {
        ServerHandler _ServerHandler = new ServerHandler();
        ClientHandler _ClientHandler = new ClientHandler();

        private static NetworkManager _Instance;

        public static NetworkManager instance => _Instance;
        public static Server server => instance?._ServerHandler.server;
        public static Client client => instance?._ClientHandler.client;

        // Source: https://stackoverflow.com/questions/3253701/get-public-external-ip-address
        public static string GetPublicIPAddress()
        {
            return new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
        }

        // Source: https://stackoverflow.com/questions/6803073/get-local-ip-address
        public static string GetLocalIPAddress()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return null;

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }

        private void Awake()
        {
            _Instance = this;

            _ClientHandler.Initialize();
            _ServerHandler.Initialize();

            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        }

        // private void Start() 
        // {
        //     serverHandler.StartServer();

        //     clientHandler.Connect();
        // }

        private void FixedUpdate() 
        {
            if(server.IsRunning)
                server.Tick();

            if(client.IsConnected)
                client.Tick();
        }

        private void OnApplicationQuit() 
        {
            if(server.IsRunning)
                server.Stop();    

            if(client.IsConnected)
                client.Disconnect();
        }
    }
}