using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_security_guard : MonoBehaviour
{
    Rigidbody2D rb;

    //敵の動き
    public float speed = 2.5f;
    private int distance_traveled = 5;//移動距離
    public int Enemy_start_count = 0;//最初の動き出す時間を変える

    //カウント用
    private float countleftTime  = 3.0f;   //左向き
    private float countrightTime = 3.0f;   //右向き
    private bool direction = false;        //trueは右向き

    private bool Moved_Enemy = false;


    //アニメーション用
    Animator animator; //アニメーター
    public string stopAnime = "Enemy_security_guardB_stand";
    public string moveAnime = "Enemy_security_guardB_run";
    

    Vector2 MyEnemy = new Vector2(0, 0);
    Vector2 MyEnemy2 = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Animator をとってくる
        animator = GetComponent<Animator>();

     

        //初期座標を記憶
        MyEnemy = transform.position;
        MyEnemy2 = MyEnemy;
        MyEnemy2.x = MyEnemy2.x - distance_traveled;//敵の可動域

        countleftTime = Enemy_start_count;//動き出す時間をずらす
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Pose")
        {
            animator.Play(stopAnime);    //アニメーション再生
            Moved_Enemy = true;//初期位置に戻す
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.GState == "Playing")
        {

            if (Moved_Enemy)
            {
                animator.Play(moveAnime);    //アニメーション再生
                if (transform.position.x < MyEnemy.x)
                {
                    this.transform.localScale = new Vector2(-2, 2);//左向き
                }
                else if (transform.position.x > MyEnemy.x)
                {
                    this.transform.localScale = new Vector2(2, 2);//左向き
                }
                //指定座標まで移動
                transform.position = Vector3.MoveTowards(transform.position, MyEnemy, speed * Time.deltaTime);

                if (MyEnemy.x == transform.position.x)
                {
                    countrightTime = 3.0f;
                    countleftTime = 3.0f;
                    direction = false;
                    Moved_Enemy = false;

                }
            }

            if (!Moved_Enemy)
            {
                if (direction)
                {
                    
                    countrightTime -= Time.deltaTime; //カウントアップ

                    if (countrightTime < 0)
                    {
                        animator.Play(moveAnime);
                        this.transform.localScale = new Vector2(-2, 2);//右向き
                        //指定座標まで移動
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy, speed * Time.deltaTime);
                       
                        if (transform.position.x == MyEnemy.x)
                        {
                            animator.Play(stopAnime);
                            Debug.Log("aaaa");
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
                        animator.Play(moveAnime);
                        this.transform.localScale = new Vector2(2, 2);//左向き
                        //指定座標まで移動                                             
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy2, speed * Time.deltaTime);

                        if (transform.position.x == MyEnemy2.x)
                        {
                            animator.Play(stopAnime);
                            Debug.Log("aaaa");
                            countleftTime = 3.0f;
                            direction = true;
                        }
                    }
                }
            }
        }
        else
        {
            animator.Play(stopAnime);    //アニメーション再生
            rb.Sleep();//動きを止める
        }

    }
  

}
