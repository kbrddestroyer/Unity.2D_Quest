using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject controlledObject;

    private void Update()
    {
        if (Input.GetKeyUp(toggleKey))
        {
            controlledObject.SetActive(!controlledObject.activeInHierarchy);
        }
    }
}
