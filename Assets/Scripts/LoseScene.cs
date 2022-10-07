using UnityEngine;
using UnityEngine.UI;

public class LoseScene : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveListener(Exit);
    }
}