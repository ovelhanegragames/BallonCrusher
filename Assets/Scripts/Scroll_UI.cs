using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll_UI : MonoBehaviour {

    public RawImage back;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        back.uvRect = new Rect(0.03f * Time.time, 0, 1, 1);

    }
}
