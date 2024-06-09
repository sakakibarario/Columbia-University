using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Strength_Security_Guard : MonoBehaviour
{
    Rigidbody2D rb;
    private GameObject player;
    PlayerController PlayerController;
    //GameObject MyEnemy;

    //敵の動き
    public float speed = 4.0f;
    float speed_P = 2.5f;
    private int distance_traveled = 7;//移動距離
    private bool EMove_Stop = true;
    static public bool EMove_Stop_mark = false;
    public float Enemy_Start_Count = 3.0f;//最初の動き出す時間を変える
    Vector2 movementx;
    Vector2 movementy;

    //カウント用
    private float countleftTime = 3.0f;   //左向き
    private float countrightTime = 3.0f;   //右向き
    private bool direction = false;        //trueは右向き
    private float countstoptime = 3.0f;   //左向き

    //playerとの距離
    public float reactionDistanceX = 10.0f;//距離
    public float reactionDistanceY = 4.0f;//距離
    private bool isActive = false;
    private bool Moved_Enemy = false;

    //アニメーション用
    Animator animator; //アニメーター
    private string stopAnime = "Enemy_Strength_Security_Guard_stand";
    private string moveAnime = "Enemy_Strength_Security_Guard_run";

    //マーク用
    public GameObject Exclamation_mark;
    public GameObject Question_mark;

    //SE用
    AudioSource AudioSource;
    public AudioClip ExclamationAudio;
    public AudioClip QuestionAuidoname;
    private bool AudioFlag = false;

    Vector2 MyEnemy = new Vector2(0, 0);
    Vector2 MyEnemy2 = new Vector2(0, 0);

    bool isPose = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        AudioSource = GetComponent<AudioSource>();
        AudioFlag = true;
        Exclamation_mark.SetActive(false);
        Question_mark.SetActive(false);

        //Player　のゲームオブジェクトを得る
        PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //最初の動き出す時間を変える
        countleftTime = Enemy_Start_Count;

        //初期座標を記憶
        MyEnemy = transform.position;
        MyEnemy2 = MyEnemy;
        MyEnemy2.x = MyEnemy2.x - distance_traveled;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Pose")
        {
            animator.Play(stopAnime);    //アニメーション再生
            Moved_Enemy = true;//初期位置に戻す
            isPose = true;
        }

        if(PlayerController.inLocker == true)
        {
            Debug.Log("ロッカー");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            isActive = false;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

        }
            

        if (transform.position.x < PlayerController.transform.position.x)
        {
            Question_mark.transform.localScale = new Vector2(-0.7f, 0.7f);//左向き
        }
        else if (transform.position.x > PlayerController.transform.position.x)
        {
            Question_mark.transform.localScale = new Vector2(0.7f, 0.7f);//左向き
        }


        if (isActive)//主人公発見時
        {
            Exclamation_mark.SetActive(true);

            if (AudioFlag)
            {
                //オーディオ再生
                AudioSource.PlayOneShot(ExclamationAudio, 1.0f);
                AudioFlag = false;
            }
        }
        else
        {
            Exclamation_mark.SetActive(false);
        }

        if (EMove_Stop_mark)//主人公見失い時
        {
            Debug.Log("未発見");
            Question_mark.SetActive(true);

            if (!AudioFlag)
            {
                //オーディオ再生
                AudioSource.PlayOneShot(QuestionAuidoname, 1.0f);
                AudioFlag = true;
            }
        }
        else
        {
            Question_mark.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.GState == "Playing")
        {
            //プレイヤーとの距離を求める
            movementx.x = this.transform.position.x - PlayerController.transform.position.x;
            movementy.y = this.transform.position.y - PlayerController.transform.position.y;
            float distx = movementx.magnitude;
            float disty = movementy.magnitude;

            if (disty < reactionDistanceY && distx < reactionDistanceX && PlayerController.inLocker == false)
            {
              
                isActive = true; //アクティブにする
                Debug.Log(isActive);
                EMove_Stop_mark = false;
            }
            else
            {
                isActive = false; //非アクティブにする

                Debug.Log("mihakken");
                if (Moved_Enemy)
                {
                    MoveBack();
                }
               
            }

            if (!isActive && !Moved_Enemy)//主人公と離れているとき
            {
                if (direction)
                {
                    countrightTime -= Time.deltaTime; //カウントアップ

                    if (countrightTime < 0)
                    {
                        animator.Play(moveAnime);    //アニメーション再生
                        this.transform.localScale = new Vector2(-0.6f, 0.6f);//右向き
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy, speed * Time.deltaTime);
                        
                        if (transform.position.x == MyEnemy.x)
                        {
                            animator.Play(stopAnime);    //アニメーション再生
                            countrightTime = 3.0f;
                            direction = false;
                        }
                    }
                }
                else
                {
                    countleftTime -= Time.deltaTime;  //カウントアップ

                    if (countleftTime < 0)
                    {
                        animator.Play(moveAnime);    //アニメーション再生
                        this.transform.localScale = new Vector2(0.6f, 0.6f);//左向き
                       
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy2, speed * Time.deltaTime);

                        if (transform.position.x == MyEnemy2.x)
                        {
                            animator.Play(stopAnime);    //アニメーション再生
                            countleftTime = 3.0f;
                            direction = true;
                        }
                    }
                }
            }
            else if(isActive)//主人公が近くにいた時の動き
            {
                animator.Play(moveAnime);    //アニメーション再生
                Moved_Enemy = true;
                countleftTime = 3.0f;//カウントリセット
                countrightTime = 3.0f;//カウントリセット
                 // PLAYERの位置を取得
                Vector2 targetPos = PlayerController.transform.position;
                // PLAYERのx座標
                float x = targetPos.x;
                // ENEMYは、地面を移動させるので座標は常に0とする
                float y = 0;
                // 移動を計算させるための２次元のベクトルを作る
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                // ENEMYのRigidbody2Dに移動速度を指定する
                rb.velocity = direction * speed_P;
                //反転
                if (transform.position.x < PlayerController.transform.position.x)
                {
                    this.transform.localScale = new Vector2(-0.6f, 0.6f);//左向き
                }
                else if (transform.position.x > PlayerController.transform.position.x)
                {
                    this.transform.localScale = new Vector2(0.6f, 0.6f);//左向き
                }
            }
        }
        else
        {
            rb.Sleep();//動きを止める
        }

    }
    void MoveBack()
    { 
        if(EMove_Stop)//一時停止
        {
            animator.Play(stopAnime);    //アニメーション再生
            if (!isPose)
            EMove_Stop_mark = true;
            countstoptime -= Time.deltaTime;

            if(countstoptime < 0)
            {
                countstoptime = 3.0f;
                EMove_Stop_mark = false;
                EMove_Stop = false;
            }
        }
        if (!EMove_Stop)
        {
            animator.Play(moveAnime);    //アニメーション再生
            if (transform.position.x < MyEnemy.x)
            {
                this.transform.localScale = new Vector2(-0.6f, 0.6f);//左向き
            }
            else if (transform.position.x > MyEnemy.x)
            {
                this.transform.localScale = new Vector2(0.6f, 0.6f);//左向き
            }
              
            transform.position = Vector3.MoveTowards(transform.position, MyEnemy, speed * Time.deltaTime);//初期位置戻る

            if (MyEnemy.x == transform.position.x)//初期位置に戻ったら
            {
                animator.Play(stopAnime);    //アニメーション再生
                countrightTime = 3.0f;
                countleftTime = 3.0f;
                isPose = false;//ポーズフラグ
                EMove_Stop = true;
                direction = false;//初期の向きに戻す
                Moved_Enemy = false;//最初の動きに戻す
            }
        }
    }
}
