using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton2 : MonoBehaviour
{
    public Button targetButton;

    void Start()
    {
        Time.timeScale = 1f;
        targetButton.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}