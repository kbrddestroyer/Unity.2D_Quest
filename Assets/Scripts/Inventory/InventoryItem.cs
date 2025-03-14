using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour, IInventoryItem
{
    public abstract void OnRegistered();
}
