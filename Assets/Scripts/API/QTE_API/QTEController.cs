using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class QTEController : MonoBehaviour
{
    [SerializeField] protected KeyCode[] codes;
    [SerializeField, Range(0f, 10f)] protected float delay;
    [SerializeField, Range(0f, 1f)] protected float timeBoundToPress;

    protected bool completed = false;
    protected bool started = false;

    protected uint keyInSequence = 0;
    protected float deltaTime = 0f;

    protected abstract void OnStartQTE();
    protected abstract void OnNextStage(uint index);
    protected abstract void OnUpdateDeltaTime(float deltaTime);
    protected abstract void OnFail();
    protected abstract void OnComplete();
    protected abstract void OnCancel();

    public void StartQTE()
    {
        if (completed) return;
        started = true;

        keyInSequence = 0;
        deltaTime = 0f;
        OnStartQTE();
        OnNextStage(keyInSequence);
    }

    private void Fail()
    {
        keyInSequence = 0;
        deltaTime = 0f;
        started = false;

        OnUpdateDeltaTime(0);
        OnFail();
        OnCancel();
    }

    // Update is called once per frame
    protected void Update()
    {
        deltaTime += Time.deltaTime;
        
        if (started)
            OnUpdateDeltaTime(deltaTime);

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

            if (keyInSequence >= codes.Length)
            {
                OnComplete();
                completed = true;
                started = false;
                keyInSequence = 0;
                return;
            }
            OnNextStage(keyInSequence);
        }
    }
}
