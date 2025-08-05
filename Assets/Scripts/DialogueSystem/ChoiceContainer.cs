using System.Collections.Generic;
using UnityEngine;

public class ChoiceContainer : MonoBehaviour
{
    [SerializeField] private ChoiceButton choicePrefab;

    public void ShowChoices(List<Choice> choices, Dialogue dialogue)
    {
        // Remove old buttons
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var choice in choices)
        {
            ChoiceButton button = Instantiate(choicePrefab, transform);
            button.Setup(choice, dialogue);
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
