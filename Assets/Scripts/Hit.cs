using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

    float timer = 0;
    public bool isActive;
    GameObject wt;

	// Use this for initialization
	void Start () {
        wt = GameObject.Find("Watcher");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= .15f)
        {
            if (wt.GetComponent<Combo>().comboIsActive)
            {
                //wt.SendMessage("BonusScoreCombo");
            }
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "redZone")
        {
            isActive = true;
        }
    }
}
