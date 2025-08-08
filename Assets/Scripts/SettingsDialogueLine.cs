using System.Collections;
using TMPro;
using UnityEngine;

public class SettingsDialogueLine : MonoBehaviour
{
    private float textSpeed;
    private TextMeshProUGUI textComponent;
    [SerializeField] private string exampleText;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textSpeed = GameSettings.TypingSpeed;
    }

    private void Update()
    {
        textSpeed = GameSettings.TypingSpeed;
    }

    private void OnEnable()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        foreach (char c in exampleText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        typingCoroutine = StartCoroutine(TypeLine());
    }
}