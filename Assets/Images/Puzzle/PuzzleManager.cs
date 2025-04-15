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
                return; // не все собраны
            }
        }

        Debug.Log("Puzzle completed!");
        puzzleUI.SetActive(false);
        // Здесь можно вызвать следующий квест, сцену, катсцену и т.п.
    }
}
