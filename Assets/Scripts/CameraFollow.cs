using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        
        public Transform Target { get; set; }

        private void Update()
        {
            if (Target != null)
            {
                var targetPosition = Target.GetComponent<Renderer>().bounds.center;
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(targetPosition.x, targetPosition.y, transform.position.z), speed * Time.deltaTime);
            }
        }
    }
}