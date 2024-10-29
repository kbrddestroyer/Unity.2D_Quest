using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public InventoryItem inventoryItem;

    private PlayerMover playerMover;

    private void OnMouseDown()
    {
        // Clicked on collectable item
        Debug.Log(gameObject.name);

        playerMover.MoveToPoint(transform.position);
    }

    private void Start()
    {
       playerMover = Player.Instance.GetComponent<PlayerMover>();
    }

    public void OnCollect()
    {
        InventoryController.Instance.AddItem(inventoryItem);
        Destroy(gameObject);
    }
}
