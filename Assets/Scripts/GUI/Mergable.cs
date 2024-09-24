using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mergable : MonoBehaviour, IDragHandler
{
    [System.Serializable]
    public struct MergeStruct
    {
        public Mergable with;
        public Mergable output;
    }

    [SerializeField] private MergeStruct[] merges;
    [SerializeField, Range(0f, 10f)] private float fDistanceOnGui;
    [SerializeField] private uint id;

    private bool bDragged = false;
    private Vector3 position;
    private Camera mainCamera;

    public uint ID { get => id; set => id = value; }

    private void Start()
    {
        mainCamera = Camera.main;
        position = transform.position;

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

    public void DispatchMergeEvent()
    {
        Mergable closest = Mergables.Instance.GetClosest(this);
        Debug.Log(closest.gameObject);
        Merge(closest);

        transform.position = position;
    }

    private void CreateNewMergable(Mergable newMergable, Mergable with)
    {
        GameObject createdMergable = Instantiate(newMergable.gameObject, with.transform.position, Quaternion.identity) as GameObject;

        createdMergable.transform.parent = with.transform.parent;

        Destroy(with.gameObject);
        Destroy(this.gameObject);
    }

    public void Merge(Mergable with)
    {
        foreach (MergeStruct mergeRule in merges)
        {
            if (with.id == mergeRule.with.id)
            {
                CreateNewMergable(mergeRule.output, with);
            }
        }
        foreach (MergeStruct mergeRule in with.merges)
        {
            if (this.id == mergeRule.with.id)
            {
                CreateNewMergable(mergeRule.output, with);
            }
        }
    }

    private void OnDragStop()
    {
        // Check merge
        if (!bDragged)
            return;
        bDragged = false;
        Debug.Log("Merge event dispatch");
        this.DispatchMergeEvent();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
        bDragged = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fDistanceOnGui);
    }
}
