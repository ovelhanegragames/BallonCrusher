using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Caixa : MonoBehaviour {

    public Animator anim;
    public GameObject balao1_local, balao2_local, balao3_local, balao4_local, balao5_local, confete1_local, confete2_local;
    public GameObject balao1, balao2, balao3, confete1, confete2;
    public List<GameObject> balloonSpot;
    public List<GameObject> confeteSpot;
    List<Color32> cores = new List<Color32>();
    public int y;
    int numberOfBalloons = 0;
    int numberOfConfetes = 0;
    int index = 0;
    bool isReady = false;
    public GameObject gui;

    // Use this for initialization
    void Start () {
        gui.SetActive(false);
        anim = GetComponent<Animator>();
        //balao1.GetComponent<SpriteRenderer>().color = new Color32 (150, 30, 200, 255);
        //balao2.GetComponent<SpriteRenderer>().color = new Color32(10, 130, 200, 255);
        //balao3.GetComponent<SpriteRenderer>().color = new Color32(0, 200, 50, 255);
        cores.Add(new Color32(150, 30, 200, 255));
        cores.Add(new Color32(10, 130, 200, 255));
        cores.Add(new Color32(0, 200, 50, 255));
        cores.Add(new Color32(241, 96, 223, 255));
        cores.Add(new Color32(255, 53, 0, 255));

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.CompareTag("caixa"))
            {
             StartCoroutine(Balao());                
                anim.SetTrigger("Abre");
                StartCoroutine(Balao());
                StartCoroutine(Confete());
                //Instantiate(confete1, confete1_local.transform.position, confete1_local.transform.rotation);
                //Instantiate(confete2, confete2_local.transform.position, confete2_local.transform.rotation);
            }
        }

        //termina a animação da caixa e mostra o menu principal
        if (isReady)
        {
            anim.SetTrigger("Fade");
        }
       
    }

    public void ActiveGui()
    {
        gui.SetActive(true);
        gameObject.SetActive(false);
    }
    
    IEnumerator Balao()
    {
        yield return new WaitForSeconds(0.5f);
        //Instantiate(balao1, balao1_local.transform.position, balao1_local.transform.rotation);
        //Instantiate(balao2, balao2_local.transform.position, balao2_local.transform.rotation);
        //Instantiate(balao3, balao3_local.transform.position, balao3_local.transform.rotation);
        balao1.GetComponent<SpriteRenderer>().color = cores[Mathf.RoundToInt(Random.Range(0, cores.Count))];
        Instantiate(balao1, balloonSpot[Mathf.RoundToInt(Random.Range(0, balloonSpot.Count))].transform.position, 
            balloonSpot[Mathf.RoundToInt(Random.Range(0, balloonSpot.Count))].transform.rotation);
        numberOfBalloons++;
        if(numberOfBalloons > 5)
        {
            isReady = true;
            StopCoroutine(Balao());
        }
        else
        {
            StartCoroutine(Balao());
        }

    }
    IEnumerator Confete()
    {
        yield return new WaitForSeconds(.2f);
        Instantiate(confete1, confeteSpot[index].transform.position,confeteSpot[index].transform.rotation);
        index++;
        if (index >= confeteSpot.Count)
        {

            StopCoroutine(Confete());
        }
        else
        {
            StartCoroutine(Confete());
        }
    }
}
