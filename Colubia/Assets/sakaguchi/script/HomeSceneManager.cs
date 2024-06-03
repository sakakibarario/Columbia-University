using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{

    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;


    // Start is called before the first frame update
    void Start()
    {
        RandomQestion.BeginnerQuestionFlag = false;
        RandomQestion.IntermediateQestionFlag = false;
        RandomQestion.AdvancedQuestionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if(gameObject.tag == "Stage1")
            {
                RandomQestion.BeginnerQuestionFlag = true;
                SceneManager.LoadScene("Stage1");
            }

            if (gameObject.tag == "Stage2")
            {
                RandomQestion.IntermediateQestionFlag = true;
                SceneManager.LoadScene("Stage2");
            }

            if (gameObject.tag == "Stage3")
            {
                RandomQestion.AdvancedQuestionFlag = true;
                SceneManager.LoadScene("sakaguchi");
            }
        }
    }
}
