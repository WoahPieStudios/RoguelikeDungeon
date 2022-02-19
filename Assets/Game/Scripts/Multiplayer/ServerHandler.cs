using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Game.Multiplayer
{
    [System.Serializable]
    public class ServerHandler
    {
        [SerializeField]
        private ushort _Port;
        [SerializeField]
        private ushort _MaxClientCount;

        private Server _Server = new Server();

        public Server server => _Server;
        

        public void StartServer()
        {
            server.Start(_Port, _MaxClientCount);
        }
    }
}