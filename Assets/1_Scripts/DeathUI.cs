using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;

    private bool isDead = false;
    private bool keyboardChecked = false;

    public void OnDeath()
    {
        isDead = true;
        Time.timeScale = 0f;

        deathPanel.SetActive(true);
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
    }
}