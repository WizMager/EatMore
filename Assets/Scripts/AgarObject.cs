using UnityEngine;

public class AgarObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float scale;
    private Vector3 _movePosition;

    private void Awake()
    {
        _movePosition = transform.position;
    }

    private void Update()
    {
        if (speed != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movePosition, speed * Time.deltaTime);
        }
    }

    public void SetColor(Color32 color)
    {
        var playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = color;
    }

    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius * scale, radius * scale, 1);
    }

    public void SetMovePosition(Vector3 newPosition)
    {
        _movePosition = newPosition;
    }
}