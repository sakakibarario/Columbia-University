using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    public GameObject LadderF;
    public GameObject childLadderF;
    public GameObject[] childLadder = new GameObject[1];

    public bool doDown = false;

    PlayerController PlayerCTRL;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCTRL = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerCTRL.onLadder != false && this.name != "ladderTopLeftArea")
        {
            LadderF.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerCTRL.onLadder == false)
            {
                if (this.name != "ladderTopLeftArea")
                {
                    LadderF.SetActive(true);// �擾����obj��\��������
                    PlayerCTRL.inBottomArea = true;
                }
            }

            else
            {
                 if (this.name != "ladderTopLeftArea")
                     PlayerCTRL.inBottomArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.name != "ladderTopLeftArea")
            {
                LadderF.SetActive(false);// �擾����obj��\��������
                PlayerCTRL.inBottomArea = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            doDown = true;
        if (PlayerCTRL.onLadder == false && collision.gameObject.tag == "Player")
            childLadderF.SetActive(true);// �擾����obj��\��������
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doDown = false;
            childLadderF.SetActive(false);// �擾����obj���\���ɂ���
        }
    }
}
