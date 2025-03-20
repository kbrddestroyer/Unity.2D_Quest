using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTEController : MonoBehaviour
{
    [SerializeField] private KeyCode[] codes;
    [SerializeField, Range(0f, 10f)] private float delay;
    [SerializeField, Range(0f, 1f)] private float timeBoundToPress;
    [SerializeField] private UnityEvent onSuccess;

    private uint keyInSequence = 0;
    private float deltaTime = 0f;
 
    private void Fail()
    {
        keyInSequence = 0;
        deltaTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > delay + timeBoundToPress)
        {
            Fail();
            return;
        }

        if (Input.GetKeyDown(codes[keyInSequence]))
        {
            if (deltaTime <= deltaTime - timeBoundToPress)
            {
                Fail();
                return;
            }

            // Dispatch successfull press event
            keyInSequence++;
            deltaTime = 0f;

            onSuccess.Invoke();
        }
    }
}
