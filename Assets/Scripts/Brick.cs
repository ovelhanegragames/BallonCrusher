﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            GameObject gm = GameObject.Find("Manager");
            gm.GetComponent<GameManager>().gameState = "gameover";
        }
        if(collision.gameObject.tag == "tnt")
        {
            Destroy(this.gameObject);
        }
    }
}
