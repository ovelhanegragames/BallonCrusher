using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        playerStatistics.SetActive(false);
        gm = GameObject.Find("Manager");
        cb = GameObject.Find("Watcher");
        dbr = GameObject.Find("Rank");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //cria um objeto do tipo player com os dados da respectiva partida e adiciona na lista de rank
    public void GetPlayerName()
    {
        dbr.GetComponent<DB_Rank>().playerList.Add(MakePlayer());

        PrintPlayerInfos(dbr.GetComponent<DB_Rank>().ArrangePlayerRank());

    }

    private Player MakePlayer()
    {
        Player pl = new Player();
        pl.Nome = playerName.transform.GetChild(2).gameObject.GetComponent<Text>().text;
        pl.NumberMaxCombo = cb.GetComponent<Combo>().comboCountMax;
        pl.MaxLevel = gm.GetComponent<GameManager>().level;
        pl.Score = gm.GetComponent<GameManager>().score;

        return pl;
    }


    public void PrintPlayerInfos(Player currentPlayer)
    {
        playerName.SetActive(false);
        playerStatistics.SetActive(true);
        playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
            "Position:........" + currentPlayer.Position + "\n" +
            "Score:..........." + currentPlayer.Score.ToString() + "\n" +
            "Level:............" + currentPlayer.MaxLevel.ToString() + "\n" +
            "Combo:........" + currentPlayer.NumberMaxCombo.ToString();
    }

    public Player SearchPlayer()
    {
        //compara o nome do jogador da list para devolver seu rank
        foreach (Player p in dbr.GetComponent<DB_Rank>().playerList)
        {
            if (p.Nome.Equals(playerName.transform.GetChild(2).gameObject.GetComponent<Text>().text))
            {
                return p;
            }
        }
        return null;
    }


}
