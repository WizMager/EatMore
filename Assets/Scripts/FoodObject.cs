using UnityEngine;

public class FoodObject : MonoBehaviour
{
       [SerializeField] private Renderer foodRenderer;
       [SerializeField] private Transform foodTransform;
       [SerializeField] private float scale;

       public void SetPosition(Vector3 position)
       {
           foodTransform.position = position;
       }

       public void SetRadius(float radius)
       {
           foodTransform.localScale = new Vector3(radius * scale, radius * scale, 1);
       }

       public void SetColor(Color32 color)
       {
           foodRenderer.material.color = color;
       }
}