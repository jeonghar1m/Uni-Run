using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageScript : MonoBehaviour
{
    private float moveSpeed = 2.0f; //텍스트 이동 속도
    private float destroyTime = 2.0f;   //텍스트가 생기고나서부터 파괴되기 까지의 시간
    public int damage;
    TextMeshPro damageText;
    Color damageTextColor;
    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<TextMeshPro>();
        damageText.text = damage.ToString();
        damageTextColor = damageText.color;
        Invoke("DestroyObject", destroyTime);   //맨 밑에 있는 destroyObject 소환
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime));    //데미지 효과가 위로 올라가는 효과
        damageTextColor.a = Mathf.Lerp(damageTextColor.a, 0, Time.deltaTime * destroyTime); //0에 가까워짐
        damageText.color = damageTextColor; //초기화
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
