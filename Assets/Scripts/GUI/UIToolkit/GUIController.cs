using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private ButtonController[] buttons;

    private void Start()
    {
        foreach (ButtonController controller in buttons)
            controller.Initialize();
    }
}
