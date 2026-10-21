using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour
{
    public Sprite emptyLifeSprite;
    public Sprite fullLifeSprite;
    public Image[] lifeImages = new Image[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateLife(int currentLife)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].sprite = i < currentLife ? fullLifeSprite : emptyLifeSprite;
        }
    }
}
