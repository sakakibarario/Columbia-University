using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQestion : MonoBehaviour
{
    static public bool BeginnerQuestionFlag = false;
    static public bool IntermediateQestionFlag = false;
    static public bool AdvancedQuestionFlag = false;

    public GameObject QuestionText;

    // Start is called before the first frame update
    void Start()
    {
        //BeginnerQuestionFlag = true;
        //IntermediateQestionFlag = false;
        //AdvancedQuestionFlag = false;       
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

        if(IntermediateQestionFlag)//íÜãâ
        {
            int rand = Random.Range(1, 3);

            switch(rand)
            {
                case 1:
                    int[] Intermediate1 = new int[4] { 8, 7, 4, 6 };
                    RandomQuestion(Intermediate1);
                    break;

                case 2:
                    int[] Intermediate2 = new int[4] { 1, 7, 3, 9 };
                    RandomQuestion(Intermediate2);
                    break;

                case 3:
                    break;
            }
        }

        if(AdvancedQuestionFlag)//è„ãâ
        {
            int rand = Random.Range(1, 3);

            switch(rand)
            {
                case 1:
                    int[] Advanced1 = new int[4] { 1, 5, 1, 5 };
                    RandomQuestion(Advanced1);
                    break;

                case 2:
                    int[] Advanced2 = new int[4] { 0, 5, 0, 5 };
                    RandomQuestion(Advanced2);
                    break;

                case 3:
                    int[] Advanced3 = new int[4] { 1, 1, 8, 0 };
                    RandomQuestion(Advanced3);
                    break;
            }
        }
    }

    public void RandomQuestion(int[] Q)
    {
        Debug.Log("a");

        BeginnerQuestionFlag = false;
        IntermediateQestionFlag = false;
        AdvancedQuestionFlag = false;
        for (int i = 0; i < 4; i++)
        {
            KeyPad_Program.answer[i] = Q[i];
            Debug.Log(KeyPad_Program.answer[i]);
        }
    }
}
