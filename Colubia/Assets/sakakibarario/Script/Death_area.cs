using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_area : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")// 主人公
        {
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Over);
        }
    }
}
