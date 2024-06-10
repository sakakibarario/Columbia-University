using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeImage : MonoBehaviour
{
    public GameObject Safe1;
    public GameObject Safe2;
    public GameObject Safe3;

    // Start is called before the first frame update
    void Start()
    {
        Safe1.gameObject.SetActive(false);
        Safe2.gameObject.SetActive(false);
        Safe3.gameObject.SetActive(false);
        Check_Now_Degree_Of_Difficulty();
    }

    void Check_Now_Degree_Of_Difficulty()
    {
        if (RandomQestion.BeginnerQuestionFlag == true)
        {
            Safe1.gameObject.SetActive(true);
        }
        if (RandomQestion.IntermediateQestionFlag == true)
        {
            Safe2.gameObject.SetActive(true);
        }
        if (RandomQestion.AdvancedQuestionFlag == true || RandomQestion.TutorialFlag ==true)
        {
            Safe3.gameObject.SetActive(true);
        }
    }
}
