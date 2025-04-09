using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TalkerController : MonoBehaviour
{
    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> wrapped;

        public int Count { get => wrapped.Count; }
    }

    [SerializeField] private List<ListWrapper<string>> sDialogues;
    [SerializeField, Range(0f, 0.5f)] private float fCharacterDelay;
    [Header("GUI")]
    [SerializeField] private TMP_Text text;
    [Header("Dynamic Scripting")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onType;
    [SerializeField] private UnityEvent onFinishAll;
    [SerializeField] private UnityEvent onStop;

    private bool isTalking = false;
    private bool isBusy = false;
    private int line = 0;
    private int dialogue = 0;

    private IEnumerator Talk(string line)
    {
        isTalking = true;
        text.text = "";

        foreach (char ch in line)
        {
            text.text += ch;
            onType.Invoke();
            yield return new WaitForSeconds(fCharacterDelay);
        }
        isTalking = false;
    }

    public void Interact(int dialogue = 0)
    {
        this.dialogue = dialogue;  
        if (!isBusy && sDialogues.Count > dialogue)
            StartDialogue();
        
        SkipOrSay();
    }

    private void Say(int lineNumber)
    {
        if (lineNumber == sDialogues[dialogue].Count)
        {
            text.text = "";
            ResetDialogue();

            onFinishAll.Invoke();
            return;
        }
        else if (lineNumber > sDialogues[dialogue].Count)
        {
            Debug.LogError("Trying to call dialogue with line > length!");
            return;
        }
        StartCoroutine(Talk(sDialogues[dialogue].wrapped[lineNumber]));
    }

    private void SayNextLine()
    {
        Say(line++);
    }

    private void SkipOrSay()
    {
        if (isTalking)
        {
            if (line > sDialogues[dialogue].Count)
                return;

            StopAllCoroutines();
            text.text = sDialogues[dialogue].wrapped[line - 1];
            isTalking = false;
        }
        else
        {
            SayNextLine();
        }
    }

    public void ResetDialogue()
    {
        text.text = "";

        line = 0;
        isTalking = false;
        isBusy = false;

        onStop.Invoke();
    }

    private void StartDialogue()
    {
        line = 0;
        isBusy = true;

        onStart.Invoke();
    }
}
