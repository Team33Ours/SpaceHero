using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        // 게임 씬으로 이동
        // 임시 ScyScene 연결
        SceneManager.LoadScene("ScyScene");
    }

    public void OnClickExitButton()
    {
        // 게임 종료
        Application.Quit();
    }
}
