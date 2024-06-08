using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ゲームステート
    public enum GameState
    {
        Playing,
        Clear,
        Over,
        Home,
        Pose,
        Title,
        Safe
    }
    //フェード用
    public string sceneNameO;
    public string sceneNameC;
    public string sceneName1;
    public string sceneName2;
    public string sceneName3;
    public string sceneNameS;
    public string sceneNameH;
    public string sceneNameT;

    public Color loadToColor = Color.white;
    public float fadeSpeed;

    // 現在のゲーム進行状態
    GameState currentState = GameState.Home;

    public static string GState = "home";//ゲームの状態

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            dispatch(GameState.Playing);
    }

    // 状態による振り分け処理
    public void dispatch(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.Playing:
                GameStart();
                break;
            case GameState.Clear:
                GameClear();
                break;
            case GameState.Over:
                GameOver();
                break;
            case GameState.Home:
                GameHome();
                break;
            case GameState.Pose:
                GamePose();
                break;
            case GameState.Title:
                GameTitle();
                break;
            case GameState.Safe:
                GameSafe();
                break;
        }

    }
    void GameTitle()
    {
        Debug.Log("Title");
        Initiate.Fade(sceneNameT, loadToColor, fadeSpeed);
    }
    // オープニング処理
    void GameHome()
    {
        Debug.Log("home");
        Initiate.Fade(sceneNameH, loadToColor, fadeSpeed);
    }

    //ポーズ処理
    void GamePose()
    {
        GState = "Pose";
    }

    // ゲームスタート処理
    void GameStart()
    {
        GState = "Playing";
        if (RandomQestion.BeginnerQuestionFlag == true)
        {
            Debug.Log("Beginner");
            Initiate.Fade(sceneName1, loadToColor, fadeSpeed);
        }
        if (RandomQestion.IntermediateQestionFlag == true)
        {
            Debug.Log("Intermediate");
            Initiate.Fade(sceneName2, loadToColor, fadeSpeed);
        }
        if (RandomQestion.AdvancedQuestionFlag == true)
        {
            Debug.Log("Advabced");
            Initiate.Fade(sceneName3, loadToColor, fadeSpeed);
        }

        Debug.Log("playing");
    }

    //金庫処理
    void GameSafe()
    {
        GState = "GameSafe";
        Initiate.Fade(sceneNameS, loadToColor, fadeSpeed);
        Debug.Log("GameSafe");
    }

    // ゲームクリアー処理
    void GameClear()
    {
        GState = "GameClear";
        //SceneManager.LoadScene(sceneNameC);
        Initiate.Fade(sceneNameC, loadToColor, fadeSpeed);
        Debug.Log("GameClear");
    }

    // ゲームオーバー処理
    void GameOver()
    {
        GState = "GameOver";
        Initiate.Fade(sceneNameO, loadToColor, fadeSpeed);
        
        Debug.Log("gameover");
    }

}
