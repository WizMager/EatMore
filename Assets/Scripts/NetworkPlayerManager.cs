using System.Collections.Generic;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class NetworkPlayerManager : MonoBehaviour
{
        [SerializeField] private UnityClient client;
        private Dictionary<ushort, AgarObject> _networkPlayers = new Dictionary<ushort, AgarObject>();

        private void Awake()
        {
                client.MessageReceived += MessageReceiveHandler;
        }

        private void MessageReceiveHandler(object sender, MessageReceivedEventArgs e)
        {
                using (var message = e.GetMessage())
                {
                        if (message.Tag == Tags.PlayerMoveTag)
                        {
                                using (var reader = message.GetReader())
                                {
                                        var id = reader.ReadUInt16();
                                        var newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), 0);

                                        if (_networkPlayers.ContainsKey(id))
                                        {
                                                _networkPlayers[id].SetMovePosition(newPosition);
                                        }
                                }
                        }
                }
        }

        public void Add(ushort id, AgarObject player)
        {
                _networkPlayers.Add(id, player);
        }
}