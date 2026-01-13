using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Button targetButton;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
        }
    }
}