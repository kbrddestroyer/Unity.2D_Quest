using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mergable : MonoBehaviour, IDragHandler
{
    public struct MergeStruct
    {
        Mergable with;
        Mergable output;
    }
    [SerializeField] private MergeStruct[] merges;
    [SerializeField, Range(0f, 10f)] private float fDistanceOnGui;

    private Vector3 position;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        position = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            OnDragStop();
    }

    public void DispatchMergeEvent()
    {

    }
    private void Merge()
    {

    }

    private void OnDragStop()
    {
        // Check merge

        transform.position = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fDistanceOnGui);
    }
}
