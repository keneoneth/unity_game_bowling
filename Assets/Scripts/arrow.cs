using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private enum State
    {
        Original,
        Blinking,
        Disappear
    }

    private State state;
    private float disappearTime = 0.1f;
    private IEnumerator coroutine;
    public Image im;

    // Start is called before the first frame update
    void Start()
    {
        if (state == State.Original)
        {
            state = State.Blinking;

        }

        coroutine = blinkImage();
        StartCoroutine(coroutine);

    }

    // Update is called once per frame
    void Update()
    {

        state = movement.launched ? State.Disappear : State.Original;

        im.enabled = !(state == State.Disappear);

        if (state == State.Original)
        {
            state = State.Blinking;
            coroutine = blinkImage();
            StartCoroutine(coroutine);
        }
        
    }


    private IEnumerator blinkImage()
    {
        while (state == State.Blinking)
        {
            im.enabled = !im.enabled; //appear or disappear

            yield return new WaitForSeconds(disappearTime);

            //Debug.Log($"my state is {state}");
        }
    }


}
