using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int[] a = new int[5];

  
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(a[0]);
        Debug.Log(a[1]);
        Debug.Log(a[2]);
        Debug.Log(a[3]);
        Debug.Log(a[4]);
        a[0] = 10;
        a[3] = -1;
        Debug.Log(a[0]);
        Debug.Log(a[1]);
        Debug.Log(a[2]);
        Debug.Log(a[3]);
        Debug.Log(a[4]);
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
