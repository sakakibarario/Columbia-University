using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryAndEndButtonProgram : MonoBehaviour
{
    //SE用
    AudioSource AudioSource;
    public AudioClip ButtonSE;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEnd()
    {
        //オーディオ再生
        AudioSource.PlayOneShot(ButtonSE, 1.5f);
        //ゲームの状態をhomeに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }

    public void OnClickRetry()
    {
        //オーディオ再生
        AudioSource.PlayOneShot(ButtonSE, 1.5f);
        //ゲームの状態をplayingに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
    }
}
