using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuestPassCondition : MonoBehaviour
{
    public abstract void OnQuestStarted(Quest quest);

    public abstract bool isPassed();
}
