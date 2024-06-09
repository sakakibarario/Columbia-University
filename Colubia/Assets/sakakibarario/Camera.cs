using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject player;
    PlayerController PlayerController;

    public bool OnBox = false;
    // Start is called before the first frame update
    void Start()
    {
          PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player　のゲームオブジェクトを得る
        player = GameObject.FindGameObjectWithTag("Player");
        if (RandomQestion.BeginnerQuestionFlag == true)
        {
            if(OnBox)
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            else
            {             
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
        if (RandomQestion.IntermediateQestionFlag == true)
        {
           
             if (OnBox)
             {
                 if (PlayerController.isCeiling)
                 {
                      transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                 } 
                 else
                 {
                    if (PlayerController.isCeilingWalk)
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + player.transform.up.y * 2.0f, transform.position.z);
                    else
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + player.transform.up.y * 4.0f, transform.position.z);
                }   
             }
             else
             {
                 if (PlayerController.isCeiling)
                 {
                     transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                 } 
                 else
                 {
                    if(PlayerController.isCeilingWalk)
                        transform.position = new Vector3(transform.position.x, player.transform.position.y + player.transform.up.y * 2.0f, transform.position.z);
                    else
                        transform.position = new Vector3(transform.position.x, player.transform.position.y + player.transform.up.y * 4.0f, transform.position.z);
                 }
             }
        }
        if (RandomQestion.AdvancedQuestionFlag == true)
        {

            if (OnBox)
            {
                if (PlayerController.isCeiling)
                {
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                }
                else
                {
                    if (PlayerController.isCeilingWalk)
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + player.transform.up.y * 2.0f, transform.position.z);
                    else
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + player.transform.up.y * 4.0f, transform.position.z);
                }
            }
            else
            {
                if (PlayerController.isCeiling)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }
                else
                {
                    if (PlayerController.isCeilingWalk)
                        transform.position = new Vector3(transform.position.x, player.transform.position.y + player.transform.up.y * 2.0f, transform.position.z);
                    else
                        transform.position = new Vector3(transform.position.x, player.transform.position.y + player.transform.up.y * 4.0f, transform.position.z);
                }
            }
        }
    }
   
}
