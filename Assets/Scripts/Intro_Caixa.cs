using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Caixa : MonoBehaviour {

    public Animator anim;
    public GameObject balao1_local, balao2_local, balao3_local, confete1_local, confete2_local;
    public GameObject balao1, balao2, balao3, confete1, confete2;

    public int y;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        balao1.GetComponent<SpriteRenderer>().color = new Color32 (150, 30, 200, 255);
        balao2.GetComponent<SpriteRenderer>().color = new Color32(10, 130, 200, 255);
        balao3.GetComponent<SpriteRenderer>().color = new Color32(0, 200, 50, 255);


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
                Instantiate(confete1, confete1_local.transform.position, confete1_local.transform.rotation);
                Instantiate(confete2, confete2_local.transform.position, confete2_local.transform.rotation);
            }
        }
       
    }
    
    IEnumerator Balao()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(balao1, balao1_local.transform.position, balao1_local.transform.rotation);
        Instantiate(balao2, balao2_local.transform.position, balao2_local.transform.rotation);
        Instantiate(balao3, balao3_local.transform.position, balao3_local.transform.rotation);
    }
}
