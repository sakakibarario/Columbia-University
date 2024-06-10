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
        RandomQestion.TutorialFlag = false;
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
            // Playingˆ—‚ğŒÄ‚Ô
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.BeginnerQuestionFlag);
        }

        if (gameObject.tag == "Stage2")
        {
            Debug.Log("Stage2");
            RandomQestion.IntermediateQestionFlag = true;
            // Playingˆ—‚ğŒÄ‚Ô
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.IntermediateQestionFlag);
        }

        if (gameObject.tag == "Stage3")
        {
            Debug.Log("Stage3");
            RandomQestion.AdvancedQuestionFlag = true;
            // Playingˆ—‚ğŒÄ‚Ô
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log(RandomQestion.AdvancedQuestionFlag);
        }

        if (gameObject.tag == "Demo")
        {
            Debug.Log("Demo");
            RandomQestion.TutorialFlag = true;
            // Demoˆ—‚ğŒÄ‚Ô
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Demo);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
