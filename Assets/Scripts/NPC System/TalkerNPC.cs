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
[RequireComponent(typeof(TalkerController))]
public class TalkerNPC : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float fTriggerDistance;
    [SerializeField] private int currDialogue = 0;

    public void setDialogue(int dialogue) { currDialogue = dialogue; }
    
    private TalkerController mController;

    private bool canBeTriggered = false;

    private bool CanBeTriggered
    {
        get => canBeTriggered;
        set
        {
            if (!value && canBeTriggered)
                mController.ResetDialogue();

            canBeTriggered = value;
        }
    }

    private void Start()
    {
        mController = GetComponent<TalkerController>();
    }

    private void Update()
    {
        if (!Player.Instance)
            return;

        float fDistance = Vector2.Distance(transform.position, Player.Instance.transform.position);
        CanBeTriggered = fDistance <= fTriggerDistance;

        if (Input.GetKeyDown(KeyCode.E) && CanBeTriggered)
            mController.Interact(currDialogue);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, fTriggerDistance);
    }
#endif
}
