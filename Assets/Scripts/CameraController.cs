using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            float destinationX = math.lerp(transform.position.x, playerTransform.position.x, fSpeed * Time.deltaTime);
            transform.position = new Vector3(destinationX, transform.position.y, transform.position.z);
            isMoving = Math.Abs(playerTransform.position.x - transform.position.x) > fStopRange;
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

    public bool ResolvePlayerOnScene()
    {
        Player playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (playerRef)
        {
            this.playerTransform = playerRef.transform;
            return true;
        }

        playerRef = GameObject.FindAnyObjectByType<Player>();
        if (playerRef)
        {
            this.playerTransform = playerRef.transform;
            return true;
        }

        return false;
    }
#endif
}
