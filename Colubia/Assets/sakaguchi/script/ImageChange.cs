using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChange : MonoBehaviour
{
    public GameObject BarGraphick;
    public GameObject HomeButton;
    public GameObject GoButton;

    bool ButtonFlag = false;
    bool OneClickFlag = false;

    Vector3 position = new Vector3(0.0f, 5.0f, 0.0f);
    Vector3 scale = new Vector3(1.2f, 1.2f, 0.0f);

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        HomeButton.SetActive(false);
        OneClickFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void OnClick()
    {
        StartCoroutine(ImagePosition());
    }

    IEnumerator ImagePosition()
    {
        if (OneClickFlag == true)
        {
            OneClickFlag = false;
            for (int i = 0; i < 10; i++)
            {
                BarGraphick.transform.position += position * 0.01f;
                BarGraphick.transform.localScale += scale * 0.01f;
                audioSource.PlayOneShot(audioSource.clip);

            }
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 10; i++)
            {
                BarGraphick.transform.position += position * 0.01f;
                BarGraphick.transform.localScale += scale * 0.01f;
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 10; i++)
            {
                BarGraphick.transform.position += position * 0.01f;
                BarGraphick.transform.localScale += scale * 0.01f;
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 10; i++)
            {
                BarGraphick.transform.position += position * 0.01f;
                BarGraphick.transform.localScale += scale * 0.01f;
            }

            yield return new WaitForSeconds(0.5f);


            Destroy(BarGraphick.gameObject);
            GoButton.SetActive(false);
            ButtonFlag = true;
            if (ButtonFlag == true)
            {
                HomeButton.SetActive(true);
            }
            Debug.Log(ButtonFlag);
        }
        yield break;

    }
}
