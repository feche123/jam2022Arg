using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rb;

    [Header("StateFollow")]
    [SerializeField] float visionDistance;
    [SerializeField] float timeFollow;
    [SerializeField] bool isOnVision;

    private void Update()
    {
        DistanceVision();
    }

    void DistanceVision()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < visionDistance)
        {
            isOnVision = true;
            StateFollow();
        }
        else
        {
            isOnVision = false;
            StateIdle();
        }
    }
    void StateFollow()
    {
        if (LeanTween.isTweening()) return;
        LeanTween.value(0, 1, timeFollow).setOnUpdate((float value) =>
        {
            Vector2 dir = player.transform.position - transform.position;
            rb.velocity = dir;
        }).setOnComplete(_ => { rb.velocity = Vector2.zero;});
    }

    void StateIdle()
    {
        if (LeanTween.isTweening()) return;
        LeanTween.move(gameObject, new Vector2(transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), transform.position.y + UnityEngine.Random.Range(-0.1f, 0.1f)), 0.1f).setEase(LeanTweenType.easeShake).setLoopPingPong(-1)
            .setOnCompleteOnRepeat(true).setOnComplete(_=> { LeanTween.cancel(gameObject); });
    }





    /// <summary>
    /// Para que se ejecute FramebyFrame [timesToRepeat= -1]
    /// </summary>
    /// <param name="stateActual"></param>
    /// <param name="timesToRepeat"></param>
    /// <returns></returns>
    IEnumerator EjecutarStadoActual(Action stateActual,float timesToRepeat)
    {
        if (timesToRepeat == -1)
        {
            for (int i = 0; i > timesToRepeat; i++)
            {
                stateActual();
                yield return null;
            }
        }
        else
        {
            for(int i = 0; i < timesToRepeat; i++)
            {
                stateActual();
                yield return null;
            }
        }
        
    }
}
