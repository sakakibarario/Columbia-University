using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad_ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter()
    {
        Debug.Log("color");
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 70);
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
    }
}
