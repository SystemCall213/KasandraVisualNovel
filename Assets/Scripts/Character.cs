using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void setSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
