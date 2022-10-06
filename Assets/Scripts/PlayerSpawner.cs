using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
      [SerializeField] private UnityClient client;
      [SerializeField] private GameObject controllablePrefab;
      [SerializeField] private GameObject networkPrefab;

      private void Awake()
      {
            if (client == null)
            {
                  Debug.LogError("Client unassigned in PlayerSpawner.");
                  Application.Quit();
            }

            if (controllablePrefab == null)
            {
                  Debug.LogError("Controllable Prefab unassigned in PlayerSpawner.");
                  Application.Quit();
            }

            if (networkPrefab == null)
            {
                  Debug.LogError("Network Prefab unassigned in PlayerSpawner.");
                  Application.Quit();
            }

            client.MessageReceived += SpawnPlayer;
      }

      private void SpawnPlayer(object sender, MessageReceivedEventArgs e)
      {
            using var message = e.GetMessage();
            using (var reader = message.GetReader())
            {
                  if (message.Tag == Tags.PlayerSpawnTag)
                  {
                        if (reader.Length % 17 != 0)
                        {
                              Debug.LogWarning("Received malformed spawn packet.");
                              return;
                        }

                        while (reader.Position < reader.Length)
                        {
                              var id = reader.ReadUInt16();
                              var position = new Vector3(reader.ReadSingle(), reader.ReadSingle());
                              var radius = reader.ReadSingle();
                              var color = new Color32(
                                    reader.ReadByte(), 
                                    reader.ReadByte(), 
                                    reader.ReadByte(),
                                    255);
                              
                              var playerGameObject = Instantiate(id == client.ID ? controllablePrefab : networkPrefab, position, Quaternion.identity);
                              
                              var agarObject = playerGameObject.GetComponent<AgarObject>();
                              agarObject.SetRadius(radius);
                              agarObject.SetColor(color);
                        }
                  } 
            }
      }
}