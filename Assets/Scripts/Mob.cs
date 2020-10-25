using UnityEditor;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public GameObject mob;
    private bool isMob = false;
    private float width;
    private float speed = 2.0f;
    private void OnEnable()
    {

        if (Random.Range(0, 0) == 0)    //3분의 1 확률로 몹 활성화
        {
            mob.SetActive(true);
            isMob = true;
        }
        else
            mob.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool isFirstMoving = true;
        if (isMob && transform.position.x >= width && !GameManager.instance.isGameover)
        {
            if (isFirstMoving)  //맨 처음 왼쪽으로 이동
            {
                mob.transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (mob.transform.localPosition.x == -4.0)
                    isFirstMoving = false;
            }
            else if(!isFirstMoving) //왼쪽으로 최대한 이동 후
            {
                if (mob.transform.localPosition.x <= -4.0)
                {
                    mob.transform.Rotate(0, 180, 0);
                    mob.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else if (mob.transform.localPosition.x >= 4.0)
                {
                    mob.transform.Rotate(0, 180, 0);
                    mob.transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
            }
        }
    }
}
