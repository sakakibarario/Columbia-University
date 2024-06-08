using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;

    public GameObject ClearStamp;
    public GameObject TitleButton;

    //SE用
    AudioSource AudioSource;
    public AudioClip ClearStampSE;
    private bool StampSEFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        StampSEFlag = true;
        stage1.gameObject.SetActive(false);
        stage2.gameObject.SetActive(false);
        stage3.gameObject.SetActive(false);

        ClearStamp.gameObject.SetActive(false);
        TitleButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //各ステージによって変更
        if (RandomQestion.BeginnerQuestionFlag == true)
        {
            stage1.gameObject.SetActive(true);
        }
        if (RandomQestion.IntermediateQestionFlag == true)
        {
            stage2.gameObject.SetActive(true);
        }
        if (RandomQestion.AdvancedQuestionFlag == true)
        {
            stage3.gameObject.SetActive(true);
        }

        //スタンプ表示
        if (StampSEFlag)
            StartCoroutine(Stamp());
    }

    IEnumerator Stamp()
    {
        StampSEFlag = false;

        yield return new WaitForSeconds(1.0f);
        //オーディオ再生
        AudioSource.PlayOneShot(ClearStampSE, 1.0f);
        yield return new WaitForSeconds(0.2f);
        //スタンプ表示
        ClearStamp.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        //ボタンの表示
        TitleButton.gameObject.SetActive(true);
        yield break;
    }

}
