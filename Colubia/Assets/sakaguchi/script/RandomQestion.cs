using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQestion : MonoBehaviour
{
    static public bool BeginnerQuestionFlag = false;
    static public bool IntermediateQestionFlag = false;
    static public bool AdvancedQuestionFlag = false;

    int[] QQQ = new int[4] {0,0,0,0};

    //íÜãâ

    //è„ãâ

    // Start is called before the first frame update
    void Start()
    {
        BeginnerQuestionFlag = true; 


    }

    // Update is called once per frame
    void Update()
    {
        if(BeginnerQuestionFlag)//èâãâ
        {          
            int rand = Random.Range(1,3);
            
            switch(rand)
            {
                case 1:
                    int[] beginner1 = new int[4] { 8, 7, 5, 3 };
                    RandomQuestion(beginner1);
                    break;

                case 2:
                    int[] beginner2 = new int[4] { 2, 9, 8, 3 };
                    RandomQuestion(beginner2);
                    break;

                case 3:
                    int[] beginner3 = new int[4] { 4, 8, 4, 2 };
                    RandomQuestion(beginner3);
                    break;

                    
            }
            
            
        }

        if(IntermediateQestionFlag)
        {

        }

        if(AdvancedQuestionFlag)
        {

        }
    }

    public void RandomQuestion(int[] Q)
    {
        BeginnerQuestionFlag = false;
        for (int i=0;i<4;i++)
        {
            QQQ[i] = Q[i];
            Debug.Log(QQQ[i]);
        }
    }
}
