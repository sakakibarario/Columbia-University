using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    GameObject player;

    //マーク用
    public GameObject Exclamation_mark;
    public GameObject Question_mark;

    //SE用
    AudioSource AudioSource;
    public AudioClip ExclamationAudio;
    public AudioClip QuestionAuidoname;
    private bool AudioFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioFlag = true;
        Exclamation_mark.SetActive(false);
        Question_mark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Player　のゲームオブジェクトを得る
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.x < player.transform.position.x)
        {
            Question_mark.transform.localScale = new Vector2(-0.7f, 0.7f);//左向き
        }
        else if (transform.position.x > player.transform.position.x)
        {
            Question_mark.transform.localScale = new Vector2(0.7f, 0.7f);//左向き
        }


        if (Enemy_Strength_Security_Guard.isActive)//主人公発見時
        {
            Exclamation_mark.SetActive(true);

            if(AudioFlag)
            {
                //オーディオ再生
                AudioSource.PlayOneShot(ExclamationAudio, 1.0f);
                AudioFlag = false;
            }
        }
        else
        {
            Exclamation_mark.SetActive(false);
        }

        if(Enemy_Strength_Security_Guard.EMove_Stop_mark)//主人公見失い時
        {
             Question_mark.SetActive(true);

            if (!AudioFlag)
            {
                //オーディオ再生
                AudioSource.PlayOneShot(QuestionAuidoname, 1.0f);
                AudioFlag = true;
            }
        }
        else
        {
            Question_mark.SetActive(false);
        }
    }
}
