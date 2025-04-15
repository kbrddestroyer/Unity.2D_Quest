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
        // Ищем компоненты только внутри этого Canvas
        dialogueText = GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
        backgroundImage = GetComponentInChildren<Image>(includeInactive: true);

        if (dialogueText == null)
            Debug.LogWarning($"[DialogueVisualHelper] Не найден TextMeshProUGUI в {gameObject.name}");

        if (backgroundImage == null)
            Debug.LogWarning($"[DialogueVisualHelper] Не найден Image (фон) в {gameObject.name}");
    }

    private void Update()
    {
        if (dialogueText == null || backgroundImage == null)
            return;

        bool shouldShowBackground = dialogueText.enabled && !string.IsNullOrWhiteSpace(dialogueText.text);

        // Включаем или выключаем фон
        backgroundImage.enabled = shouldShowBackground;
    }
}
