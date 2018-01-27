using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobControl : MonoBehaviour {

    public int value = 0;
    public int position = 0;
    GameObject TheGameController = null;
    int objPos = 0;
    string knobNum;


    // Use this for initialization
    void Start() {

        TheGameController = GameObject.FindGameObjectWithTag("GameController");
        //TheGameController = TheGameController.GetComponent<GameManager>();

    }

    public void KnobNumber()
    {
        if (TheGameController.GetComponent<GameManager>())
        {
          //  knobNum = TheGameController.
        }
    }

    public void Increment()
    {
        value++;
        value = value % 10;

        switch (position)
        {
            case 0:

                if (TheGameController != null)
                    TheGameController.SendMessage("SetTenths", value);
                break;
            case 1:
                if (TheGameController != null)
                    TheGameController.SendMessage("SetOnes", value);

                break;
            case 2:
                if (TheGameController != null)
                    TheGameController.SendMessage("SetTens", value);

                break;

            default:
                if (TheGameController != null)
                    TheGameController.SendMessage("SetHundreds", value);

                break;
        }
    }
}
