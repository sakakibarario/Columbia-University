using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Vector3 position = new Vector3(3.5f, 2.0f, 0.0f);
    Vector3 scale = new Vector3(-0.7f, -0.7f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += position * 2 * Time.deltaTime;
        transform.localScale += scale * Time.deltaTime;
    }
}
