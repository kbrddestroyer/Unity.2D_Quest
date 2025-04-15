using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class DialogueVisualHelper : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    private Image backgroundImage;

    private void Start()
    {
        // ���� ���������� ������ ������ ����� Canvas
        dialogueText = GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
        backgroundImage = GetComponentInChildren<Image>(includeInactive: true);

        if (dialogueText == null)
            Debug.LogWarning($"[DialogueVisualHelper] �� ������ TextMeshProUGUI � {gameObject.name}");

        if (backgroundImage == null)
            Debug.LogWarning($"[DialogueVisualHelper] �� ������ Image (���) � {gameObject.name}");
    }

    private void Update()
    {
        if (dialogueText == null || backgroundImage == null)
            return;

        bool shouldShowBackground = dialogueText.enabled && !string.IsNullOrWhiteSpace(dialogueText.text);

        // �������� ��� ��������� ���
        backgroundImage.enabled = shouldShowBackground;
    }
}
