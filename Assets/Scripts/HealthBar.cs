using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;

    public void UpdateHealth(float health)
    {
        healthFill.fillAmount = health / 100;
    }
}
