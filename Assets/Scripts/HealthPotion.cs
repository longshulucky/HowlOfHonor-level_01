using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healthAmount = 20f;
    [SerializeField] private SpriteRenderer potionImage;

    public float GetHealthAmount() => healthAmount;

    public Sprite GetSprite() => potionImage.sprite; // Méthode pour obtenir le sprite

    public void Consume()
    {
        Destroy(gameObject);
    }
}
