using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject rankPanel;

	// Use this for initialization
	void Start () {
        rankPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseApp()
    {
        Application.Quit();
    }

    public void PlayTheGame()
    {
        SceneManager.LoadScene("play");
    }

    public void OpenRank()
    {
        //rankPanel.SetActive(true);
        SceneManager.LoadScene("Kids");
    }

}
