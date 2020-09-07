using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movement : MonoBehaviour
{

    Vector3 originalPosition;
    Quaternion originalRotation;

    Vector3 firstmousepos;
    Vector3 secondmousepos;

    private bool pressed = false;
    public static bool launched = false;
    private static float force_factor = 5.0f;
    private static float z_force_factor = 10.0f;
    private static double vec_thres = 0.08d;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }


    void reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            launched = false;
            pressed = false;
            Debug.Log("reset");
        }
    }

    bool compare(float vec, double thres)
    {
        return Math.Abs(double.Parse(vec.ToString("F8"))) < thres;
    }

    // Update is called once per frame
    void Update()
    {

        if (launched && (transform.position.y < -10.0 || 
            (double.Parse(transform.position.y.ToString("F2")) == 0.14 &&
            compare(GetComponent<Rigidbody>().velocity.x, vec_thres) && compare(GetComponent<Rigidbody>().velocity.y, vec_thres) && compare(GetComponent<Rigidbody>().velocity.z, vec_thres) ) ))
        {
            reset();
        }else if (!launched)
        {
            //Debug.Log(Input.mousePosition.ToString("F16"));
            if (!pressed && Input.GetMouseButtonDown(0))
            {
                firstmousepos = Input.mousePosition;
                pressed = true;
                Debug.Log("pressed" + firstmousepos.ToString("F16"));
            }
            else if (pressed && Input.GetMouseButtonUp(0))
            {

                secondmousepos = Input.mousePosition;

                GetComponent<Rigidbody>().useGravity = true; // can start falling

                float forceX = secondmousepos.x - firstmousepos.x;
                float forceZ = secondmousepos.y - firstmousepos.y;

                Debug.Log("released" + secondmousepos.ToString("F16") +" "+ GetComponent<Rigidbody>().useGravity.ToString());

                GetComponent<Rigidbody>().AddForce(forceX * force_factor, 0, forceZ * z_force_factor); // Add a force
                launched = true;
            }
        }

    }
}
