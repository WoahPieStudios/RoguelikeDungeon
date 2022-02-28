using System;
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
        private Client _Client = new Client();

        public Client client => _Client;

        private void OnClientConnect(object sender, ClientConnectedEventArgs args)
        {
            
        }

        private void OnClientDisconnect(object sender, ClientDisconnectedEventArgs args)
        {

        }

        private void OnConnectionFailed(object sender, EventArgs e)
        {
            Debug.Log("BALH");
        }

        public void Initialize()
        {
            client.ClientConnected += OnClientConnect;
            client.ClientDisconnected += OnClientDisconnect;
            client.ConnectionFailed += OnConnectionFailed;
        }
    }
}