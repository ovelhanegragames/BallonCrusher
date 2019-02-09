using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour {

    public GameObject playerName;
    public GameObject playerStatistics;
    public List<Player> playerList = new List<Player>();
    Player player;
    GameObject gm;
    GameObject cb;


    // Use this for initialization
    void Start () {
        player = new Player();
        playerStatistics.SetActive(false);
        gm = GameObject.Find("Manager");
        cb = GameObject.Find("Watcher");

        playerList.Add(new Player("nome", 500, 5, 3));
        playerList.Add(new Player("nome", 600, 5, 3));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetPlayerName()
    {
        playerList.Add(MakePlayer());
        //Player pl = new Player();
        //pl = ArrangePlayerRank();
        //PrintPlayerInfos(pl);
        PrintPlayerInfos(ArrangePlayerRank());
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

    public Player ArrangePlayerRank()
    {
        Player pl = new Player();

        if(playerList.Count == 1)
        {
            playerList[0].Position = 1;
            return playerList[0];
        }

      //ordena maior score
        for(int i = 1; i < playerList.Count; i++)
        {
            print("I:" + i.ToString());
            for(int j = 0; j < playerList.Count - i; j++)
            {
                print("J:" + j.ToString());
                if (playerList[j].Score < playerList[j+1].Score)
                {
                    print("score alterado");
                    pl = playerList[j];
                    playerList[j] = playerList[j+1];
                    playerList[j+1] = pl;
                }
            }
        }

        //atribui a posição
        for(int i = 1; i <= playerList.Count; i++)
        {
            playerList[i].Position = i;
        }

        //compara o nome do jogador da list para devolver seu rank
        foreach(Player p in playerList)
        {
            if (p.Nome.Equals(playerName.transform.GetChild(2).gameObject.GetComponent<Text>().text))
            {
                return p;
            }
        }

        return null;
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


}
