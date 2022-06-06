using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NaviController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rb;
    // Follow 
    [SerializeField] float visionDistance;
    bool asd;

    // StateMachine
    public State actualState;
    int actionCounter;

    public enum State
    {
        idle,follow
    }
    private void Update()
    {
        DistanceVision();
        DoState();
    }

    void DoState()
    {
        switch (actualState)
        {
            case State.idle:
                StateIdle();
                break;
            case State.follow:
                StateFollow();
                break;
            default:
                break;
        }
    }
    void DistanceVision()
    {
        if(Vector2.Distance(transform.position,player.transform.position)< visionDistance)
        {
            ChangesState(State.follow);
        }
        else
        {
            ChangesState(State.idle);
        }
    }
    void ChangesState(State newState)
    {
        actionCounter = 0;
        LeanTween.pauseAll();
        actualState = newState;
        LeanTween.resumeAll();
    }
    IEnumerator DoForTime(Action action, int time)
    {
        float timer = 0;
        while(timer < time)
        {
            action();
            timer += Time.deltaTime;
            yield return null;
        }
    }
    void DoOneTime (Action action)
    {
        if(actionCounter<1)
        {
            action();
            actionCounter++;
        }
    }
    void StateIdle()
    {
        if (LeanTween.isPaused()) return;
        LeanTween.move(gameObject, new Vector2(transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), transform.position.y + UnityEngine.Random.Range(-0.1f, 0.1f)), 0.1f).setEase(LeanTweenType.easeShake).setLoopPingPong(-1);
    }
    void StateFollow ()
    {
            if (LeanTween.isPaused()) return;
            LeanTween.value(0, 1, 2).setOnUpdate((float value) =>
            {
                Vector2 dir = player.transform.position - transform.position;
                rb.velocity = dir;
            }).setOnComplete(_ => { rb.velocity = Vector2.zero; });
    }
       
}
