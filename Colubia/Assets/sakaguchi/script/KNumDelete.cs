using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNumDelete : MonoBehaviour
{
    static public bool deleteFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deleteFlag)
        {
            KeyPad_Program.zero++;
            Destroy(this.gameObject, 0.7f);        }
    }
}
