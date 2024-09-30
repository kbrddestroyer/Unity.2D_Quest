using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public InventoryItem inventoryItem;

    public void OnCollect()
    {
        InventoryController.Instance.AddItem(inventoryItem);
        Destroy(gameObject);
    }
}
