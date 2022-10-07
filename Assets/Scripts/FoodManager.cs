using System.Collections.Generic;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private UnityClient client;
    [SerializeField] private GameObject foodPrefab;
    private Dictionary<ushort, FoodObject> _foodObjects = new Dictionary<ushort, FoodObject>();

    private void Awake()
    {
        if (client == null)
        {
            Debug.LogError("Client unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (foodPrefab == null)
        {
            Debug.LogError("Controllable Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }
        
        client.MessageReceived += MessageReceiveHandler;
    }

    private void MessageReceiveHandler(object sender, MessageReceivedEventArgs e)
    {
        using (var message = e.GetMessage())
        {
            if (message.Tag == Tags.FoodItemSendTag)
            {
                SpawnFood(sender, e);
            }
        }
    }

    private void SpawnFood(object sender, MessageReceivedEventArgs e)
    {
        using var message = e.GetMessage();
        using var reader = message.GetReader();
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

            var foodObject = Instantiate(foodPrefab, position, Quaternion.identity).GetComponent<FoodObject>();
            foodObject.SetRadius(radius);
            foodObject.SetColor(color);
            _foodObjects.Add(id, foodObject);
        }
    }
}

        
