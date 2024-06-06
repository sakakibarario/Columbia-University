using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGunController : MonoBehaviour
{
    PlayerController playerController;
    static public Enemy_security_guard enemy_Security_Guard;
    static public Enemy_Strength_Security_Guard enemy_Strength_Security_Guard;
    static public Warning_area warning_area;

    public bool checkInArea = false;

    public bool strong = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy_security_guard" || collision.gameObject.tag == "Enemy_Strength_Security_Guard")
        {
            Debug.Log("enter");
            checkInArea = true;

            if (collision.gameObject.tag == "Enemy_security_guard")
            {
                enemy_Security_Guard = collision.GetComponent<Enemy_security_guard>();
                strong = false;
            }
            else if (collision.gameObject.tag == "Enemy_Strength_Security_Guard")
            {
                enemy_Strength_Security_Guard = collision.GetComponent<Enemy_Strength_Security_Guard>();
                strong = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy_security_guard" || collision.gameObject.tag == "Enemy_Strength_Security_Guard")
        {
            Debug.Log("exit");
            checkInArea = false;

            //enemy_Security_Guard = null;
            //enemy_Strength_Security_Guard = null;
        }
    }

}
