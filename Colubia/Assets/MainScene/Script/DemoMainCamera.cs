using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMainCamera : MonoBehaviour
{
    PlayerController p;

    public float PullCamera = 0.125f;
    public float PushCamera = 0.015f;
    public bool EndPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!EndPoint)
        {
            if (transform.position.x - PullCamera <= p.transform.position.x && transform.position.x + PullCamera >= p.transform.position.x)
                transform.position = new Vector3(p.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            if (transform.position.x < 0)
                transform.position = new Vector3(transform.position.x + PushCamera, transform.position.y, transform.position.z);
            if (transform.position.x > 0)
                transform.position = new Vector3(transform.position.x - PushCamera, transform.position.y, transform.position.z);
        }

        if(p.onLadder)
            transform.position = new Vector3(p.transform.position.x, transform.position.y, transform.position.z);
    }          


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EndPoint")
            EndPoint = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EndPoint")
            EndPoint = false;
    }
}
