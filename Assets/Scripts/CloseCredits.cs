using UnityEngine;
using UnityEngine.UI;

public class CloseCredits : MonoBehaviour
{
    [SerializeField] private GameObject Credits;
    [SerializeField] private Button closeCredits;

    private void Awake()
    {
        closeCredits.onClick.AddListener(backToMenuClicked);
    }

    public void backToMenuClicked()
    {
        Credits.SetActive(false);
    }
}
