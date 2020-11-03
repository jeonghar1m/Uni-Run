using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class Mob : MonoBehaviour
{
    public GameObject mob;
    private bool isMob = false;
    private float width;
    private float speed = 2.0f;
    private int sign = -1;
    private float minX = -4.0f;
    private float maxX = 4.0f;
    private void OnEnable()
    {

        if (Random.Range(0, 2) == 0)    //2분의 1 확률로 몹 활성화
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
        if (isMob && transform.position.x >= width)
        {
            mob.transform.localPosition += new Vector3(speed * Time.deltaTime * sign, 0, 0);

            if (mob.transform.localPosition.x <= minX || mob.transform.localPosition.x >= maxX)
            {
                mob.transform.Rotate(0, 180, 0);
                sign *= -1;
            }
        }
    }
}