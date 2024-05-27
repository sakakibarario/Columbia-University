using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_security_guard : MonoBehaviour
{
    Rigidbody2D rb;

    //敵の動き
    public float speed = 2.5f;

    //カウント用
    private float countleftTime  = 3.0f;   //左向き
    private float countrightTime = 3.0f;   //右向き
    private bool direction = false;        //trueは右向き

    private bool Moved_Enemy = false;

    Vector2 MyEnemy = new Vector2(0, 0);
    Vector2 MyEnemy2 = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //初期座標を記憶
        MyEnemy = transform.position;
        MyEnemy2 = MyEnemy;
        MyEnemy2.x = MyEnemy2.x - 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Pose")
        {
            Moved_Enemy = true;//初期位置に戻す
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.GState == "Playing")
        {

            if (Moved_Enemy)
            {
                if (transform.position.x < MyEnemy.x)
                {
                    this.transform.localScale = new Vector2(-1, 1);//左向き
                }
                else if (transform.position.x > MyEnemy.x)
                {
                    this.transform.localScale = new Vector2(1, 1);//左向き
                }
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
                        this.transform.localScale = new Vector2(-1, 1);//右向き
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy, speed * Time.deltaTime);
                        //StartCoroutine(Moveright());
                        if (transform.position.x == MyEnemy.x)
                        {
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
                        this.transform.localScale = new Vector2(1, 1);//左向き
                                                                      //StartCoroutine(Moveleft());
                        transform.position = Vector3.MoveTowards(transform.position, MyEnemy2, speed * Time.deltaTime);

                        if (transform.position.x == MyEnemy2.x)
                        {
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
            rb.Sleep();//動きを止める
        }

    }
    //IEnumerator Moveleft()
    //{
    //    this.transform.localScale = new Vector2(1, 1);//左向き
    //    rb.velocity = new Vector2(-speed, rb.velocity.y);//動きを決める
    //    yield return new WaitForSeconds(2.0f);
    //    direction = true;
    //    countleftTime = 3.0f;
    //    yield break;
    //}
    //IEnumerator Moveright()
    //{
    //    this.transform.localScale = new Vector2(-1, 1);//右向き
    //    rb.velocity = new Vector2(speed, rb.velocity.y);//動きを決める
    //    yield return new WaitForSeconds(2.0f);
    //    direction = false;
    //    countrightTime = 3.0f;
    //    yield break;
    //}
}
