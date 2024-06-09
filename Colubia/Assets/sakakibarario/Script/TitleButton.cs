using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
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
    public void OnClick()
    {
        //オーディオ再生
        AudioSource.PlayOneShot(ButtonSE, 1.5f);
        //ゲームの状態をtitleに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }
}
