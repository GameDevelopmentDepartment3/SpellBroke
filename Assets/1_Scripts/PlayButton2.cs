using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayButton2 : MonoBehaviour
{
    public Button targetButton;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene(2);
        }
    }
}