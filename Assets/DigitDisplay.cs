using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDisplay : MonoBehaviour {

	public List<GameObject> TheDigitImages;
	public int CurrentValue;

	public void SetValue(int newValue)
	{
		CurrentValue = newValue % 10;
		if (CurrentValue < TheDigitImages.Count)
		{
			foreach (GameObject g in TheDigitImages)
			{
				g.SetActive(false);
			}

			TheDigitImages[CurrentValue].SetActive(true);
		}

	}

	// Use this for initialization
	void Start () {
		SetValue(CurrentValue);		
	}
	
}
