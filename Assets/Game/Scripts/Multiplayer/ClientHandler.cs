using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Game.Multiplayer
{
    [System.Serializable]
    public class ClientHandler
    {
        [SerializeField]
        private string _Ip;
        [SerializeField]
        private ushort _Port;

        private Client _Client = new Client();

        public Client client => _Client;

        private void OnClientConnect(object sender, ClientConnectedEventArgs args)
        {
            
        }
        private void OnClientDisconnect(object sender, ClientDisconnectedEventArgs args)
        {

        }

        public void Initialize()
        {
            client.ClientConnected += OnClientConnect;
            client.ClientDisconnected += OnClientDisconnect;
        }

        public void Tick()
        {
        }

        public void Connect()
        {
            client.Connect($"{_Ip}:{_Port}");
        }
    }
}