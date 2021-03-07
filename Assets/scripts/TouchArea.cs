using UnityEngine;
using System.Collections;

public class TouchArea : MonoBehaviour
{
    public delegate void TouchStart();
    public static event TouchStart OnTouchStart;
    public delegate void TouchEnd();
    public static event TouchEnd OnTouchEnd;
    public delegate void TouchMove(Vector3 target);
    public static event TouchMove OnTouchMove;
    public Camera cam;

    private Vector3 target;
    private bool touched;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touched = true;
            OnTouchStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touched = false;
            OnTouchEnd();
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

            OnTouchMove(target);
        }
    }
}
