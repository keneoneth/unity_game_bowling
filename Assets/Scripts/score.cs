using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{

    public static int trialno;
    public Text scoreText;
    private static int marks;
    private static int scorefactor = 10;
    private static int missfactor = 10;
    private static int penaltyfactor = 2;
    private static bool reset = false;
    private static int lastmarks = 0;
    private static int increpenalty = 0;

    // Start is called before the first frame update
    void Start()
    {
        trialno = 0;
        marks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // add one trial if launched
        if (reset && !movement.launched)
        {
            reset = false;

            if (lastmarks == marks)
            {
                increpenalty += 1;
            }
            lastmarks = calmarks();
        }


        if (!reset && movement.launched)
        {
            trialno += 1;
            reset = true;

          //  Debug.Log("mm" + lastmarks.ToString() + " " + marks.ToString());
;           
        }

        marks = calmarks();




        if (monitor.fallcount == 10 && !movement.launched)
        {
            //reset game
            trialno = 0;
            marks = 0;
            lastmarks = 0;
            increpenalty = 0;
        }

        scoreText.text = "Trial     :  " + trialno.ToString() +"\n" + "Scores :  " + marks.ToString();




    }

    int calmarks()
    {
        return monitor.fallcount * scorefactor / ((trialno >= 2) ? penaltyfactor : 1) - increpenalty * missfactor;
    }
}
