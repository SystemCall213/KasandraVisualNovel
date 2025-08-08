using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using System;
using System.IO;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI speakerComponent;
    [SerializeField] private string dialogueFileName;
    [SerializeField] private float textSpeed;
    [SerializeField] private ChoiceContainer choiceContainer;
    [SerializeField] private CharacterDisplayer characterDisplayer;
    [SerializeField] private BackgroundDisplayer backgroundDisplayer;
    [SerializeField] private MusicManager musicManager;

    // Setting line to adjust speed
    [SerializeField] private SettingsManager settingsManager;

    // lets me check if I can click (because we should be able to read if we clicked esc)
    [SerializeField] private GameObject ecsMenuWrapper;

    private List<DialogueLine> lines;
    private Dictionary<string, int> lineMap;
    private int index;
    private bool waitingForChoice = false;

    private void Awake()
    {
        textComponent.text = string.Empty;
        speakerComponent.text = string.Empty;
        textSpeed = GameSettings.TypingSpeed;
        settingsManager.OnTypingSpeedChanged += AdjustSpeed;

        if (TempLoadData.Data != null)
        {
            LoadDialogueFromFile(dialogueFileName);
            index = TempLoadData.Data.lineIndex;
            backgroundDisplayer.setBackground(TempLoadData.Data.backgroundName);
            musicManager.setMusic(TempLoadData.Data.musicName);

            List<string> characters = TempLoadData.Data.characters;

            foreach (string ch in characters)
            {
                characterDisplayer.SetCharacterSprite(ch, null, true);
            }

            TempLoadData.Data = null; // Clear after use
            StartCoroutine(TypeLine());
        }
        else
        {
            LoadDialogueFromFile(dialogueFileName);
            StartDialogue();
        }
    }

    void AdjustSpeed()
    {
        textSpeed = GameSettings.TypingSpeed;
    }

    void LoadDialogueFromFile(string fileName)
    {
        TextAsset json = Resources.Load<TextAsset>(fileName);
        DialogueData data = JsonUtility.FromJson<DialogueData>(json.text);
        lines = data.lines;

        // Optional: build ID lookup map
        lineMap = new Dictionary<string, int>();
        for (int i = 0; i < lines.Count; i++)
        {
            if (!string.IsNullOrEmpty(lines[i].id))
            {
                lineMap[lines[i].id] = i;
            }
        }
    }

    void Update()
    {
        if (waitingForChoice) return;

        if (Input.GetMouseButtonDown(0) && !ecsMenuWrapper.activeInHierarchy)
        {
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
                // Check for choices after typing finishes
                if (lines[index].choices != null && lines[index].choices.Count > 0)
                {
                    waitingForChoice = true;
                    choiceContainer.ShowChoices(lines[index].choices, this);
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        AdvanceToVisibleLine();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        DialogueLine currentLine = lines[index];

        textComponent.text = string.Empty;
        if (currentLine.speaker != null)
        {
            speakerComponent.text = currentLine.speaker;
        }
        else
        {
            speakerComponent.text = string.Empty;
        }

        if (currentLine.spriteName != null)
        {
            characterDisplayer.SetCharacterSprite(currentLine.spriteName, currentLine.spriteToChange, currentLine.showCharacter);
        }

        if (currentLine.backgroundName != null)
        {
            backgroundDisplayer.setBackground(currentLine.backgroundName);
        }

        if (currentLine.backgroundMusic != null)
        {
            musicManager.setMusic(currentLine.backgroundMusic);
        }

        foreach (char c in currentLine.text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Check for choices after typing finishes
        if (currentLine.choices != null && currentLine.choices.Count > 0)
        {
            waitingForChoice = true;
            choiceContainer.ShowChoices(currentLine.choices, this);
        }
    }

    bool IsLineVisible(DialogueLine line)
    {
        if (line.condition.flag == null) return true;
        bool flagState = GameData.flags.ContainsKey(line.condition.flag) && GameData.flags[line.condition.flag];
        return flagState == line.condition.value;
    }

    void NextLine()
    {
        DialogueLine currentLine = lines[index];

        // Check if this line explicitly points to a next one
        if (!string.IsNullOrEmpty(currentLine.nextLineId))
        {
            if (lineMap.TryGetValue(currentLine.nextLineId, out int next))
            {
                index = next;
                while (index < lines.Count && !IsLineVisible(lines[index]))
                {
                    index++;
                }
            }
            else
            {
                Debug.LogWarning($"Next line ID {currentLine.nextLineId} not found.");
                gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            // Default sequential behavior
            index++;
            while (index < lines.Count && !IsLineVisible(lines[index]))
            {
                index++;
            }

            if (index >= lines.Count)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }


    void AdvanceToVisibleLine()
    {
        while (index < lines.Count && !IsLineVisible(lines[index]))
        {
            index++;
        }

        /*
        if (index >= lines.Count)
        {
            gameObject.SetActive(false);
        }
        */
    }

    public void OnChoiceSelected(string nextLineId)
    {
        if (lineMap.TryGetValue(nextLineId, out int nextIndex))
        {
            index = nextIndex;
            waitingForChoice = false;
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.LogWarning("Next line ID not found: " + nextLineId);
        }
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        string backName = backgroundDisplayer.getBackgroundName();
        string musicClipName = musicManager.getMusicClipName();
        List<string> characters = new List<string>();

        foreach (Character ch in characterDisplayer.GetCharacters())
        {
            if (ch.getSpriteName() != string.Empty)
                characters.Add(ch.getSpriteName());
        }

        SaveData save = new SaveData
        {
            lineIndex = index,
            backgroundName = backName,
            musicName = musicClipName,
            characters = characters
        };

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), json);
    }

}
