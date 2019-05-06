using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    GameObject om;
	// Use this for initialization
	void Start () {
        om = GameObject.Find("OptionsManager");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().volume = om.GetComponent<OptionsManager>().bgmVolume;
	}
}
