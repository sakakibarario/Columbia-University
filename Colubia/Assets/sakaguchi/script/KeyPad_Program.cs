using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class KeyPad_Program : MonoBehaviour
{
    public GameObject Number0;
    public GameObject Number1;
    public GameObject Number2;
    public GameObject Number3;
    public GameObject Number4;
    public GameObject Number5;
    public GameObject Number6;
    public GameObject Number7;
    public GameObject Number8;
    public GameObject Number9;

    Vector3 pos1 = new Vector3(-2.1f, 3.31f, 0.0f);
    Vector3 pos2 = new Vector3(-0.9f, 3.31f, 0.0f);
    Vector3 pos3 = new Vector3(0.285f, 3.31f, 0.0f);
    Vector3 pos4 = new Vector3( 1.48f, 3.31f, 0.0f);

    Vector3 pos;

    public static int clickCount = 0;

    int[] answer = new int[4]{ 1, 2, 3, 4 };
    int[] p_answer = new int[4]{ 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        p_answer[0] = 3;
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }


    public void Click()
    {
        if (clickCount == 0)
            pos = pos1;
        if (clickCount == 1)
            pos = pos2;
        if (clickCount == 2)
            pos = pos3;
        if (clickCount == 3)
            pos = pos4;

        if(clickCount >= 3)
        {
            Debug.Log(p_answer[0]);
            Debug.Log(p_answer[1]);
            Debug.Log(p_answer[2]);
            Debug.Log(p_answer[3]);
            CheckAnswer();
        }

        if (gameObject.tag == "KeyButton1")
        {
            Instantiate(Number1, pos, Quaternion.identity);
            p_answer[clickCount] = 1;
            Debug.Log(p_answer[0]);
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton2")
        {
            Instantiate(Number2, pos, Quaternion.identity);           
            p_answer[clickCount] = 2;
            Debug.Log(p_answer[0]);
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton3")
        {
            Instantiate(Number3, pos, Quaternion.identity);
            p_answer[clickCount] = 3;
            Debug.Log(p_answer[0]);
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton4")
        {
            Instantiate(Number4, pos, Quaternion.identity);           
            p_answer[clickCount] = 4;
            Debug.Log(p_answer[0]);
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton5")
        {
            Instantiate(Number5, pos, Quaternion.identity);            
            p_answer[clickCount] = 5;
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton6")
        {
            Instantiate(Number6, pos, Quaternion.identity);           
            p_answer[clickCount] = 6;
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton7")
        {
            Instantiate(Number7, pos, Quaternion.identity);           
            p_answer[clickCount] = 7;
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton8")
        {
            Instantiate(Number8, pos, Quaternion.identity);            
            p_answer[clickCount] = 8;
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton9")
        {
            Instantiate(Number9, pos, Quaternion.identity);           
            p_answer[clickCount] = 9;
            clickCount++;
        }
        else if (gameObject.tag == "KeyButton0")
        {
            Instantiate(Number0, pos, Quaternion.identity);           
            p_answer[clickCount] = 0;
            clickCount++;
        }
    }

    void CheckAnswer()
    {
        for(int i = 0; i < 4; i++)
        {

            Debug.Log(i);
            if (answer[i] != p_answer[i])
            {
                Debug.Log("ïsê≥â");
                break;
            }
           
            if (i >= 3)
            {
                Debug.Log("ê≥â");
            }
        }
    }
}
