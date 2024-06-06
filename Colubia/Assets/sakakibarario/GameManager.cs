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
        Pose
    }
    //フェード用
    [SerializeField] private string sceneNameO;
    [SerializeField] private string sceneNameC;
    [SerializeField] private string sceneName1;
    [SerializeField] private string sceneName2;
    [SerializeField] private string sceneName3;

    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

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
        }

    }
    // オープニング処理
    void GameHome()
    {

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
            Initiate.Fade(sceneName1, fadeColor, fadeSpeed);
        }
        if (RandomQestion.IntermediateQestionFlag == true)
        {
            Debug.Log("Intermediate");
            Initiate.Fade(sceneName2, fadeColor, fadeSpeed);
        }
        if (RandomQestion.AdvancedQuestionFlag == true)
        {
            Debug.Log("Advabced");
            Initiate.Fade(sceneName3, fadeColor, fadeSpeed);
        }

        Debug.Log("playing");
    }

    // ゲームクリアー処理
    void GameClear()
    {
        GState = "GameClear";
        //SceneManager.LoadScene(sceneNameC);
        Initiate.Fade(sceneNameC, fadeColor, fadeSpeed);
        Debug.Log("GameClear");
    }

    // ゲームオーバー処理
    void GameOver()
    {
        GState = "GameOver";
        Initiate.Fade(sceneNameO, fadeColor, fadeSpeed);
        Debug.Log("gameover");
    }

}
