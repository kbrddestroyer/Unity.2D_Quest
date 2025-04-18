using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Mergables : MonoBehaviour, CommonInterfaces.ISingleton
{
    #region SINGLETON
    private static Mergables instance;
    public static Mergables Instance { get => instance; protected set => instance = value; }
    #endregion

    public List<Mergable> mergables;
    [SerializeField, Range(0f, 100f)] private float fDistanceToMerge;

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("RuntimeError: Cannot create two singleton classes.");
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void RegisterMergable(Mergable mergable)
    {
        if (!mergables.Contains(mergable))
        {
            mergables.Add(mergable);
        }
    }

    public void UnregisterMergable(Mergable mergable)
    {
        if (mergables.Contains(mergable))
        {
            mergables.Remove(mergable);
        }
    }

    public Mergable GetClosest(Mergable related)
    {
        Mergable closest = null;
        float fClosestDistance = 0.0f;
        foreach (Mergable mergable in mergables)
        {
            if (mergable.Equals(related))
                continue;

            float fDistance = Vector2.Distance(mergable.transform.position, related.transform.position);
            if (!closest || fDistance < fClosestDistance)
            {
                closest = mergable;
                fClosestDistance = fDistance;
            }
        }

        if (fClosestDistance > fDistanceToMerge)
            return null;
        return closest;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistanceToMerge);
    }
#endif
}
