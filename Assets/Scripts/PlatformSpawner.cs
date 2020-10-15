using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public GameObject blockPrefab; // 생성할 블록의 원본 프리팹
    private int count = 10; // 생성할 발판의 개수

    private float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    private float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    private float yMin = -3.5f; // 배치할 위치의 최소 y값
    private float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = -20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private GameObject[] blocks;    //미리 생성한 블록들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 platformsPoolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private Vector2 blocksPoolPosition = new Vector2(0, -30); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판과 블록들을 미리 생성
        //Count만큼의 공간을 가지는 새로운 발판 배열 생성
        platforms = new GameObject[count];
        blocks = new GameObject[count];

        //count 만큼 루프하면서 발판 생성
        for(int i=0;i<count;i++)
        {
            //platformPrefab을 원본으로 새 발판을 poolPosition 위치에 복제 생성
            //생성된 발판을 platforms, blocks 배열에 할당
            platforms[i] = Instantiate(platformPrefab, platformsPoolPosition, Quaternion.identity);
            blocks[i] = Instantiate(blockPrefab, blocksPoolPosition, Quaternion.identity);
        }
        //마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        //다음번 배치까지의 시간 간격 0초기화
        timeBetSpawn = 0f;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        //게임오버 상태에서는 미동작
        if (GameManager.instance.isGameover)
            return;
        //마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면
        if(Time.time>=lastSpawnTime+timeBetSpawn)
        {
            //기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            //다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 설정
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정
            float platformsYPos = Random.Range(yMin, yMax);
            float blocksYPos = Random.Range(yMin, yMax);

            //사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            //이때 발판의 Platform 컴포넌트의 OnEnable 메서드 실행
            platforms[currentIndex].SetActive(false);
            blocks[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);
            blocks[currentIndex].SetActive(true);

            //현재 순번의 발판을 화면 왼쪽에 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, platformsYPos);
            blocks[currentIndex].transform.position = new Vector2(xPos, blocksYPos);

            if(blocksYPos <= platformsYPos)             //블록의 y축이 플랫폼의 y축보다 작거나 같으면
                blocks[currentIndex].SetActive(false);  //해당 순서의 블록 제거

            //순번 넘기기
            currentIndex++;

            //마지막 순번에 도달했다면 순번 리셋
            if (currentIndex >= count)
                currentIndex = 0;
        }
    }
}