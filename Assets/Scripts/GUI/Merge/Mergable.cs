using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mergable : InventoryItem, IDragHandler
{
    [Header("Mergable Schema")]
    [SerializeField] private MergeSchema merges;
    [Header("Settings")]
    [SerializeField, Range(0f, 10f)] private float fDistanceOnGui;
    [SerializeField] private uint id;


    private bool bDragged = false;
    private Vector3 position;
    private Camera mainCamera;

    public uint ID { get => id; }

    public override void OnRegistered()
    {
        mainCamera = Camera.main;
        position = GetComponent<RectTransform>().position;

        Mergables.Instance.RegisterMergable(this);
    }

    private void OnDestroy()
    {
        Mergables.Instance.UnregisterMergable(this);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            OnDragStop();
    }

    /// <summary>
    /// Handles merge event, validates closest mergable and calls Merge()
    /// </summary>
    public bool HandleMergeEvent()
    {
        Mergable closest = Mergables.Instance.GetClosest(this);
        if (!closest)
        {
            return false;
        }
        return Merge(closest);
    }

    /// <summary>
    /// Creates new mergable in target position, destroys used components
    /// </summary>
    /// <param name="newMergable">Prefab: mergable to create</param>
    /// <param name="with">GameObject: Second mergable</param>
    private void CreateNewMergable(Mergable newMergable, Mergable with)
    {
        GameObject createdMergable = Instantiate(newMergable.gameObject, with.transform.position, Quaternion.identity) as GameObject;

        createdMergable.transform.parent = with.transform.parent;

        Destroy(with.gameObject);
        Destroy(this.gameObject);
    }

    private bool Merge(Mergable with)
    {
        Mergable output = merges.Validate(this, with);
        if (!output) return false;
        CreateNewMergable(output, with);

        return true;
    }

    private void OnDragStop()
    {
        // Check merge
        if (!bDragged)
            return;
        bDragged = false;
        Debug.Log("Merge event dispatch");
        if (!this.HandleMergeEvent())
        {
            InventoryController.Instance.OnMergeFail();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
        bDragged = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fDistanceOnGui);
    }
#endif
}
