using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public AudioClip bgm;
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text timeText; //타이머를 출력할 UI 텍스트
    public Text hpText; //HP를 출력할 UI 텍스트
    public Text levelText;    //스테이지를 알려주는 UI 텍스트

    public GameObject pauseText;  //게임 일시정지를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    public static int currentLevel = 1;    //현재 레벨

    private int score = 0; // 게임 점수

    private int currentHP = 5; //플레이어 HP

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        playerAudio.clip = bgm;
        playerAudio.Play();

        levelText.text = "Stage: " + currentLevel;  //현재 레벨 UI 출력
    }

    void Update() {

        // 게임을 재시작할 수 있게 하는 처리
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1)  //R 누르면 현재 씬 재시작. 일시정지 상태에서는 작동 안함.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (!isGameover)
            timeText.text = "Time: " + (int)Time.timeSinceLevelLoad;

        if (Input.GetKeyDown(KeyCode.P) && !isGameover)
            GamePause();

        if(Input.GetKeyDown(KeyCode.Q) && Time.timeScale == 1)
            SceneManager.LoadScene("Title");

        if (score >= 20 && !isGameover) //점수가 20점 이상이면 다음 레벨로 넘어가는 스크립트
        {
            if (GameManager.currentLevel == 1)
                SceneManager.LoadScene("LevelComplete");
            else
                SceneManager.LoadScene("Ending");
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore) {
        if (!isGameover)
        {
            //점수 증가
            score += newScore;
            scoreText.text = "Score: " + score;
        }
    }

    public void PlayerDamaged(int damage)
    {
        if (currentLevel >= 2)
        {
            if (score > 2)  //스코어가 음수가 되는 것을 방지하기 위해
                score -= 2;
            else
                score = 0;
        }
        currentHP -= damage;
        hpText.text = "HP: " + currentHP + " / 5";
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() {
        isGameover = true;
        gameoverUI.SetActive(true);
        playerAudio.Stop();
    }

    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseText.SetActive(true);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseText.SetActive(false);
        }
    }
}