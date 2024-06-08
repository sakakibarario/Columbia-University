using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 position;

    StunGunIconController stunGunIconController;
    LockerController lockerController;
    PaperController paperController;
    BatteryController batteryController;
    BatteryBar batteryBar;
    StunGunController stunGunController;
    enemyenemy en;
    LadderController ladderController;
    AudioSource audioSource;
    SafeController safeController;

    Animator animator;

    SpriteRenderer sp;
    Color spriteColor;

    public GameObject stungun;

    //  sound管理
    public AudioClip stungun_SE;
    public AudioClip Locker_SE;
    public AudioClip Locker_HeartBeat_SE;
    public AudioClip Ladder_SE;


    //　プレイヤー管理
    public float hideduration = 0.05f;
    public float speed = 3.0f;
    private float playerX;
    private float playerY;
    private bool CanMove = true;
    private bool isMoveLeft = false;
    private bool isMoveRight = false;
    private bool isTenjo = false;
    private bool top = false;
    private bool under = false;

    public bool CanInteract = true;

    //　ロッカー系
    public bool inLocker = false;

    //　ペーパー系
   //public bool isLookPaper = false;

    //  重力管理
    public bool CanSwitchGravity = true;
    private float GravityPoint;

    //  回転管理
    private float PlayerAngle = 0;
    private int PlayerAngleCount = 0;

    //  スタンガン系
    public int Battery = 2;
    public bool onFire = false;
    public bool CanUseStungun = true;

    //  ladder
    public bool onLadder = false;


    float mcount = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        batteryBar = GameObject.Find("BatteryBar").GetComponent<BatteryBar>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        spriteColor = sp.color;
        stunGunIconController = GameObject.Find("StunGunIconCover").GetComponent<StunGunIconController>();

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Playing")
        {
            if (CanMove)
            {
                //　Aを押したら左に進む
                if (Input.GetKey(KeyCode.A))
                {
                    isMoveLeft = true; isMoveRight = false;
                    playerX = -speed;
                    animator.Play("playerwalk");
                }
                //　Dを押したら右に進む
                else if (Input.GetKey(KeyCode.D))
                {
                    isMoveRight = true; isMoveLeft = false;
                    playerX = speed;
                    animator.Play("playerwalk");
                }
                else
                {
                    playerX = 0;
                    animator.Play("playerstop");
                }
            }
            if (onLadder)
            {
                //　Sを押したら下に進む
                if (Input.GetKey(KeyCode.S) && under != true)
                {
                    top = false;
                    playerY = -speed;
                    animator.Play("climb");
                }
                //　Wを押したら上に進む
                else if (Input.GetKey(KeyCode.W) && top != true)
                {
                    under = false;
                    playerY = speed;
                    animator.Play("climb");
                }
                else
                {
                    playerY = 0;
                    animator.Play("stopclimb");
                }

                if (playerY != 0)
                {
                    mcount -= Time.deltaTime;
                    if (mcount < 0)
                    {
                        audioSource.PlayOneShot(Ladder_SE, 0.4f);
                        mcount = 0.3f;
                    }
                }
            }

            //  キャラクターが進行方向に進むようにする
            if (!isTenjo)
            {
                if (isMoveRight)
                {
                    transform.localScale = new Vector2(-0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
                if (isMoveLeft)
                {
                    transform.localScale = new Vector2(0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
            }
            else
            {
                if (isMoveRight)
                {
                    transform.localScale = new Vector2(0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
                if (isMoveLeft)
                {
                    transform.localScale = new Vector2(-0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
            }

            //　Spaceを押したら重力を反転させ、グラフィックの向きを整える
            if (CanSwitchGravity && inLocker == false /*&& isLookPaper == false */&& onLadder == false)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    GravityChange();
            }

            ////　スタンガン
            if (CanSwitchGravity && inLocker == false /*&& isLookPaper == false */&& onLadder == false)
            {
                if (Input.GetMouseButtonDown(0) && CanUseStungun && Battery > 0)
                {
                    stungun.SetActive(true);
                    stungun.GetComponent<Animator>().Play("stungun0");
                    stunGunController = GameObject.Find("stunarea").GetComponent<StunGunController>();
                }

                if (Input.GetMouseButtonUp(0) && CanUseStungun && Battery > 0)
                {
                    CanUseStungun = false;
                    stunGunIconController.countTime = stunGunIconController.count;
                    StartCoroutine(StunGun());
                }
            }

            //  ロッカーのボタンガイドがアクティブなら
            if (lockerController != null && lockerController.LockerF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true)
                {
                    CanInteract = false;
                    StartCoroutine(Interactive("Locker"));
                }
            }

            if (safeController != null && safeController.SafeF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true)
                {
                    CanInteract = false;
                    StartCoroutine(Interactive("Safe"));
                }
            }

            //　ペーパーのボタンガイドがアクティブなら
            //if (paperController != null && paperController.PaperF.activeSelf)
            //{
            //    if (Input.GetKey(KeyCode.F) && isInteract == true)
            //    {
            //        isInteract = false;
            //        StartCoroutine(Interactive("Paper"));
            //    }
            //}
            //  ペーパーを見てる時　＆＆　ペーパーESCガイドが有効の時
            //if (paperController != null && isLookPaper == true && paperController.PaperESC.activeSelf)
            //{
            //    if (Input.GetKey(KeyCode.Escape))
            //    {
            //        Debug.Log("iashd");
            //        isLookPaper = false;
            //        Onmove = true;
            //        paperController.PaperESC.SetActive(false);
            //        paperController.PaperLook.SetActive(false);

            //        isInteract = true;
            //    }
            //}

            //　バッテリー
            if (batteryController != null && batteryController.BatteryF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true && Battery < 5)
                {
                    CanInteract = false;
                    StartCoroutine(Interactive("Battery"));
                    batteryBar.UpdateBatteryBar();
                }
            }

            //  ladder
            if (ladderController != null)
            {
                if (CanSwitchGravity && inLocker == false /*&& isLookPaper == false*/ && ladderController.LadderF.activeSelf || ladderController.childLadderF.activeSelf)
                {
                    if (Input.GetKey(KeyCode.F) && CanInteract == true)
                    {
                        CanInteract = false;
                        StartCoroutine(Interactive("Ladder"));
                    }
                }

                for (int i = 0; i < ladderController.childLadder.Length; i++)
                {
                    if (ladderController.childLadder[i].GetComponent<LadderController>() != null)
                    {
                        if (transform.position.y <= ladderController.transform.position.y - 0.4 ||
                            transform.position.y >= ladderController.childLadder[i].transform.position.y + 0.4 && onLadder)
                        {
                            if (transform.position.y >= ladderController.childLadder[i].transform.position.y + 0.4)
                                top = true;//playerY = 0;    //梯子の一番上まで登った時に降りるように促すため
                            if (transform.position.y <= ladderController.transform.position.y - 0.4)
                                under = true;
                            if (Input.GetKey(KeyCode.Space))
                            {
                                onLadder = false;
                                CanMove = true;
                                CanInteract = true;
                                top = false; under = false;

                                ladderController.childLadder[i].GetComponent<BoxCollider2D>().enabled = true;
                                rb2D.gravityScale = GravityPoint;
                            }
                        }
                    }

                }
            }
            if (onLadder == false)
                rb2D.velocity = new Vector2(playerX, rb2D.velocity.y);
            else
                rb2D.velocity = new Vector2(playerX, playerY);
        }
    }

    void GravityChange()
    {
        playerX = 0;//  移動中に反転できないようにできる
        CanSwitchGravity = false;
        CanMove = false;
        CanInteract = false;

        stungun.SetActive(false);

        //　重力を反転させる
        rb2D.gravityScale *= -1;

        StartCoroutine(PlayerRotate());
    }

    IEnumerator PlayerRotate()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        //  PlayerAngleCountを初期化させて数字を大きくなりすぎないようにする
        if (PlayerAngleCount >= 2) PlayerAngleCount = 0;
        //  PlayerCountを初期化させて数字を大きくなりすぎないようにする
        if (PlayerAngle >= 360) PlayerAngle = 0;


        //  これを使用して最大角度を変更させることで、天井か床に頭で着地しないようにする
        PlayerAngleCount += 1;

        //　プレイヤーの向きをジョジョに変える
        for (; PlayerAngle <= 180 * PlayerAngleCount;) 
        {
            //  1°づつ回転させる
            transform.rotation = Quaternion.Euler(0, 0, PlayerAngle);
            PlayerAngle += 10.0f;

            //  次の回転まで少し待機
            yield return new WaitForSecondsRealtime(0.000025f);
        }

        isTenjo = !isTenjo;

        //  空中で回転できないように少し待機
        yield return new WaitForSecondsRealtime(0.25f);
        CanSwitchGravity = true;
        CanMove = true; //着地後に移動できるようにする
        CanInteract = true;
    }

    IEnumerator Interactive(string anyOBJ)
    {
        Debug.Log("F");

        if (anyOBJ == "Locker")
        {
            //  隠れる
            if (inLocker == false)
            {
                playerX = 0;
                inLocker = true;
                CanMove = false;      //　主人公を止める
                rb2D.isKinematic = true;
                StartCoroutine(hideCTRL(0));    //　主人公を非表示にする
                StartCoroutine(LockerActivate(true));   //　ロッカー視点を表示する
                audioSource.PlayOneShot(Locker_SE, 0.5f);

                audioSource.clip = Locker_HeartBeat_SE;
                audioSource.loop = true;
                audioSource.volume = 0.5f;
                audioSource.PlayDelayed(1.0f);

            }
            //　表に出る
            else
            {
                inLocker = false;
                CanMove = true;      //　主人公を動けるようにする

                audioSource.Stop();
                audioSource.loop = false;
                audioSource.clip = null;
                rb2D.isKinematic = false;
                StartCoroutine(hideCTRL(1));    //　主人公を表示する
                StartCoroutine(LockerActivate(false)); //　ロッカー視点を取り除く
                audioSource.PlayOneShot(Locker_SE, 0.5f);
            }

            //　ロッカーのX座標を「主人公とは無関係」のベクター型変数に保存
            position.x = lockerController.transform.position.x;
            //　保存した座標をプレイヤーに入れる
            transform.position = new Vector2( position.x, transform.position.y );

            yield return new WaitForSeconds(4f);
            CanInteract = true;
        }

        //if (anyOBJ == "Paper") 
        //{
        //    if(isLookPaper==false)
        //    {
        //        isLookPaper = true;
        //        Onmove = false;
        //        paperController.PaperLook.SetActive(true);

        //        yield return new WaitForSeconds(2f);
        //        paperController.PaperESC.SetActive(true);
        //    }
        //}

        if (anyOBJ == "Battery")
        {
            Battery += 1;

            batteryController.objDestroy();

            yield return new WaitForSeconds(2f);
            CanInteract = true;
        }

        if (anyOBJ == "Ladder") 
        {
            playerX = 0;
            onLadder = true;
            CanMove = false;

            GameObject childLadder;
            for(int i =0; ; i++)
            {
                if (ladderController.childLadder[i].GetComponent<BoxCollider2D>() != null)
                {
                    childLadder = ladderController.childLadder[i];
                    break;
                }
            }

            GravityPoint = rb2D.gravityScale;
            rb2D.gravityScale = 0;

            //yield return new WaitForSeconds(0.25f);
            if(ladderController.doDown)
            {
                position = childLadder.transform.position;
                transform.position = position;
                ladderController.doDown = false;
            }
            else
            {
                position = ladderController.transform.position;
                transform.position = position;
            }

            childLadder.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (anyOBJ == "Safe")
        {
           //FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Safe);
        }
    }

    IEnumerator hideCTRL(float targetAlpha)
    {
        Debug.Log("aaa");
        while (!Mathf.Approximately(spriteColor.a, targetAlpha))
        {
            float changePerFrame = Time.deltaTime / hideduration;
            spriteColor.a = Mathf.MoveTowards(spriteColor.a, targetAlpha, changePerFrame);
            sp.color = spriteColor;
            yield return null;
        }
    }

    IEnumerator LockerActivate(bool activate)
    {
        yield return new WaitForSeconds(0.05f);

        if (activate)
            lockerController.LockerVision.SetActive(true);
        else
            lockerController.LockerVision.SetActive(false);
    }

    IEnumerator StunGun()
    {
        if (Battery > 0) 
        {
            onFire = true;

            stungun.GetComponent<Animator>().Play("stungun1");
            audioSource.PlayOneShot(stungun_SE, 0.5f);

            if (stunGunController.checkInArea && onFire)
            {
                Debug.Log("hit");
                Battery -= 1;
                batteryBar.UpdateBatteryBar();

                    StunGunController.warning_Area.enabled = false;
               if( stunGunController.strong == false)
                    StunGunController.enemy_Security_Guard.enabled = false;
               else
                    StunGunController.enemy_Strength_Security_Guard.enabled=false;

                yield return new WaitForSeconds(0.5f);

                stungun.SetActive(false);

                yield return new WaitForSeconds(4.5f);

                    StunGunController.warning_Area.enabled = true;
                if (stunGunController.strong == false)
                    StunGunController.enemy_Security_Guard.enabled = true;
                else
                    StunGunController.enemy_Strength_Security_Guard.enabled = true;
            }
            else if (stunGunController.checkInArea == false && onFire)
            {
                Debug.Log("miss");
                Battery -= 1;
                batteryBar.UpdateBatteryBar();
                yield return new WaitForSeconds(0.5f);
                stungun.SetActive(false);
            }

            onFire = false;
        }
        else
        {
            Debug.Log("0battery");
            yield return new WaitForSeconds(0.5f);
            stungun.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            ladderController = collision.GetComponent<LadderController>();
        }

        if (collision.gameObject.tag == "Locker")
        {
            lockerController = collision.GetComponent<LockerController>();
        }

        if (collision.gameObject.tag == "paper")
        {
            paperController = collision.GetComponent<PaperController>();
        }

        if (collision.gameObject.tag == "Battery")
        {
            batteryController = collision.GetComponent<BatteryController>();
        }

        if (collision.gameObject.tag == "Safe")
        {
            safeController = collision.GetComponent<SafeController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            GameObject laddertop = collision.gameObject;
            ladderController = laddertop.GetComponent<LadderController>();
        }
    }

    //  pose宙に動けなくする
}