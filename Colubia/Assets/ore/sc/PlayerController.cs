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

    LockerController lockerController;
    PaperController paperController;
    BatteryController batteryController;
    BatteryBar batteryBar;
    StunGunController stunGunController;
    enemyenemy en;

    SpriteRenderer sp;
    Color spriteColor;

    public GameObject stungun;

    //　プレイヤー管理
    public float hideduration = 0.05f;
    public float speed = 3.0f;
    private float playerX;
    private bool Onmove = true;
    private bool isMoveLeft = false;
    private bool isMoveRight = false;


    public bool isInteract = true;

    //　ロッカー系
    public bool inLocker = false;

    //　ペーパー系
    public bool isLookPaper = false;

    //  重力管理
    private bool SwitchGravity = true;

    //  回転管理
    private float PlayerAngle = 0;
    private int PlayerAngleCount = 0;

    //  スタンガン系
    public int Battery = 2;
    public bool onFire = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lockerController = GameObject.FindWithTag("Locker").GetComponent<LockerController>();
        paperController = GameObject.FindWithTag("paper").GetComponent<PaperController>();
        batteryController = GameObject.FindWithTag("Battery").GetComponent<BatteryController>();
        batteryBar = GameObject.Find("BatteryBar").GetComponent<BatteryBar>();
        en = GameObject.FindWithTag("Enemy").GetComponent<enemyenemy>();

        sp = GetComponent<SpriteRenderer>();
        spriteColor = sp.color;

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Onmove)
        {
            //　Aを押したら左に進む
            if (Input.GetKey(KeyCode.A))
            {
                isMoveLeft = true; isMoveRight = false;
                playerX = -speed;
            }

            //　Bを押したら左に進む
            else if (Input.GetKey(KeyCode.D))
            {
                isMoveRight = true; isMoveLeft = false;
                playerX = speed;
            }
            else playerX = 0;
        }

        //  キャラクターが進行方向に進むようにする
        if (isMoveRight)
        {
            transform.localScale = new Vector2(-0.4f, 0.4f);
            stungun.transform.localScale = new Vector2(-0.3f, 0.7f);
        }
        if (isMoveLeft)
        {
            transform.localScale = new Vector2(0.4f, 0.4f);
            stungun.transform.localScale = new Vector2(0.3f, 0.7f);
        }


        //　Spaceを押したら重力を反転させ、グラフィックの向きを整える
        if (SwitchGravity && inLocker == false && isLookPaper == false) 
        {
            if (Input.GetKey(KeyCode.Space))
                GravityChange();
        }

        //　スタンガン
        if (SwitchGravity && inLocker == false && isLookPaper == false)
        {
            if(Input.GetMouseButton(0))
            {
                stungun.SetActive(true);
                stunGunController = GameObject.Find("stunarea").GetComponent<StunGunController>();
            }

            if(Input.GetMouseButtonUp(0))
            {
                StartCoroutine(StunGun());
            }
        }

        //  ロッカーのボタンガイドがアクティブなら
        if (lockerController.LockerF.activeSelf) 
        {
            if (Input.GetKey(KeyCode.F) && isInteract == true) 
            {
                isInteract = false;
                StartCoroutine(Interactive("Locker"));
            }
        }

        //　ペーパーのボタンガイドがアクティブなら
        if (paperController.PaperF.activeSelf) 
        {
            if(Input.GetKey(KeyCode.F) && isInteract == true)
            {
                isInteract = false;
                StartCoroutine(Interactive("Paper"));
            }
        }
        //  ペーパーを見てる時　＆＆　ペーパーESCガイドが有効の時
        if (isLookPaper == true && paperController.PaperESC.activeSelf)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("iashd");
                isLookPaper = false;
                Onmove = true;
                paperController.PaperESC.SetActive(false);
                paperController.PaperLook.SetActive(false);

               // StartCoroutine(waitTime(1000));
                isInteract = true;
            }
        }

        //　バッテリー
        if (batteryController != null && batteryController.BatteryF.activeSelf) 
        {
            if (Input.GetKey(KeyCode.F) && isInteract == true && Battery < 5) 
            {
                isInteract = false;
                StartCoroutine(Interactive("Battery"));
                batteryBar.UpdateBatteryBar();
            }
        }

        rb2D.velocity = new Vector2(playerX, rb2D.velocity.y);
    }

    void GravityChange()
    {
        playerX = 0;//  移動中に反転できないようにできる
        SwitchGravity = false;
        Onmove = false;
        isInteract = false;

        stungun.SetActive(false);

        //　重力を反転させる
        rb2D.gravityScale *= -1;

        StartCoroutine(PlayerRotate());
    }

    IEnumerator PlayerRotate()
    {
        yield return new WaitForSeconds(0.25f);

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
            PlayerAngle += 3.0f;

            //  次の回転まで少し待機
            yield return new WaitForSeconds(0.000025f);
        }

        //  回転後、左右が逆なので反転させる
        FlipX(gameObject);

        //  空中で回転できないように少し待機
        yield return new WaitForSeconds(0.25f);
        SwitchGravity = true;
        Onmove = true; //着地後に移動できるようにする
        isInteract = true;
    }

    void FlipX(GameObject anyobj)
    {
        if (anyobj.GetComponent<SpriteRenderer>().flipX == false)
            anyobj.GetComponent<SpriteRenderer>().flipX = true;
        else
            anyobj.GetComponent<SpriteRenderer>().flipX = false;

    }

    IEnumerator Interactive(string anyOBJ)
    {
        Debug.Log("F");

        if (anyOBJ == "Locker")
        {
            //  隠れる
            if (inLocker == false)
            {
                inLocker = true;
                Onmove = false;      //　主人公を止める
                StartCoroutine(hideCTRL(0));    //　主人公を非表示にする
                StartCoroutine(LockerActivate(true));   //　ロッカー視点を表示する
            }
            //　表に出る
            else
            {
                inLocker = false;
                Onmove = true;      //　主人公を動けるようにする
                StartCoroutine(hideCTRL(1));    //　主人公を表示する
                StartCoroutine(LockerActivate(false)); //　ロッカー視点を取り除く
            }

            //　ロッカーのX座標を「主人公とは無関係」のベクター型変数に保存
            position = lockerController.transform.position;
            //　保存した座標をプレイヤーに入れる
            transform.position = position;

            yield return new WaitForSeconds(4f);
            isInteract = true;
        }

        if (anyOBJ == "Paper") 
        {
            if(isLookPaper==false)
            {
                isLookPaper = true;
                Onmove = false;
                paperController.PaperLook.SetActive(true);

                yield return new WaitForSeconds(2f);
                paperController.PaperESC.SetActive(true);
            }
        }

        if (anyOBJ == "Battery")
        {
            Battery += 1;

            batteryController.objDestroy();

            yield return new WaitForSeconds(2f);
            isInteract = true;
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
        onFire = true;

        if (stunGunController.checkInArea && onFire)
        {
            Debug.Log("hit");
            Battery -= 1;
            batteryBar.UpdateBatteryBar();
            en.enabled = false;
            yield return new WaitForSeconds(0.5f);
            stungun.SetActive(false);
            yield return new WaitForSeconds(5.0f);
            en.enabled = true;
        }
        else if(stunGunController.checkInArea == false && onFire)
        {
            Debug.Log("miss");
            Battery -= 1;
            batteryBar.UpdateBatteryBar();
            yield return new WaitForSeconds(0.5f);
            stungun.SetActive(false);
        }

        onFire = false;
    }

}