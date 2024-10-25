using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggeredAction : MonoBehaviour
{
    [SerializeField] private new Collider2D collider;
    [SerializeField] private UnityEvent actionEvent;
    [SerializeField] private UnityEvent deactivationEvent;

    protected void Action()
    {
        actionEvent.Invoke();
    }

    protected void Deactivate()
    {
        deactivationEvent.Invoke();
    }

    protected void Start()
    {
        if (!collider.isTrigger)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Please, set collider to trigger mode for {name} trigger object");
#endif
            collider.isTrigger = true;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            if (!player.trigger) player.trigger = this;
            Action();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            if (player.trigger == this) player.trigger = null;
            Deactivate();
        }
    }
}
