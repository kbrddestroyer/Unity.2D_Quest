using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPassConditionHasItem : IQuestPassCondition
{
    private Quest questReference;
    
    [SerializeField] private Mergable inventoryItem;

    public override bool isPassed()
    {
        return InventoryController.Instance.HasItem(inventoryItem);
    }

    public override void OnQuestStarted(Quest quest)
    {
        questReference = quest;
    }
}
