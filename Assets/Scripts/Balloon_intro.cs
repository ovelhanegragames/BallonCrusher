using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_intro : MonoBehaviour {

    
     public float vel;



	// Use this for initialization
	void Move () {
       

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * vel * Time.deltaTime);
    }
}
