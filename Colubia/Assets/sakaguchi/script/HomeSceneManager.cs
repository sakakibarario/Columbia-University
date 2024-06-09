using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    AudioSource audioSource;

    //public GameObject Stage1;
    //public GameObject Stage2;
    //public GameObject Stage3;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        RandomQestion.BeginnerQuestionFlag = false;
        RandomQestion.IntermediateQestionFlag = false;
        RandomQestion.AdvancedQuestionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void Click()
   {
        if(gameObject.tag == "Stage1")
        {
            Debug.Log("Stage1");
            RandomQestion.BeginnerQuestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.BeginnerQuestionFlag);
        }

        if (gameObject.tag == "Stage2")
        {
            Debug.Log("Stage2");
            RandomQestion.IntermediateQestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.IntermediateQestionFlag);
        }

        if (gameObject.tag == "Stage3")
        {
            Debug.Log("Stage3");
            RandomQestion.AdvancedQuestionFlag = true;
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.AdvancedQuestionFlag);
        }
    }
}
