using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmFm : MonoBehaviour
{

    public int value = 0;
    public int position = 0;
    GameObject TheGameController = null;
    int objPos = 0;
    string knobNum;

    public float targetAngle = 45;
    public float rotationSpeed = 60;

    public float MinAngle = 45;
    public float MaxAngle = 315;
    public Animator anim;
    public static bool amBool = false;
    public static bool fmBool = false;

    public float PushOffset = 0.005f;


    public float StartPosition;
    public float OnPosition
    {
        get
        {
            return StartPosition - PushOffset;
        }
    }

    public Vector3 targetPosition;

    public float PushSpeed = 1f;
    bool oldState = false;

    private bool State
    {
        get

        {
            if (position % 2 == 0)
            {
                return amBool;

            }
            return fmBool;
        }
    }

    public bool amOn
    {
        get { return amBool; }
    }

    public bool fmOn
    {
        get { return fmBool; }
    }



    // Use this for initialization
    void Start()
    {
        StartPosition = transform.localPosition.z;

        targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z );
        //  anim = GameObject.Find("radio").GetComponent<Animator>();
        TheGameController = GameObject.FindGameObjectWithTag("GameController");
        amBool = true;
        fmBool = false;
        SetPushState(State);
    }



    private void Update()
    {
        if (oldState != State)
        {
            Debug.Log(gameObject.name + " state changed to " + State);
            SetPushState(State);
            oldState = State;
        }

            if (Vector3.Distance(transform.position, targetPosition) > Mathf.Abs(PushOffset / 100))
             {
            Vector3 newPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, PushSpeed * Time.deltaTime);
            transform.localPosition = newPosition;
            }
    }

    void SetPushState(bool state)
    {
        if (state)
            targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, OnPosition);
        else
            targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, StartPosition);
        Debug.Log("New " + gameObject.name + " position = " + targetPosition.z);
    }


    void NotifyController()
    {
        if (TheGameController != null)
        {
            if (amBool)
                TheGameController.SendMessage("BandHasChanged", "AM", SendMessageOptions.DontRequireReceiver);
            else
                TheGameController.SendMessage("BandHasChanged", "FM", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Increment()
    {

        Debug.Log("Changing Button?");
        switch (position)
        {
            case 4:
                
                if (TheGameController != null && amBool == false )
                {
                    Debug.Log("turning on AM");
                    amBool = true;
                    fmBool = false;
                    SetPushState(amBool);
                    NotifyController();
                    break;
                }
                break;
            case 5:
                if (TheGameController != null && fmBool == false )
                {
                    Debug.Log("Turning on FM");
                    fmBool = true;
                    amBool = false;
                    Debug.Log("FM ON");
                    SetPushState(fmBool);
                    NotifyController();
                    break;
                }
                break;

            default:
                if (TheGameController != null)
                    break;

                break;
        }
        
    }


}
