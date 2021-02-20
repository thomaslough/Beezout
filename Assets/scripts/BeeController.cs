﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool mouseIsDown;
    private Vector3 target;


    // Use this for initialization
    void Start()
    {

        // https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
        tr = GetComponent("Transform") as Transform;

        BasePosition = tr.position;

        NoiseIndex.x = Random.value;
        NoiseIndex.y = Random.value;
        NoiseIndex.z = Random.value;

        mouseIsDown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseIsDown = true;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                //target.z = 4f;
                //target.z -= -3.0f;
                target.y -= -0.1f;
            }



        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseIsDown = false;
        }

        //Debug.Log("mouseIsDown" + mouseIsDown);

        if (!mouseIsDown)
        {
            Hover();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target, 0.1f);
            //transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            BasePosition = target;
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
}