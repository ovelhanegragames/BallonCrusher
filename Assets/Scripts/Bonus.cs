using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public GameObject combo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GiveTheBonus()
    {
        print("terminou animaçaão");
        combo.SendMessage("GiveBonusToPlayer");
    }
}
