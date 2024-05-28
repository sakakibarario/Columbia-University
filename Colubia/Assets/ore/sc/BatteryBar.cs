using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    PlayerController PlayerCTRL;

    void Start()
    {
        PlayerCTRL = GameObject.Find("Player").GetComponent<PlayerController>();
        slider.value = (float)PlayerCTRL.Battery / 5.0f;
    }
    void Update()
    {

        //if (hp >= 0)
        //{
        //    hpBar.SetPosition(1, new Vector3(0.0f,3 * (hp / MaxHP), 0.0f ));
        
    }

    public void UpdateBatteryBar()
    {
        slider.value = (float)PlayerCTRL.Battery / 5.0f;
    }
}
