using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableQTE : QTEController
{
    private Player player;

    [Header("GUI Settings of QTE")]
    [SerializeField] private TMP_Text output;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private GameObject GUIRoot;

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
        // Nothing goes here now
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
        // Added to maintain QTE API, nothing here currently
    }
}
