using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //LoadScene을 사용하기 위함.

public class GoToNextChapter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
     // 다음 스테이지로 이동할 때
     if (Input.GetKeyDown(KeyCode.Return))
        SceneManager.LoadScene("Main2");
    }
}
