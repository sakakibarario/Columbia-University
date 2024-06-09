using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Strength_electric : MonoBehaviour
{
    Enemy_Strength_Security_Guard enemy_Strength;
    //アニメーション用
    Animator animator; //アニメーター
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy_Strength = GameObject.FindWithTag("Enemy_Strength_Security_Guard").GetComponent<Enemy_Strength_Security_Guard>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_Strength.enabled == false)
        {
            animator.Play("Enemy_Strength_electric");
        }
    }
}
