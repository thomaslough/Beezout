using UnityEngine;
using System;

public class Main : MonoBehaviour
{
    public BeeController bee;

    void OnEnable()
    {
        TouchArea.OnTouchStart += DidTouchStart;
        TouchArea.OnTouchEnd += DidTouchEnd;
        TouchArea.OnTouchMove += DidTouchMove;
    }

    void OnDisable()
    {
        TouchArea.OnTouchStart -= DidTouchStart;
        TouchArea.OnTouchEnd -= DidTouchEnd;
        TouchArea.OnTouchMove -= DidTouchMove;
    }

    void DidTouchStart()
    {
        bee.DoTheThing();
    }

    void DidTouchEnd()
    {
        bee.ReturnToBase();
    }

    void DidTouchMove(Vector3 target)
    {
        bee.GoToTouchPoint(target);
        //Debug.Log("DidTouchMove: " + target.ToString("F4"));
    }
}