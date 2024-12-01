using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;
    [SerializeField] private TextMeshProUGUI healthText; // Ajout du texte de la santé

    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;
    [SerializeField] private Color healthIncreaseColor = Color.cyan; // Couleur pour l'augmentation de la santé

    private void Start()
    {
        UpdateHealth(100);
    }

    public void UpdateHealth(float health)
    {
        healthFill.fillAmount = health / 100;
        healthText.text = $"{(int)Mathf.Max(health, 0)} / 100"; // Mise à jour du texte de la santé

        // Change color based on health percentage
        if (health > 66)
        {
            SetHealthColor(fullHealthColor);
        }
        else if (health > 33)
        {
            SetHealthColor(mediumHealthColor);
        }
        else
        {
            SetHealthColor(lowHealthColor);
        }
    }

    public void AnimateHealthIncrease()
    {
        StartCoroutine(HealthIncreaseAnimation());
    }

    private IEnumerator HealthIncreaseAnimation()
    {
        Color originalColor = healthText.color;
        healthText.color = healthIncreaseColor;
        yield return new WaitForSeconds(0.5f); // Durée de l'animation
        healthText.color = originalColor;
    }

    private void SetHealthColor(Color color)
    {
        healthFill.color = color;
        healthText.color = color;
    }
}
