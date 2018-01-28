using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobControl : MonoBehaviour {

    public int value = 0;
    public int position = 0;
    GameObject TheGameController = null;
    int objPos = 0;
    string knobNum;

    public float targetAngle = 45;
    public float rotationSpeed = 60;

    public float MinAngle = 45;
    public float MaxAngle = 360;

    // Use this for initialization
    void Start() {

        TheGameController = GameObject.FindGameObjectWithTag("GameController");
        //TheGameController = TheGameController.GetComponent<GameManager>();

    }

    void Update()
    {
    	if (transform.localEulerAngles.y != targetAngle)
    	{
    		float newAngle = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, targetAngle, rotationSpeed*Time.deltaTime);
    		transform.localEulerAngles = new Vector3(transform.rotation.x, newAngle, transform.rotation.z);
    		Debug.Log(targetAngle);
    	}
    }


    public void KnobNumber()
    {
        if (TheGameController.GetComponent<GameManager>())
        {
          //  knobNum = TheGameController.
        }
    }

    float CalcAngle(int value)
    {
    	return MinAngle + ((MaxAngle - MinAngle) / 9 * value);
    }



    public void Increment()
    {
        value++;
        value = value % 10;
        targetAngle = CalcAngle(value);
        Debug.Log("Angle is " + targetAngle);
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
