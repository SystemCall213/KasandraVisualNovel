using System;
using UnityEngine;

public class CharacterDisplayer : MonoBehaviour
{
    [SerializeField] private Character[] characters;

    // Shows sprite, changes existing sprite if spriteToChange passed, show means we show the sprite or hide it if false
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
                    ch.setSpriteName(string.Empty);
                    ch.Disable();
                    break;
                }
            }
        }
    }

    public Character[] GetCharacters()
    {
        return characters;
    }
}
