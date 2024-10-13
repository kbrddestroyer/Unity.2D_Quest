using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;
    public static InventoryController Instance { get => instance; }

    private List<InventoryItem> allItems = new List<InventoryItem>();

    [SerializeField] private Transform inventoryGUIRoot;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private bool isGUIEnabled = true;

    public bool GUIEnabled { get => isGUIEnabled; }

    private void SetGUIEnabled(bool bState)
    {
        isGUIEnabled = bState;
        inventoryGUIRoot.gameObject.SetActive(isGUIEnabled);
    }

    private void Update()
    {
        if (Input.GetKeyUp(toggleKey))
        {
            SetGUIEnabled(!isGUIEnabled);
        }
    }

    private void Awake()
    {
        if (instance)
            Debug.LogWarning("Multiple singleton instances on scene");
        SetGUIEnabled(false);
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void AddItem(InventoryItem prefab)
    {
        InventoryItem item = Instantiate(prefab, inventoryGUIRoot);
        allItems.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        allItems.Remove(item);
        Destroy(item.gameObject);
    }
}
