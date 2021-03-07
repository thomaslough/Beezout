using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public BeeController bee;

    void OnEnable()
    {
        TouchArea.OnTouchAction += DidTouch;
        TouchArea.OnUnTouchAction += DidUnTouch;
        TouchArea.OnTouchMoveAction += DidMove;
    }

    void OnDisable()
    {
        TouchArea.OnTouchAction -= DidTouch;
        TouchArea.OnUnTouchAction -= DidUnTouch;
        TouchArea.OnTouchMoveAction -= DidMove;
    }

    void DidTouch()
    {
        Debug.Log("DidTouch");
        bee.DoTheThing();
    }

    void DidUnTouch()
    {
        Debug.Log("DidUnTouch");
    }

    void DidMove(Vector3 target)
    {
        Debug.Log("DidMove: " + target.ToString("F4"));
    }
}