using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMover : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float fPlayerSpeed = 0.0f;
    [SerializeField, Range(0f, 1f)] private float fPlayerPositionBias;
    [SerializeField] private bool bLockYAxis = true;
    [SerializeField] private bool bMoveWithMouse = false;
    [Header("Object dependencies")]
    [SerializeField] private Camera mainCamera;

    public Animator animator;
    private Vector2 _inputVector;
    private bool _faceRight = true;


    private Vector3 vDesiredPosition = Vector2.zero;
    private bool bShouldMoveToDesired = false;
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

        animator = GetComponent<Animator>();
    }

    public void MoveToPoint(Vector3 destination)
    {
        movePlayerToPoint((Vector3) destination);
        bShouldMoveToDesired = true;
    }

    private void MovePlayerToComputedPositions()
    {
        if (bShouldMoveToDesired && Vector2.Distance(transform.position, vDesiredPosition) > fPlayerPositionBias)
        {
            transform.position = Vector2.MoveTowards(transform.position, vDesiredPosition, Time.deltaTime * fPlayerSpeed);
        }
        else
        {
            bShouldMoveToDesired = false;
        }
    }

    private void MovePlayerWithKeyboard()
    {
        float horizontalAspect = Input.GetAxis("Horizontal");
        if (horizontalAspect != 0)
            bShouldMoveToDesired = false;
        transform.position += new Vector3(horizontalAspect, 0, 0) * Time.deltaTime * fPlayerSpeed;
    }

    private void Update()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");

        MovePlayerToComputedPositions();    // Point-click system impl
        MovePlayerWithKeyboard();           // WASD
        animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        ReflectPlayer();
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fPlayerPositionBias);
    }

    private void ReflectPlayer()
    {
        if (_inputVector.x > 0 && _faceRight == false ||
            _inputVector.x < 0 && _faceRight == true)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

            _faceRight = !_faceRight;
        }
    }
}
