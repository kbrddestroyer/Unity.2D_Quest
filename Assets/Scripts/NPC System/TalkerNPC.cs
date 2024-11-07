using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/*
 *  It'r really not recommended to put something heavy inside onType action
 *  because it's being called on every character type (around 100 times per sec.)
*/
/// <summary>
/// Basic talker class with scripting implementation
/// </summary>
public class TalkerNPC : MonoBehaviour
{
    [SerializeField] private string[] sDialogue;
    [SerializeField, Range(0f, 0.5f)] private float fCharacterDelay;
    [SerializeField, Range(0f, 10f)] private float fTriggerDistance;
    [Header("GUI")]
    [SerializeField] private TMP_Text text;
    [Header("Dynamic Scripting")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onType;
    [SerializeField] private UnityEvent onFinishAll;
    [SerializeField] private UnityEvent onStop;


    private bool isTalking = false;
    private bool isBusy = false;
    private bool canBeTriggered = false;
    private uint line = 0;

    private bool CanBeTriggered
    {
        get => canBeTriggered;
        set
        {
            if (!value)
                ResetDialogue();

            canBeTriggered = value;
        }
    }

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

    private void Update()
    {
        if (!Player.Instance)
            return;

        float fDistance = Vector2.Distance(transform.position, Player.Instance.transform.position);
        CanBeTriggered = fDistance <= fTriggerDistance;

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    public void Interact()
    {
        if (!canBeTriggered) return;

        if (!isBusy)
            StartDialogue();
        SkipOrSay();
    }

    private void Say(uint lineNumber)
    {
        if (lineNumber == sDialogue.Length)
        {
            text.text = "";
            ResetDialogue();

            onFinishAll.Invoke();
            return;
        }
        else if(lineNumber > sDialogue.Length)
        {
            Debug.LogError("Trying to call dialogue with line > length!");
            return;
        }
        StartCoroutine(Talk(sDialogue[lineNumber]));
    }

    private void SayNextLine()
    {
        Say(line++);
    }

    private void SkipOrSay()
    {
        if (isTalking)
        {
            if (line > sDialogue.Length)
                return;

            StopAllCoroutines();
            text.text = sDialogue[line - 1];
            isTalking = false;
        }
        else
        {
            SayNextLine();
        }
    }

    private void ResetDialogue()
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, fTriggerDistance);
    }
#endif
}
