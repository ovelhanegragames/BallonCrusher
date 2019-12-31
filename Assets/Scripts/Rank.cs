using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Rank : MonoBehaviour {

    public GameObject playerName;
    public GameObject playerStatistics;
    
    Player player;
    GameObject gm;
    GameObject cb;
    GameObject dbr;

    // Use this for initialization
    void Start () {
        player = new Player();
        gm = GameObject.Find("Manager");
        cb = GameObject.Find("Watcher");
        dbr = GameObject.Find("Rank");

        if (gm.GetComponent<GameManager>().isKids)
        {

        }
        else
        {
            playerStatistics.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void PrintPlayerInfos(Player currentPlayer)
    {
        playerName.SetActive(false);
        playerStatistics.SetActive(true);
        if(currentPlayer != null)
        {
            playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Position:........" + currentPlayer.Position + "\n" +
                "Score:..........." + currentPlayer.Score.ToString() + "\n" +
                "Level:............" + currentPlayer.MaxLevel.ToString() + "\n" +
                "Combo:........" + currentPlayer.NumberMaxCombo.ToString();
        }
        else
        {
            playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Position:........" + "OFR" + "\n" +
                "Score:..........." + dbr.GetComponent<DB_Rank>().playerList[5].Score.ToString() + "\n" +
                "Level:............" + dbr.GetComponent<DB_Rank>().playerList[5].MaxLevel.ToString() + "\n" +
                "Combo:........" + dbr.GetComponent<DB_Rank>().playerList[5].NumberMaxCombo.ToString();
        }

    }
}
