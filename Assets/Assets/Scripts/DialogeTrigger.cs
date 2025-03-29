using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueNode[] dialogueNodes; // Массив реплик

    // Вызывается, например, при входе в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueNodes);
            }
            else
            {
                Debug.LogError("DialogueManager.Instance не найден!");
            }
        }
    }

    // Для теста: запуск диалога по кнопке
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.StartDialogue(dialogueNodes);
        }
    }
}