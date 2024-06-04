using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsHide : MonoBehaviour
{
    public GameObject Tipshide;

    // Start is called before the first frame update
    void Start()
    {
        Tipshide.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (gameObject.tag == "TipsButton")
        {
            Tipshide.SetActive(false);
        }
    }
}
