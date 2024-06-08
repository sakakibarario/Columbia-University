using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject player;
    public bool OnBox = true;
    // Start is called before the first frame update
    void Start()
    {
        RandomQestion.BeginnerQuestionFlag = true;
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
          
          
        }
        if (RandomQestion.AdvancedQuestionFlag == true)
        {
           
        }
    }
   
}
