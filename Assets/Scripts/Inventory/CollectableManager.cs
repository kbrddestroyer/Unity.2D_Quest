using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CollectableManager : MonoBehaviour
{
    private static CollectableManager instance;
    public static CollectableManager Instance { get => instance; }

    private List<Collectable> lCollectables = new List<Collectable>();

    [SerializeField, Range(0f, 10f)] private float fDistance;
    [SerializeField] private KeyCode collectKey = KeyCode.E;

    private void Awake()
    {
        instance = this;
        lCollectables = FindObjectsOfType<Collectable>().ToList<Collectable>();
    }

    private void Update()
    {
        Collectable closest;
        if (closest = FindClosest())
        {
            if (Input.GetKeyDown(collectKey))
            {
                CollectItem(closest);
            }
        }
    }

    private Collectable FindClosest()
    {
        Collectable closest = null;
        float fClosestDistance = 0;
        foreach (Collectable item in lCollectables)
        {
            float fDistance = Vector2.Distance(transform.position, item.transform.position);
            if (!closest || fClosestDistance > fDistance)
            {
                closest = item;
                fClosestDistance = fDistance;
            }
        }

        return closest;
    }

    private void CollectItem(Collectable item)
    {
        lCollectables.Remove(item);
        item.OnCollect();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
    }
#endif
}
