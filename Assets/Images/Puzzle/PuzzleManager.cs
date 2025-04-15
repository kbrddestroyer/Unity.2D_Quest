using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GameObject puzzleUI;
    public GameObject[] puzzlePieces;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowPuzzle()
    {
        puzzleUI.SetActive(true);
    }

    public void CheckPuzzle()
    {
        foreach (var piece in puzzlePieces)
        {
            if (piece.GetComponent<CanvasGroup>().blocksRaycasts)
            {
                return; // �� ��� �������
            }
        }

        Debug.Log("Puzzle completed!");
        puzzleUI.SetActive(false);
        // ����� ����� ������� ��������� �����, �����, �������� � �.�.
    }
}
