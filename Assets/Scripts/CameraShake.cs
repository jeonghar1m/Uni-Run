using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount = 1.0f;   //진동 세기
    float shakeTime;    //화면 진동 지속 시간
    Vector3 initialPosition;    //진원지. Start 함수에서 위치 지정.

    public void VibrateForTime(float time)
    {
        shakeTime = time;   //GameManager로 부터 값을 전달 받음.
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = new Vector3(0f, 0f, -5f); //진원지 좌표 지정.
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere;   //임의의 지점으로 카메라 좌표를 이동시켜 진동효과를 줌.
            shakeTime -= Time.deltaTime;    //시간이 흐르는 만큼 shakeTime 감소.
        }
        //shakeTime == 0
        else
        {
            shakeTime = 0.0f;   //shakeTime 초기화
            transform.position = initialPosition;   //카메라 좌표 원래대로 초기화
        }
    }
}
