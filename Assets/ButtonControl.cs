using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

    public bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveState();
	}

    public void MoveState()
    {
        if (!flag)
        {
            transform.position += Vector3.back * Time.deltaTime;
            flag = true;
        }
        else
        {
            transform.position += Vector3.forward * Time.deltaTime;
            flag = false;
        }
    }
}
