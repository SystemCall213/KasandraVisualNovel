using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterDisplayer : MonoBehaviour
{
    [SerializeField] private Character[] characters;

    public void SetCharacterSprite(string spriteName, string spriteToChange, bool show)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/{spriteName}");

        if (!sprite) return;

        if (show)
        {
            if (spriteToChange != null)
            {
                foreach (Character ch in characters)
                {
                    if (ch.getSpriteName() == spriteToChange)
                    {
                        ch.setSprite(sprite);
                        ch.setSpriteName(spriteName);
                        break;
                    }
                }
            }
            else
            {
                foreach (Character ch in characters)
                {
                    if (ch.getSpriteName() == string.Empty)
                    {
                        ch.setSprite(sprite);
                        ch.setSpriteName(spriteName);
                        break;
                    }
                }   
            }
        }
        else
        {
            foreach (Character ch in characters)
            {
                if (ch.getSpriteName() == spriteName)
                {
                    ch.Disable();
                    break;
                }
            }
        }
    }
}
