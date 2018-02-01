using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public GameObject PasswordForm;
    public string CurrentChannel = "";
    public Text theDial;
    Camera camera;
    Renderer rend;
    public GameObject startObject;
    public InputField myText;
    Vector3 pos1;
    Vector3 pos2;
    string objName;

    public GameObject GameOverObject = null;
    public Image myImage;
    public Text imageText;

    public List<GameObject> RandomSounds;

    public List<GameObject> TheDigits; 

    int[] values = new int[4];

    bool firstRun = true;

    string MakeNumString()
    {
        string s = "" + values[3].ToString() + values[2].ToString() + values[1].ToString() + "." + values[0].ToString();
        return s;
    }

    
    string CurrentBand
    {
        get
        {
            if (AmFm.amBool)
                return "AM";
            else
                return "FM";
        }
    }

    public static bool GameOver = false;

    public void EnteredAPassword()
    {
        if (string.Equals(myText.text.ToUpper(), "GEMINI")) 
        {
             StopCurrentSound();        

             Debug.Log("Correct!");
            PlayingSound = GameObject.Find("WELLDONE");
            AudioSource audio = PlayingSound.GetComponent<AudioSource>();

            if (audio != null)
            {
                Debug.Log(PlayingSound.name);
                audio.enabled = true;
            }
            myText.text = "";
            GameOver = true;
            if (GameOverObject != null)
                GameOverObject.SetActive(true);
        }
        else  
        {
             StopCurrentSound();        
             Debug.Log("Wrong!");
            PlayingSound = GameObject.Find("WRONG");
            AudioSource audio = PlayingSound.GetComponent<AudioSource>();
            if (audio != null)
            {
                Debug.Log(PlayingSound.name);
                audio.enabled = false;
                audio.enabled = true;
                PlayingSound = null;
            }
            myText.text = "";

        }

        PasswordForm.SetActive(false);
    }

    void UpdateDisplay()
    {
        if (GameOver)
            return;

        CurrentChannel = values[3].ToString() + values[2].ToString() + values[1].ToString() + "."+ values[0].ToString()  + CurrentBand;
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

    GameObject PlayingSound = null;
    string lastChannelToPlay = "x";

    void PlayRandomSound()
    {
        int SoundIndex = Random.Range(0, RandomSounds.Count);
        Debug.Log("Playing random sound " + SoundIndex);

        if (SoundIndex > 0 && SoundIndex < RandomSounds.Count)
        {
        GameObject obj = RandomSounds[SoundIndex];

        if (obj != null)
            {
            AudioSource audio = obj.GetComponent<AudioSource>();
            if (audio!=null)
                {
                    audio.enabled = true;
                    PlayingSound = obj;
                    lastChannelToPlay = CurrentChannel; 
                 }
            }
        }
    }

    string LastBand = "";

  void StopCurrentSound()
  {



          if (PlayingSound!=null )
            {
            AudioSource audio = PlayingSound.GetComponent<AudioSource>();
            if (audio!=null)
                audio.enabled = false;
            }
          PlayingSound = null;
            CurrentChannel = "";
  }

    void DoSomethingWithChannel()
     { 
        

        if (CurrentChannel != lastChannelToPlay || LastBand != CurrentBand)
        {

             StopCurrentSound();        

          string newSoundName = "Sound"+MakeNumString();
                newSoundName += CurrentBand;
           
          Debug.Log("Finding Sound: "+newSoundName);
          PlayingSound = GameObject.Find(newSoundName);
          Debug.Log("Found: "+PlayingSound);
          if (PlayingSound!=null)
          {
            AudioSource audio = PlayingSound.GetComponent<AudioSource>();
            if (audio != null)
            {
            Debug.Log("Playing: "+PlayingSound.name);
            audio.enabled = true;
            lastChannelToPlay = CurrentChannel;
            LastBand = CurrentBand;
            }
        }
          else
          {
            PlayRandomSound();
          }


        }
     }


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

    
    public void BandHasChanged(string FreqBand)
    {
        Debug.Log("Band has changed to " + CurrentBand);
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
        if (GameOver)
        {
            myImage.gameObject.SetActive(true);
            Color c = myImage.color;
            Color d = imageText.color;
            c.a += (.2f * Time.deltaTime);
            d.a += (.15f * Time.deltaTime);
            myImage.color = c;
            imageText.color = d;
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            Debug.Log("Win");
        }

        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameOver = false;
               Scene loadedLevel = SceneManager.GetActiveScene ();
                SceneManager.LoadScene (loadedLevel.buildIndex);           
            }
            return;

        }


        if (firstRun)
        {
            firstRun = false;
            UpdateDisplay();
        }

        if (Input.GetMouseButtonDown(0))
        {
            startObject = ObjectAtMouse();

            if (startObject !=null  && startObject.name == "terminal")
            {
                myText.text = "";
                PasswordForm.SetActive(true);
            }

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
