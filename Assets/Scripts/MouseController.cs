using UnityEngine;

[RequireComponent(typeof(AgarObject))]
public class MouseController : MonoBehaviour
{
    private AgarObject _agarObject;

    private void Awake()
    {
        _agarObject = GetComponent<AgarObject>();
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _agarObject.SetMovePosition(mousePosition);
    }
}