using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    public GameObject LadderF;

    PlayerController PlayerCTRL;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCTRL = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerCTRL.onLadder == false && collision.gameObject.tag == "Player") 
            LadderF.SetActive(true);// Žæ“¾‚µ‚½obj‚ð•\Ž¦‚³‚¹‚é
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            LadderF.SetActive(false);// Žæ“¾‚µ‚½obj‚ð”ñ•\Ž¦‚É‚·‚é
    }

}
