using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FaderTest : MonoBehaviour
{
    public string scene;
    public Color loadToColor = Color.white;

    public void GoFade()
    {
        Initiate.Fade(scene, loadToColor, 1.0f);
    }
}
