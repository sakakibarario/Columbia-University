using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsHide : MonoBehaviour
{
    public GameObject Tipshide;
    //SE用
    AudioSource AudioSource;
    public AudioClip ButtonSE;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
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
            //オーディオ再生
            AudioSource.PlayOneShot(ButtonSE, 1.5f);
            Tipshide.SetActive(false);
        }
    }
}
