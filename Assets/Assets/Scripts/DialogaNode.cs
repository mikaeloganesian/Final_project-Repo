using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string speakerName; // Имя говорящего
    [TextArea(3, 5)] public string message; // Текст реплики
}