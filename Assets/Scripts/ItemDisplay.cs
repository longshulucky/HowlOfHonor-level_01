using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Image itemImage;

    private float itemLifetime;
    private bool isItemActive;

    void Update()
    {
        if (isItemActive)
        {
            itemLifetime -= Time.deltaTime;
            timerText.text = itemLifetime.ToString("F1") + "s";

            if (itemLifetime <= 0)
            {
                ClearItem();
            }
        }
    }

    public void SetItem(float lifetime, Sprite itemSprite)
    {
        itemLifetime = lifetime;
        isItemActive = true;
        timerText.text = itemLifetime.ToString("F1") + "s";
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
    }

    private void ClearItem()
    {
        itemLifetime = 0;
        isItemActive = false;
        timerText.text = "";
        itemImage.enabled = false;
    }
}
