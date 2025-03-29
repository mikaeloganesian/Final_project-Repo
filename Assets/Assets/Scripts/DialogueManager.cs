using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image speakerImage;

    [Header("Character Portraits")]
    [SerializeField] private Sprite mikaeilSprite;
    [SerializeField] private Sprite vladimirSprite;

    [Header("Autoplay Settings")]
    [SerializeField] private bool startAutomatically = true;
    [SerializeField] private DialogueNode[] testDialogue;

    private DialogueNode[] currentDialogue;
    private int currentNodeIndex;
    private bool isDialogueActive;
    private bool isWaitingForInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        HideDialogue();
    }

    private void Start()
    {
        if (startAutomatically && testDialogue != null && testDialogue.Length > 0)
        {
            StartDialogue(testDialogue);
        }
    }

    public void StartDialogue(DialogueNode[] dialogue)
    {
        if (dialogue == null || dialogue.Length == 0)
        {
            Debug.LogWarning("Диалог пустой!");
            return;
        }

        currentDialogue = dialogue;
        currentNodeIndex = 0;
        isDialogueActive = true;
        isWaitingForInput = false;
        dialoguePanel.SetActive(true);
        ShowCurrentNode();
    }

    private void ShowCurrentNode()
    {
        if (!isDialogueActive) return;

        if (currentNodeIndex < currentDialogue.Length)
        {
            DialogueNode node = currentDialogue[currentNodeIndex];
            
            speakerText.text = node.speakerName ?? "???";
            messageText.text = node.message ?? "...";

            // Управление отображением портрета
            if (node.speakerName.Contains("Рассказчик"))
            {
                speakerImage.gameObject.SetActive(false);
            }
            else if (node.speakerName.Contains("Микаэл"))
            {
                speakerImage.gameObject.SetActive(true);
                speakerImage.sprite = mikaeilSprite;
                speakerImage.preserveAspect = true;
            }
            else if (node.speakerName.Contains("Владимир"))
            {
                speakerImage.gameObject.SetActive(true);
                speakerImage.sprite = vladimirSprite;
                speakerImage.preserveAspect = true;
            }

            isWaitingForInput = true;
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (!isDialogueActive || !isWaitingForInput) return;

        if (Input.GetKeyDown(KeyCode.Space) || 
           Input.GetMouseButtonDown(0) || 
           (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isWaitingForInput = false;
            ShowNextNode();
        }
    }

    private void ShowNextNode()
    {
        currentNodeIndex++;
        ShowCurrentNode();
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        HideDialogue();
    }

    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}