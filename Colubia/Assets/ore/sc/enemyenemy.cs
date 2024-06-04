using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class enemyenemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float countg = 1.0f;
    float countw = 1.0f;
    bool GHQ = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //await Task.Delay(1000);//
        if(GHQ)
        {
            countg -= Time.deltaTime;
            if (countg < 0)
            {
                spriteRenderer.color = Color.green;
                countg = 1.0f;
                GHQ = false;
            }
        }
        else
        {
            countw -= Time.deltaTime;
            if (countw < 0)
            {
                spriteRenderer.color = Color.white;
                countw = 1.0f;
                GHQ = true;
            }
        }
       
        

        // await Task.Delay(1000);//


    }
}
