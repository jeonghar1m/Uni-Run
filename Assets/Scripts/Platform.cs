using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가
    private Vector2 trapTarget;  //가시 이동 목표 지점
    private bool[] isTrap = new bool[3] { false, false, false };
    private float width;

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {
        // 발판을 리셋하는 처리
        //밟힘 상태를 리셋
        stepped = false;

        //장애물 수 만큼 루프
        for (int i = 0; i < obstacles.Length; i++)
        {
            //현재 순번의 장애물을 1/3 확률로 활성화
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
                isTrap[i] = true;
            }
            else
                obstacles[i].SetActive(false);
        }
    }
    private void Update()
    {
        trapTarget = new Vector2(transform.localPosition.x, (transform.localPosition.y + 1.0f));

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (isTrap[i] && transform.position.x >= width && !GameManager.instance.isGameover) //i번째 가시가 활성화되고 발판이 플레이어에게 다가왔으며, 게임 오버상태가 아닐 때
                obstacles[i].transform.Translate(new Vector2((transform.localPosition.x + 0.0f), (transform.localPosition.y + 1.0f)) * 1.0f * Time.deltaTime);
                //obstacles[i].transform.localPosition = Vector2.MoveTowards(transform.localPosition, trapTarget, 0.1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.collider.tag=="Player"&&!stepped)
        {
            //점수를 추가하고 밟힘 상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}