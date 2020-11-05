using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //LoadScene을 사용하기 위함.

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        // 게임을 재시작할 수 있게 하는 처리
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Main");

    }
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void ChangeHelpScene()
    {
        SceneManager.LoadScene("Help");
    }

    public void ChangeTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}