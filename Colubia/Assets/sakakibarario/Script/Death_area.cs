using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_area : MonoBehaviour
{
    PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerController.inLocker == false)// 主人公
        {
            // ゲームオーバー処理を呼ぶ
            FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Over);
        }
    }
}
