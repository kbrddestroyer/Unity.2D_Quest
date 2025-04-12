using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;
    public static InventoryController Instance { get => instance; }

    private List<InventoryItem> allItems = new List<InventoryItem>();

    [SerializeField] private Transform inventoryGUIRoot;
    [SerializeField] private UnityEvent failedMergeEvent;

    private bool isGUIEnabled = true;

    public bool GUIEnabled { get => isGUIEnabled; }

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

    public void AddItemToInventory(InventoryItem item)
    {
        item.OnRegistered();
        allItems.Add(item);
    }

    public void AddItem(InventoryItem prefab)
    {
        InventoryItem item = Instantiate(prefab, inventoryGUIRoot);
        AddItemToInventory(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        Mergable itemMergable = (Mergable)item;

        if (itemMergable)
        {
            Mergable toDestroy = null;
            foreach (Mergable mergable in allItems)
            {
                if (mergable.ID == itemMergable.ID)
                {
                    toDestroy = mergable;
                    break;
                }
            }

            if (toDestroy == null)
            {
                Debug.LogWarning("No item found!");
                return;
            }

            Destroy(toDestroy.gameObject);
            return;
        }
        allItems.Remove(item);
        Destroy(item.gameObject);
        return;
    }

    public bool HasItem(Mergable item)
    {
        
        foreach (Mergable itemMergable in allItems)
            Debug.Log(itemMergable.ID);
        return allItems.Contains(item);
    }

    public void OnMergeFail()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryGUIRoot.GetComponent<RectTransform>());

        failedMergeEvent.Invoke();
    }
}
