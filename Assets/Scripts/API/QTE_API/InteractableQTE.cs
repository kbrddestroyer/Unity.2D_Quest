using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractableQTE : QTEController
{
    private Player player;

    [Header("GUI Settings of QTE")]
    [SerializeField] private TMP_Text output;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private GameObject GUIRoot;
    [Header("Interaction Settings")]
    [SerializeField, Range(0f, 10f)] private float distanceToInteract;
    [SerializeField] private UnityEvent onSuccess;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        timeSlider.minValue = 0;
        timeSlider.maxValue = delay + timeBoundToPress;
    }

    protected override void OnStartQTE()
    {
        GUIRoot.SetActive(true);
    }

    protected override void OnCancel()
    {
        GUIRoot.SetActive(false);
    }

    protected override void OnFail()
    {
        
    }

    protected override void OnNextStage(uint index)
    {
        output.text = codes[index].ToString();
        timeSlider.value = 0;
    }

    protected override void OnUpdateDeltaTime(float deltaTime)
    {
        timeSlider.value = deltaTime;
    }

    protected override void OnComplete()
    {
        onSuccess.Invoke();
    }

    protected new void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector2.Distance(player.transform.position, transform.position) < distanceToInteract)
            {
                StartQTE();
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToInteract);
    }
#endif
}
