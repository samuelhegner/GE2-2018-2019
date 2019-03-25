using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiState {
    public LabTest owner;
    public virtual void Enter() { }
    public virtual void Think() { }
    public virtual void Exit() { }
}

public class LabTest : MonoBehaviour
{
    public AiState current;
    public AiState previous;

    public int updatesPerSeconds;

    public IEnumerator coroutine;

    public bool coroutineRunning;

    void OnEnable()
    {
        StartCoroutine(Think());
    }

    public void ChangeStateDelay(AiState newState, float delay) {
        coroutine = ChangeStateCoroutine(newState, delay);
        StartCoroutine(coroutine);
    }

    public void StopChangeStateDelay() {
        if (coroutineRunning)
        {
            StopCoroutine(coroutine);
        }
        else
        {
            Debug.LogWarning("No delayed change state is running");
        }
    }

    IEnumerator ChangeStateCoroutine(AiState newState, float delay) {
        coroutineRunning = true;
        yield return new WaitForSeconds(delay);
        ChangeState(newState);
        coroutineRunning = false;
    }

    public void ReturnToPreviousState() {
        if (previous != null)
        {
            ChangeState(previous);
        }
        else
        {
            Debug.LogWarning("There is no previous state");
        }
    }

    public void ChangeState(AiState newState) {
        previous = current;

        if (current != null) {
            current.Exit();
        }

        current = newState;
        current.owner = this;
        current.Enter();
    }

    IEnumerator Think() {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        while (true) {
            if (current != null) {
                current.Think();
            }
            yield return new WaitForSeconds(1f / (float)updatesPerSeconds);
        }
    }

 
}
