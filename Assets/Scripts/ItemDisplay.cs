using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    private Coroutine timerCoroutine;

    public void UpdateItemUI(Sprite icon, float remainingTime)
    {
        itemIcon.sprite = icon;
        itemIcon.enabled = true; // Show Icon
        timerText.text = $"{Mathf.Max(0, remainingTime):F1} s";
        timerText.enabled = true;
    }

    public void ClearItemUI()
    {
        itemIcon.enabled = false;
        timerText.enabled = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void StartTimer(float duration)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(UpdateTimer(duration));
    }

    private IEnumerator UpdateTimer(float duration)
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            timerText.text = $"{remainingTime:F1} s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }
        timerText.text = "";
    }
}
