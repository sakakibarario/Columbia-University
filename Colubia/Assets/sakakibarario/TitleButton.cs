using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        //ゲームの状態をtitleに変更
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }
}
