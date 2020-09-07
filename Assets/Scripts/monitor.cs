using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monitor : MonoBehaviour
{   
    public static int fallcount = 0;
    private static double thresY = 0.2d;
    private bool fallen = false;

    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    void reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
        }
    }


    // Update is called once per frame
    void Update()
    {


        if (!fallen && double.Parse(transform.position.y.ToString("F1")) < thresY)
        {
            fallcount += 1;
            fallen = true;
            //Debug.Log("fc" + fallcount.ToString());
        }
        if (fallen && !movement.launched)
        {
            //next trial
            transform.localScale = new Vector3(0,0,0);
            
        }
        if (!fallen && !movement.launched)
        {
            //next trial
            reset();

        }



        //Debug.Log("check" + transform.gameObject.activeSelf.ToString() + score.trialno.ToString());

        if (transform.localScale.Equals(new Vector3(0, 0, 0)) && score.trialno == 0)
        {
            reset();
            fallen = false;
            fallcount = 0;
        }
    }
}
