using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Rank : MonoBehaviour {

    public static DB_Rank instance = null;
    public List<Player> playerList = new List<Player>();
    List<string> listOfNames = new List<string>();
    List<string> listOfScore = new List<string>();
    List<string> listOfCombo = new List<string>();
    List<string> listOfLevel = new List<string>();



    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(this);

        listOfNames.Add("primeiro");
        listOfNames.Add("segundo");
        listOfNames.Add("terceiro");
        listOfNames.Add("quarto");
        listOfNames.Add("quinto");

        listOfScore.Add("p.score");
        listOfScore.Add("s.score");
        listOfScore.Add("t.score");
        listOfScore.Add("q.score");
        listOfScore.Add("qq.scoreo");

        listOfCombo.Add("p.combo");
        listOfCombo.Add("s.combo");
        listOfCombo.Add("t.combo");
        listOfCombo.Add("q.combo");
        listOfCombo.Add("qq.combo");

        listOfLevel.Add("p.level");
        listOfLevel.Add("s.level");
        listOfLevel.Add("t.level");
        listOfLevel.Add("q.level");
        listOfLevel.Add("qq.level");

        playerList.Add(new Player("Luis", 800, 5, 3));
        playerList.Add(new Player("Felipe", 600, 5, 3));

        LoadList();

    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public Player ArrangePlayerRank()
    {
        Player pl = new Player();

        if (playerList.Count == 1)
        {
            playerList[0].Position = 1;
            return playerList[0];
        }

        //ordena maior score
        for (int i = 1; i < playerList.Count; i++)
        {
            for (int j = 0; j < playerList.Count - i; j++)
            {
                if (playerList[j].Score < playerList[j + 1].Score)
                {
                    pl = playerList[j];
                    playerList[j] = playerList[j + 1];
                    playerList[j + 1] = pl;
                }
            }
        }

        //atribui a posição
        for (int i = 1; i <= playerList.Count; i++)
        {
            playerList[i].Position = i;
        }

        return null;

    }

    public bool SaveList()
    {
        for(int i = 0; i < listOfNames.Count; i++)
        {
            if (i >= playerList.Count) return true;
            PlayerPrefs.SetString(listOfNames[i],playerList[i].Nome);
            PlayerPrefs.SetInt(listOfScore[i], playerList[i].Score);
            PlayerPrefs.SetInt(listOfCombo[i], playerList[i].NumberMaxCombo);
            PlayerPrefs.SetInt(listOfLevel[i], playerList[i].MaxLevel);
        }
        return true;
    }

    public bool LoadList()
    {
        for(int i = 0; i < listOfNames.Count; i++)
        {
            if (PlayerPrefs.GetString(listOfNames[i]) == null)
            {
                print("lista vazia");
                return true;
            }
                
            playerList.Add(new Player(PlayerPrefs.GetString(listOfNames[i]), PlayerPrefs.GetInt(listOfCombo[i]), PlayerPrefs.GetInt(listOfLevel[i]), PlayerPrefs.GetInt(listOfScore[i])));
        }
        print("Lista Carregada");
        return true;
    }
}
