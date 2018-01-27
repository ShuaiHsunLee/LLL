using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float CurrentChannel = 0;
    public Text theDial;
    Camera camera;
    Renderer rend;
    public GameObject startObject;
    Vector3 pos1;
    Vector3 pos2;
    string objName;

    public List<GameObject> TheDigits; 

    int[] values = new int[4];

    bool firstRun = true;

    string MakeNumString()
    {
        string s = "" + values[3].ToString() + values[2].ToString() + values[1].ToString() + "." + values[0].ToString();
        return s;
    }


    void UpdateDisplay()
    {
        CurrentChannel = values[3] * 100 + values[2] * 10 + values[1] +  (float)values[0] / 10.0f;
        if (theDial != null)
        {
            theDial.text = MakeNumString();
            Debug.Log(CurrentChannel);
        }

        for (int i = 0; i < values.Length; i++)
        {
            if (i < TheDigits.Count)
            {
                    TheDigits[i].SendMessage("SetValue", values[i]);
            }
        }


        DoSomethingWithChannel();
    }
    void DoSomethingWithChannel() { }
    public void SetTenths(int value)
    {
        values[0] = value;
        UpdateDisplay();
    }


    public void SetOnes(int value)
    {
        values[1] = value;
        UpdateDisplay();
    }

    public void SetTens(int value)
    {
        values[2] = value;
        UpdateDisplay();

    }


    public void SetHundreds(int value)
    {
        values[3] = value;
        UpdateDisplay();

    }



    void Start()
    {
        var cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject)
        {
            camera = cameraObject.GetComponent<Camera>();
        }
        rend = GetComponent<MeshRenderer>();
        if (rend)
        {
            rend.enabled = false;
        }
    }
    void Update()
    {

        if (firstRun)
        {
            firstRun = false;
            UpdateDisplay();
        }

        if (Input.GetMouseButtonDown(0))
        {
            startObject = ObjectAtMouse();
            Debug.Log("Start at " + startObject);

            if (startObject != null)
            {
                startObject.SendMessage("Increment", SendMessageOptions.DontRequireReceiver);
                //startObject.SendMessage("KnobNumber", SendMessageOptions.DontRequireReceiver);


                AudioSource audio = startObject.GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.Play();
                }

            }
            //pos1 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.5f);
            //pos1 = Camera.main.ScreenToWorldPoint(pos1);
            //pos2 = pos1;
            //GameController.SendMessage("ClickedRadioObject", startObject);
        }
        if (Input.GetMouseButton(0))
        {

            pos2 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.5f);
            pos2 = Camera.main.ScreenToWorldPoint(pos2);
        }
        else
        {
            if (rend)
            {
                rend.enabled = false;

            }
        }
    }
        

    GameObject ObjectAtMouse()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);     // Gets the mouse position in the form of a ray.
        RaycastHit hit;
        GameObject theObject = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {        // Then see if an object is beneath us using raycasting.
            var objectTrans = hit.transform;     // If we hit an object then hold on to the object.
            theObject = objectTrans.gameObject;
        }
        return theObject;
    }

}
