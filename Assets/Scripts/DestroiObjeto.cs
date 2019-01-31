using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiObjeto : MonoBehaviour {
    public float tempo;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, tempo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
