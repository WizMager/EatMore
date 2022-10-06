using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
        [SerializeField] private float moveThreshold = 0.05f;
        private Vector3 _lastPosition;

        public UnityClient Client { get; set; }

        private void Awake()
        {
                _lastPosition = transform.position;
        }

        private void Update()
        {
                if (!(Vector3.Distance(_lastPosition, transform.position) >= moveThreshold)) return;

                var position = transform.position;
                using (var writer = DarkRiftWriter.Create())
                {
                        writer.Write(position.x);
                        writer.Write(position.y);
                        using (var message = Message.Create(Tags.PlayerMoveTag, writer))
                        {
                                Client.SendMessage(message, SendMode.Unreliable);
                        }
                }

                _lastPosition = position;
        }
}