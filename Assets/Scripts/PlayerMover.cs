using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMover : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float fPlayerSpeed = 0.0f;
    [SerializeField, Range(0f, 1f)] private float fPlayerPositionBias;
    [SerializeField] private bool bLockYAxis = true;
    [Header("Object dependencies")]
    [SerializeField] private Camera mainCamera;

    private Vector3 vDesiredPosition = Vector2.zero;

    #region PRIVATES
    private void movePlayerToPoint(Vector2 vPoint)
    {
        // bLockYAxis locks Y axis to transform.position when set to true
        vDesiredPosition = (bLockYAxis) ? new Vector2(vPoint.x, transform.position.y) : vPoint; 
    }
    #endregion

    private void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, vDesiredPosition) > fPlayerPositionBias)
        {
            transform.position = Vector2.MoveTowards(transform.position, vDesiredPosition, Time.deltaTime * fPlayerSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseSceenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseSceenPosition);

            movePlayerToPoint(mouseWorldPosition);
            Debug.Log(mouseWorldPosition);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fPlayerPositionBias);
    }
}
