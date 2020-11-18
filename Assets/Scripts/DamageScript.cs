using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageScript : MonoBehaviour
{
    private float moveSpeed = 2.0f; //텍스트 이동 속도
    private float alphaSpeed = 2.0f;    //투명도 변환 속도
    private float destroyTime = 2.0f;
    public int damage;
    TextMeshPro damageText;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<TextMeshPro>();
        damageText.text = damage.ToString();
        alpha = damageText.color;
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime);
        damageText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
