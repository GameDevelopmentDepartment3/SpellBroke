using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float time;

    void Update()
    {
        time += Time.deltaTime;

        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{min:00}:{sec:00}";
    }
}