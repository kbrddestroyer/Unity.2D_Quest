using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;
    public static InventoryController Instance { get => instance; }

    private List<InventoryItem> allItems = new List<InventoryItem>();

    [SerializeField] private Transform inventoryGUIRoot;

    private void Awake()
    {
        if (instance)
            Debug.LogWarning("Multiple singleton instances on scene");
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
