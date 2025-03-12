using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipFactory : MonoBehaviour
{
    [SerializeField] private GameObject m_tooltipPrefab;
    [SerializeField] private GameObject m_tooltipSceneRoot;

    public void CreateTooltip(string message)
    {
        Tooltip tooltip = Instantiate(m_tooltipPrefab, m_tooltipSceneRoot.transform).GetComponent<Tooltip>();

        if (!tooltip)
        {
            Debug.LogError("Could not create tooltip with messagge " + message);
            return;
        }
    
        tooltip.Text = message;
        tooltip.State = true;
    }
}
