using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergablesMigration : MonoBehaviour
{
    private List<Mergable> lItems = new List<Mergable>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartMigration()
    {
        lItems = Mergables.Instance.mergables;
    }

    public void ConfirmMigration()
    {
        foreach (Mergable item in lItems)
        {
            InventoryController.Instance.AddItem(item);
        }
    }
}
