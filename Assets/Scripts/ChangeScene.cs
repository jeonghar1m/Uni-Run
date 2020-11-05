using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //LoadScene을 사용하기 위함.

public class ChangeScene : MonoBehaviour
{
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