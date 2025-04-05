using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalSqitch : SceneLoader
{
    [SerializeField] private Mergable toPass;

    public override void invoke()
    {
        InventoryController reference = InventoryController.Instance;

        if (reference.HasItem(toPass))
        {
            base.invoke();
        }
    }
}
