using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGunController : MonoBehaviour
{
    PlayerController playerController;
    Enemy_security_guard enemy_Security_Guard;
    enemyenemy en;

    public bool checkInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy_Security_Guard = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_security_guard>();
        en = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyenemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ikuhdsfohs");
            checkInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("i");
            checkInArea = false;
        }
    }


    //public IEnumerator stun()
    //{
    //    if (checkInArea && playerController.onFire)
    //    {
    //        Debug.Log("kkkkk");
    //        playerController.Battery -= 1;
    //        en.enabled = false;

    //        yield return new WaitForSecondsRealtime(4.0f);

    //        en.enabled = true;
    //    }
    //}
}
