using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tooltip : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField, Range(0f, 10f)] protected float f_duration;
    [Header("Requirements")]
    [SerializeField] protected TMP_Text m_text;
    [SerializeField] protected GameObject m_root;

    private bool b_state = false;   // Enabled | Disabled

    public string Text { get => m_text.text; set => m_text.text = value; }

    public bool State { 
        get => b_state; 
        set
        {
            b_state = value;

            Show(b_state);
        }
    }

    protected IEnumerator DisableTimer()
    {
        if (b_state)
        {
            yield return new WaitForSeconds(f_duration);
            State = false;
        }
    }

    protected void Show(bool currentState)
    {
        m_root.SetActive(currentState);
        StartCoroutine(DisableTimer());
    }
}
