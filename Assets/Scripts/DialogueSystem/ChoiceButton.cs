using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceText;
    private Choice myChoice;
    private Dialogue dialogue;

    public void Setup(Choice choice, Dialogue dialogueRef)
    {
        myChoice = choice;
        dialogue = dialogueRef;
        choiceText.text = choice.text;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Optionally set a flag
        if (!string.IsNullOrEmpty(myChoice.setFlag))
        {
            GameData.SetFlag(myChoice.setFlag, true);
        }

        // Notify dialogue system
        dialogue.OnChoiceSelected(myChoice.nextLineId);

        // Optionally hide container
        transform.parent.gameObject.SetActive(false);
    }
}
