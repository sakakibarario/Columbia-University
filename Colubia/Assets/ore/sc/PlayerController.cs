using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject childLadder;
    Rigidbody2D rb2D;
    Vector2 position;

    GravityChangeIconController gravityChangeIconController;
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

    //カメラ
    Camera Camera;

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
    public bool isCeilingWalk = false;

    public bool CanInteract = true;

    //　ロッカー系
    public bool inLocker = false;

    //　ペーパー系
   //public bool isLookPaper = false;

    //  重力管理
    public bool CanSwitchGravity = true;
    private float GravityPoint;
    public bool isCeiling = false;

    //  回転管理
    private float PlayerAngle = 0;
    private int PlayerAngleCount = 0;

    //  スタンガン系
    public int Battery = 2;
    public bool onFire = false;
    public bool CanUseStungun = true;

    //  ladder
    public bool onLadder = false;
    public bool inTopArea = false;
    public bool inBottomArea = false;


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
        gravityChangeIconController = GameObject.Find("GravityChangeIconCover").GetComponent<GravityChangeIconController>();

        Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Playing"　|| GameManager.GState == "Demo")
        {
            //  通常時
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

            //  梯子に乗っているとき
            if (onLadder)
            {
                //　Sを押したら下に進む
                if (Input.GetKey(KeyCode.S) && !inBottomArea)
                {
                    playerY = -speed;
                    animator.Play("climb");
                }
                //　Wを押したら上に進む
                else if (Input.GetKey(KeyCode.W) && !inTopArea)
                {
                    playerY = speed;
                    animator.Play("climb");
                }
                //  止まってるとき
                else
                {
                    playerY = 0;
                    animator.Play("stopclimb");
                }
                //  動いてるとき、はしごの効果音を鳴らす
                if (playerY != 0)
                {
                    //  クールタイムをつけて音が重複しないようにする
                    mcount -= Time.deltaTime;
                    if (mcount < 0)
                    {
                        audioSource.PlayOneShot(Ladder_SE, 0.4f);
                        mcount = 0.3f;
                    }
                }
            }

            //  キャラクターの向きと進行方向を合わせる
            if (!isCeilingWalk)   //    地面を歩いてるとき
            {
                if (isMoveRight)    //  右に進んでるトキ
                {
                    transform.localScale = new Vector2(-0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
                if (isMoveLeft)     //  左に進んでるトキ
                {
                    transform.localScale = new Vector2(0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
            }
            else     //    天井を歩いてるとき
            {
                if (isMoveRight)    //  右に進んでるトキ
                {
                    transform.localScale = new Vector2(0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
                if (isMoveLeft)     //  左に進んでるトキ
                {
                    transform.localScale = new Vector2(-0.7f, 0.7f);
                    stungun.transform.localScale = new Vector2(-0.35f, 0.35f);
                }
            }

            //　LeftShiftを押して天井を歩く
            if (ladderController == null && CanSwitchGravity && inLocker == false && onLadder == false)
            {
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    CanSwitchGravity = false;
                    gravityChangeIconController.countTime = gravityChangeIconController.count;
                    GravityChange();
                }
            }

            //　スタンガン
            if (CanSwitchGravity && inLocker == false && onLadder == false)
            {
                //  左クリック押し込んでるトキ
                if (Input.GetMouseButtonDown(0) && CanUseStungun && Battery > 0)
                {
                    stungun.SetActive(true);//  スタンガンを表示
                    stungun.GetComponent<Animator>().Play("stungun0");//    スタンガンのアニメーションを再生
                    stunGunController = GameObject.Find("stunarea").GetComponent<StunGunController>();  
                }

                //  左クリック離したトキ
                if (stunGunController != null && Input.GetMouseButtonUp(0) && CanUseStungun && Battery > 0)
                {
                    CanUseStungun = false;//    スタンガン使用中にクリックできないようにする判定
                    stunGunIconController.countTime = stunGunIconController.count;//    スタンガンのクールタイムを視覚化するための　スタンガンUIのスクリプトの数値を初期化
                    StartCoroutine(StunGun());  //  スタンガンの処理開始
                }
            }

            //  ロッカーのボタンガイドがアクティブのトキに　Fを押すとロッカーに隠れる
            if (lockerController != null && lockerController.LockerF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true)
                {
                    CanInteract = false;//  隠れている間にインタラクトできないようにする
                    StartCoroutine(Interactive("Locker"));//    ロッカーの処理
                }
            }

            //  金庫のボタンガイドがアクティブのトキに　Fを押すと金庫を確認
            if (safeController != null && safeController.SafeF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true)
                {
                    CanInteract = false;//  金庫に触れた時にほかのオブジェクトを触れないようにする
                    StartCoroutine(Interactive("Safe"));//  金庫の処理開始
                }
            }

            //　バッテリーのボタンガイドがアクティブのトキに　Fを押すとバッテリーを拾う
            if (batteryController != null && batteryController.BatteryF.activeSelf)
            {
                if (Input.GetKey(KeyCode.F) && CanInteract == true && Battery < 5)//    バッテリーが最大のトキ
                {
                    CanInteract = false;
                    StartCoroutine(Interactive("Battery"));
                    batteryBar.UpdateBatteryBar();
                }
            }

            //  ladder
            if (ladderController != null)
            {
                if (CanSwitchGravity && inLocker == false && ladderController.LadderF.activeSelf || ladderController.childLadderF.activeSelf)
                {
                    if (isCeilingWalk == false && Input.GetKey(KeyCode.F) && CanInteract == true)
                    {
                        CanInteract = false;
                        StartCoroutine(Interactive("Ladder"));
                    }
                }

                if (inBottomArea || inTopArea)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        onLadder = false;
                        CanMove = true;
                        CanInteract = true;

                        childLadder.GetComponent<BoxCollider2D>().enabled = true;
                        rb2D.gravityScale = GravityPoint;
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
        isCeiling = true;
        playerX = 0;//  移動中に反転できないようにできる
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

        isCeilingWalk = !isCeilingWalk;

        //  空中で回転できないように少し待機
        yield return new WaitForSecondsRealtime(0.3f);
        CanInteract = true;
        //yield return new WaitForSecondsRealtime(0.2f);
        yield return new WaitForSecondsRealtime(0.15f);
        isCeiling = false;
        CanMove = true; //着地後に移動できるようにする

        //yield return new WaitForSecondsRealtime(1.55f);
        //CanSwitchGravity = true;

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

            for(int i =0; ; i++)
            {
                if (ladderController.childLadder[i].name == "ladderTOP")
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
           FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Safe);
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
            ladderController = collision.GetComponent<LadderController>();

        if (collision.gameObject.name == "ladderTopLeftArea")
            inTopArea = true;

        if (collision.gameObject.tag == "Locker")
            lockerController = collision.GetComponent<LockerController>();

        if (collision.gameObject.tag == "paper")
            paperController = collision.GetComponent<PaperController>();

        if (collision.gameObject.tag == "Battery")
            batteryController = collision.GetComponent<BatteryController>();

        if (collision.gameObject.tag == "Safe")
            safeController = collision.GetComponent<SafeController>();

        if (collision.gameObject.tag == "BoxON")
            Camera.OnBox = true;
       
        if (collision.gameObject.tag == "BoxOFF")
            Camera.OnBox = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
            ladderController    = null;

        if (collision.gameObject.name == "ladderTopLeftArea")
            inTopArea = false;

        if (collision.gameObject.tag == "Locker")
            lockerController    = null;

        if (collision.gameObject.tag == "paper")
            paperController     = null;

        if (collision.gameObject.tag == "Battery")
            batteryController   = null;

        if (collision.gameObject.tag == "Safe")
            safeController      = null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            GameObject laddertop = collision.gameObject;
            ladderController = laddertop.GetComponent<LadderController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ladderController = null;
    }

    //  pose宙に動けなくする
}