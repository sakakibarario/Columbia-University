using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMainCamera : MonoBehaviour
{
    PlayerController player;

    public bool EndPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= -1.3 && player.transform.position.x <= 28.0) 
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }          

}
