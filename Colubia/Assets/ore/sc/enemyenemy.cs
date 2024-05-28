using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyenemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.white;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.color = Color.green;
    }
}
