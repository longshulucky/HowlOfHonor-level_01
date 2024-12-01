using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healthAmount = 20f;

    public float GetHealthAmount() => healthAmount;

    public void Consume()
    {
        Destroy(gameObject);
    }
}