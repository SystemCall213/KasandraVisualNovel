using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string id;
    public string speaker;
    public string text;
    public string nextLineId;
    public List<Choice> choices;
    public Condition condition; // optional
}