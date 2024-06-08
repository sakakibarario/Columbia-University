using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryAndEndButtonProgram : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEnd()
    {
        //ゲームの状態をhomeに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }

    public void OnClickRetry()
    {
        //ゲームの状態をplayingに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
    }
}
