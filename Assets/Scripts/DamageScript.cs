using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageScript : MonoBehaviour
{
    private float moveSpeed = 2.0f; //텍스트 이동 속도
    private float destroyTime = 2.0f;
    public int damage;
    TextMeshPro damageText;
    Color damageTextColor;
    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<TextMeshPro>();
        damageText.text = damage.ToString();
        damageTextColor = damageText.color;
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime));
        damageTextColor.a = Mathf.Lerp(damageTextColor.a, 0, Time.deltaTime * destroyTime);
        damageText.color = damageTextColor;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
