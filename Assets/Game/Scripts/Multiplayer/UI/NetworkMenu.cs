using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace Game.Multiplayer.UI
{
    public class NetworkMenu : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _IpAddressInputField;
        [SerializeField]
        private TMP_Text _IpAddressText;
        [SerializeField]
        private TMP_Text _ConnectedClientsText;

        string _PublicIpAdress = string.Empty;
        
        private void Start()
        {
            _PublicIpAdress = NetworkManager.GetLocalIPAddress();

            _IpAddressText.text = _PublicIpAdress;
        }

        private void Update()
        {
            if(NetworkManager.server.IsRunning)
                _ConnectedClientsText.text = NetworkManager.server.Clients.Length.ToString();    
        }

        public void ConnectAsClient()
        {
            NetworkManager.client.Connect($"{_IpAddressInputField.text}:9696");
        }

        public void ConnectAsServer()
        {
            NetworkManager.server.Start(9696, 10);
            NetworkManager.client.Connect($"{_PublicIpAdress}:9696");
        }
    }
}