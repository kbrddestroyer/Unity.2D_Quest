using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : MonoBehaviour
{
    [SerializeField] public IQuestPassCondition condition;
    [SerializeField] private UnityEvent onQuestFinished;

    [SerializeField] private QuestStatus status = QuestStatus.QUEST_IDLE;
    public QuestStatus Status { get => status; protected set => status = value; }

    public void Start()
    {
        if (!condition)
            condition = GetComponent<IQuestPassCondition>();
    }

    public void StartQuest()
    {
        Status = QuestStatus.QUEST_STARTED;

        if (condition == null)
        {
            Debug.LogError("Invalid quest item on " + this.gameObject.name);
            return;
        }

        condition.OnQuestStarted(this);
    }

    public void OnQuestConditionChanged()
    {
        if (!this.enabled) return;

        if (condition != null)
        {
            if (condition.isPassed())
            {
                Status = QuestStatus.QUEST_FINISHED;

                onQuestFinished.Invoke();
                return;
            }
        }
    }
}
