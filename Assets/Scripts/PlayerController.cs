using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립. private로 선언하면 사망음 재생 안됨.
    public AudioClip jumpClip;
    public AudioClip coinClip;
    private float jumpForce = 700f; // 점프 힘

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태

    private const int playerMaxHP = 5;  //플레이어의 체력 최대값
    private int playerHP = playerMaxHP;   //플레이어의 현재 체력

    private bool isDamageCoolTime = false;  //데미지 쿨타임

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    private void Start()
    {
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
        {
            // 사망시 처리를 더 이상 진행하지 않고 종료
            return;
        }

        // 마우스 왼쪽 버튼을 눌렀으며 && 최대 점프 횟수(2)에 도달하지 않았다면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // 점프 횟수 증가
            jumpCount++;
            // 점프 직전에 속도를 순간적으로 제로(0, 0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘을 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // 오디오 소스 재생
            playerAudio.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면 (위로 상승 중)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity *= 0.5f;
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");

        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        playerAudio.clip = deathClip;
        // 사망 효과음 재생
        playerAudio.Play();

        // 속도를 제로(0, 0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        GameManager.instance.PlayerDamaged(playerHP);

        //게임 매니저의 게임오버 처리 실행
        GameManager.instance.OnPlayerDead();
    }

    private void Damage()
    {
        playerHP--;

        // deathClip 1회 재생
        playerAudio.PlayOneShot(deathClip);

        GameManager.instance.PlayerDamaged(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead) //플레이어와 가시 및 라바 블록 충돌
        {
            if (playerHP > 1)
            {
                if (!isDamageCoolTime)
                {
                    Damage();
                    isDamageCoolTime = true;    //일시적으로 데미지를 받지 않게 해준다.
                    StartCoroutine(DamageCoolTime());
                }
            }
            else if (playerHP <= 1 && !isDamageCoolTime)
                StartCoroutine(PlayerDestroy());
        }
        else if (other.tag == "Fall" && !isDead)
            Die();

        if(other.tag == "Coin" && !isDead)
        {
            playerAudio.PlayOneShot(coinClip);
            GameManager.instance.AddScore(1);   //코인 먹으면 1점 추가
            Destroy(other.gameObject);  //먹은 코인 맵에서 삭제
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        isGrounded = false;
    }

    private IEnumerator PlayerDestroy() //플레이어 사망시 플레이어 오브젝트를 소멸시켜주는 코루틴
    {
        Die();
        yield return new WaitForSeconds(3.0f);  //3초 후에
        Destroy(this.gameObject);   //플레이어 오브젝트 소멸
    }

    private IEnumerator DamageCoolTime()    //데미지를 입고 1.5초 동안은 다른 장애물과 충돌해도 일시적으로 무적으로 만들어주는 코루틴
    {
        yield return new WaitForSeconds(3.0f);  //3초간 무적
        isDamageCoolTime = false;
    }
}
