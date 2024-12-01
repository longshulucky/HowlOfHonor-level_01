using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCooldown : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;
    private bool isCooldown = false;

    private void Start()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
    }

    public void StartCooldown(float duration)
    {
        if (!isCooldown)
        {
            StartCoroutine(CooldownRoutine(duration));
        }
    }

    public bool IsCooldownActive()
    {
        return isCooldown;
    }

    private IEnumerator CooldownRoutine(float duration)
    {
        isCooldown = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = 1f - (elapsed / duration);
            yield return null;
        }

        cooldownImage.fillAmount = 0f;
        isCooldown = false;
    }
}
