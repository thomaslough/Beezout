using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beezout;

/**
 * Lessons:
 * 1. Scripts are mini-programs that operate on a GameObject in your scene.
 * 2. How to get a component of the GameObject this script is attached to?
 * 3. How to modify that component over time.
 * 4. FIX IN CLASS: There is actually a shortcut to get the transform of the GameObject.
 **/


public class BeeController : MonoBehaviour
{

    // Public variables show up in the Inspector
    public Vector3 RotateSpeed = new Vector3(10.0F, 10.0F, 10.0F);
    public Vector3 WobbleAmount = new Vector3(0.1F, 0.1F, 0.1F);
    public Vector3 WobbleSpeed = new Vector3(0.5F, 0.5F, 0.5F);
    public Camera cam;
    public float speed = 1.5f;

    // Private variables do not show up in the Inspector
    private Transform tr;
    private Vector3 BasePosition;
    private Vector3 NoiseIndex = new Vector3();

    private Vector3 target;

    private string beeState = Constants.HOVER_AT_BASE;

    // Use this for initialization
    void Start()
    {

        // https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
        tr = GetComponent("Transform") as Transform;

        BasePosition = tr.position;

        NoiseIndex.x = Random.value;
        NoiseIndex.y = Random.value;
        NoiseIndex.z = Random.value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beeState == Constants.HOVER_AT_BASE)
        {
            Hover();
        }
        else if (beeState == Constants.RETURN_TO_BASE)
        {
            transform.position = Vector3.Lerp(transform.position, BasePosition, 0.1f);

            if (System.Math.Round(transform.position.z, 4) == System.Math.Round(BasePosition.z, 4))
            {
                beeState = Constants.HOVER_AT_BASE;
            }
            //Debug.Log("return to base tr: " + transform.position.ToString("F4") + " base: " + BasePosition.ToString("F4"));
        }
        else if (beeState == Constants.TRACK_TOUCH_POINT)
        {
            transform.position = target;
        }
    }

    void Hover()
    {

        // 1. ROTATE
        // Rotate the cube by RotateSpeed, multiplied by the fraction of a second that has passed.
        // In other words, we want to rotate by the full amount over 1 second
        // tr.Rotate (Time.deltaTime * RotateSpeed);


        // 2. WOBBLE
        // Calculate how much to offset the cube from it's base position using PerlinNoise
        Vector3 offset = new Vector3();
        offset.x = Mathf.PerlinNoise(NoiseIndex.x, 0) - 0.5F;
        offset.y = Mathf.PerlinNoise(NoiseIndex.y, 0) - 0.5F;
        offset.z = Mathf.PerlinNoise(NoiseIndex.z, 0) - 0.5F;

        offset.Scale(WobbleAmount);

        // Set the position to the BasePosition plus the offset
        transform.position = BasePosition + offset;

        // Increment the NoiseIndex so that we get a new Noise value next time.
        NoiseIndex += WobbleSpeed * Time.deltaTime;
    }

    public void GoToTouchPoint(Vector3 newTarget)
    {
        //Debug.Log("GoToTouchPoint" + newTarget.ToString("F4"));
        beeState = Constants.TRACK_TOUCH_POINT;
        target.y -= -0.2f;
        target = newTarget;
    }

    public void ReturnToBase()
    {
        beeState = Constants.RETURN_TO_BASE;
        transform.position = Vector3.Lerp(transform.position, BasePosition, 0.1f);
    }

    public void HoverAtBase()
    {
        //Debug.Log("HoverAtBase ");
        beeState = Constants.HOVER_AT_BASE;
    }

    public void DoTheThing()
    {
        Debug.Log("DoTheThing !!!");
    }
}