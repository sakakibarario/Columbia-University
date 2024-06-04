using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{

    //public GameObject Stage1;
    //public GameObject Stage2;
    //public GameObject Stage3;

    // Start is called before the first frame update
    void Start()
    {
        //RandomQestion.BeginnerQuestionFlag = false;
        //RandomQestion.IntermediateQestionFlag = false;
        //RandomQestion.AdvancedQuestionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void Click()
   {
        if(gameObject.tag == "Stage1")
        {
           
            RandomQestion.BeginnerQuestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            Debug.Log(RandomQestion.BeginnerQuestionFlag);
        }

        if (gameObject.tag == "Stage2")
        {
            RandomQestion.IntermediateQestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            Debug.Log(RandomQestion.IntermediateQestionFlag);
        }

        if (gameObject.tag == "Stage3")
        {
            RandomQestion.AdvancedQuestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            Debug.Log(RandomQestion.AdvancedQuestionFlag);
        }
    }
}
