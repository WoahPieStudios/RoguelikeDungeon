using System.Collections;
using System.Collections.Generic;
using System.Net;

using UnityEngine;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Game.Multiplayer
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField]
        ServerHandler _ServerHandler;
        [SerializeField]
        ClientHandler _ClientHandler;

        private static NetworkManager _Instance;

        public static NetworkManager instance => _Instance;
        public static ServerHandler serverHandler => instance._ServerHandler;
        public static ClientHandler clientHandler => instance._ClientHandler;

        public static string GetPublicIpAddress()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            
            IPAddress externalIp = IPAddress.Parse(externalIpString);

            return externalIpString.ToString();
        }

        private void Awake()
        {
            _Instance = this;

            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        }

        // private void Start() 
        // {
        //     serverHandler.StartServer();

        //     clientHandler.Connect();
        // }

        private void FixedUpdate() 
        {
            if(serverHandler.server.IsRunning)
                serverHandler.server.Tick();

            if(clientHandler.client.IsConnected)
                clientHandler.client.Tick();
        }

        private void OnApplicationQuit() 
        {
            if(serverHandler.server.IsRunning)
                serverHandler.server.Stop();    

            if(clientHandler.client.IsConnected)
                clientHandler.client.Disconnect();
        }
    }
}