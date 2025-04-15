using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 correctPosition;
    public float snapDistance = 50f;

    private Vector3 startPosition;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(rectTransform.anchoredPosition, correctPosition) < snapDistance)
        {
            rectTransform.anchoredPosition = correctPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = false; // отключаем перетаскивание
            PuzzleManager.Instance.CheckPuzzle(); // проверка на сборку
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }
}
