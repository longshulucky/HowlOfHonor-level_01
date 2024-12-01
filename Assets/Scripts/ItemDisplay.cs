using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI timerText;
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
    }
}
