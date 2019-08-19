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
    string nameCurrentPlayer;


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

    //cria um objeto do tipo player com os dados da respectiva partida e adiciona na lista de rank
    public void GetPlayerName()
    {
        dbr.GetComponent<DB_Rank>().playerList[5] = MakePlayer();
        dbr.GetComponent<DB_Rank>().ArrangePlayerRank();
        PrintPlayerInfos(SearchPlayer());
        dbr.GetComponent<DB_Rank>().SaveList();

        for (int i = 0; i < dbr.GetComponent<DB_Rank>().playerList.Length - 1; i++)
        {
            print(dbr.GetComponent<DB_Rank>().playerList[i].Position.ToString());
            print(dbr.GetComponent<DB_Rank>().playerList[i].Nome);
            print(dbr.GetComponent<DB_Rank>().playerList[i].Score.ToString());
        }

    }

    private Player MakePlayer()
    {
        Player pl = new Player();
        pl.Nome = playerName.transform.GetChild(2).gameObject.GetComponent<Text>().text;
        pl.NumberMaxCombo = cb.GetComponent<Combo>().comboCountMax;
        pl.MaxLevel = gm.GetComponent<GameManager>().level;
        pl.Score = gm.GetComponent<GameManager>().score;

        nameCurrentPlayer = pl.Nome;

        return pl;
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

    public Player SearchPlayer()
    {
        //compara o nome do jogador da list para devolver seu rank
        //foreach (Player p in dbr.GetComponent<DB_Rank>().playerList)
       // {
        //    if (p.Nome.Equals(nameCurrentPlayer))
        //    {
        //        return p;
        //    }
       // }

        for (int i = 0; i < dbr.GetComponent<DB_Rank>().playerList.Length - 1; i++)
        {
            if (dbr.GetComponent<DB_Rank>().playerList[i].Nome.Equals(nameCurrentPlayer))
            {
                if(dbr.GetComponent<DB_Rank>().playerList[i].Score == gm.GetComponent<GameManager>().score)
                {
                    return dbr.GetComponent<DB_Rank>().playerList[i];
                }
                
            }
        }
            return null;
    }


}
