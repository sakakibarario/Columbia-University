using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Vector3 pos = new Vector3(-2.108f, 3.31f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Click()
    {  
        if(gameObject.tag == "KeyButton1")
        {
            Debug.Log("click1");
            Instantiate(Number1,pos, Quaternion.identity);
        }

        
        if (gameObject.tag == "KeyButton2")
        {
            Debug.Log("click2");
            Instantiate(Number2, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton3")
        {
            Debug.Log("click2");
            Instantiate(Number3, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton4")
        {
            Debug.Log("click4");
            Instantiate(Number4, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton5")
        {
            Debug.Log("click2");
            Instantiate(Number5, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton6")
        {
            Debug.Log("click6");
            Instantiate(Number6, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton7")
        {
            Debug.Log("click7");
            Instantiate(Number7, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton8")
        {
            Debug.Log("click8");
            Instantiate(Number8, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton9")
        {
            Debug.Log("click9");
            Instantiate(Number9, pos, Quaternion.identity);
        }

        if (gameObject.tag == "KeyButton0")
        {
            Debug.Log("click0");
            Instantiate(Number0, pos, Quaternion.identity);
        }
    }
}
