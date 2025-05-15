using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class EffectsParemetresGUI : MonoBehaviour
{
    public TextMeshProUGUI nameEffect;
    public TextMeshProUGUI timer;
    public UnityEngine.UI.Image icon;
    public TimerDelegate timerEnd;
    public TimerDelegateSeconds timerEverySecond;

    private void Awake()
    {
        timerEnd += DestroyObject;
        timerEverySecond += ChangeTimer;
    }

    private void DestroyObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void ChangeTimer(int seconds)
    {
        int countMinutes = seconds / 60;
        int countSeconds = seconds % 60;

        timer.text = countMinutes + " : " + countSeconds;
    }
}
