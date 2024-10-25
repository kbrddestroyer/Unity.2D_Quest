using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance { get => instance; }

    [Header("Base Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(0f, 10f)] private float fSpeed;
    [SerializeField, Range(0f, 10f)] private float fMinRange;
    [SerializeField, Range(0f, 1f)] private float fStopRange;
    [Header("Gizmos")]
    [SerializeField] private Color gizmoColor = new Color(0f, 0f, 0f, 1f);

    private bool isMoving = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) >= fMinRange || isMoving)
        {
            // Change camera position
            transform.position = Vector3.Slerp(transform.position, playerTransform.position + Vector3.back * 10, fSpeed * Time.deltaTime);
            isMoving = (Vector2.Distance(playerTransform.position, transform.position) > fStopRange);
        }
    }

#if UNITY_EDITOR || UNITY_EDITOR_64
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, fMinRange);
        Gizmos.DrawWireSphere(transform.position, fStopRange);
        Handles.Label(transform.position + Vector3.up * 2f, $"Reaction distance is {fMinRange}");
    }
#endif
}
