using UnityEngine;
using System.Collections;

public class TouchArea : MonoBehaviour
{
    public delegate void TouchAction();
    public static event TouchAction OnTouchAction;
    public delegate void UnTouchAction();
    public static event UnTouchAction OnUnTouchAction;
    public delegate void TouchMoveAction(Vector3 target);
    public static event TouchMoveAction OnTouchMoveAction;
    public Camera cam;

    private Vector3 target;
    private bool touched;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touched = true;
            OnTouchAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touched = false;
            OnUnTouchAction();
        }

        if (touched)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                //target.z = 4f;
                //target.z -= -3.0f;
                //target.y -= -0.1f;
            }

            OnTouchMoveAction(target);
        }

        Debug.Log("target: " + target.ToString("F4"));
    }
}
