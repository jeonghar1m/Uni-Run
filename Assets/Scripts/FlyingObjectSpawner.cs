using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class FlyingObjectSpawner : MonoBehaviour
{
    public GameObject flyingLavaPrefab;
    private int count = 10; // 생성할 발판의 개수

    private float objectSpeed = 2.0f;

    private float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    private float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    private float yMin = -3.5f; // 배치할 위치의 최소 y값
    private float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = -20f; // 배치할 위치의 x 값

    private GameObject[] lavas; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 lavasPoolPosition = new Vector2(0, -35); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치

    private float lastSpawnTime; // 마지막 배치 시점


    void Start()
    {
        lavas = new GameObject[count];

        //count 만큼 루프하면서 비행체 생성
        for (int i = 0; i < count; i++)
            lavas[i] = Instantiate(flyingLavaPrefab, lavasPoolPosition, Quaternion.identity);
        //마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        //다음번 배치까지의 시간 간격 0초기화
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        //게임오버 상태에서는 미동작
        if (GameManager.instance.isGameover)
            return;
        //마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            //다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 설정
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정
            float lavasYPos = Random.Range(yMin, yMax);
            float blocksYPos = Random.Range(yMin, yMax);

            //사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            //이때 발판의 Platform 컴포넌트의 OnEnable 메서드 실행
            lavas[currentIndex].SetActive(false);
            lavas[currentIndex].SetActive(true);

            //현재 순번의 발판을 화면 왼쪽에 재배치
            lavas[currentIndex].transform.position = new Vector2(xPos, lavasYPos);

            //순번 넘기기
            currentIndex++;

            //마지막 순번에 도달했다면 순번 리셋
            if (currentIndex >= count)
                currentIndex = 0;
        }
    }
}
