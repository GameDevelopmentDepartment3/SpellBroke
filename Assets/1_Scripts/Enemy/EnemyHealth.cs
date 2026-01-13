using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    public string EnemyName;
    public List<GameObject> childObject = new List<GameObject>();
    public GameObject damagePopupPrefab;
    public float Hp
    {
        get { return currentHP; }
        set
        {
            if (value <= 0f)
            {
                Die();
            }
            else if (value > maxHP)
                currentHP = maxHP;
            else
                currentHP = value;
            Debug.Log($"Enemy HP: {currentHP}/{maxHP}");
        }

    }
    private void Awake()
    {
        // 리스트가 null이라면 새로 생성
        if (childObject == null) childObject = new List<GameObject>();
        InitializeChildObjects();
    }
    private void InitializeChildObjects()
    {
        childObject.Clear();
        // 1단계 자식뿐만 아니라 하위의 모든 자식에서 Renderer를 찾음 (더 안전함)
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            // 자기 자신은 제외하고 싶다면 조건 추가 가능
            childObject.Add(rend.gameObject);
        }
    }
    void OnEnable()
    {
        // 풀링으로 재활성화될 때마다 리스트를 다시 점검하거나 색상을 초기화
        if (childObject == null || childObject.Count == 0)
        {
            InitializeChildObjects();
        }

        ResetColor();
        Hp = maxHP;
    }
    private void ResetColor()
    {
        if (childObject == null) return;

        for (int i = 0; i < childObject.Count; i++)
        {
            if (childObject[i] != null)
            {
                var renderer = childObject[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.white;
                }
            }
        }
    }
    void Start()
    {
        Hp = maxHP;
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }
    public void TakeDamage(float damage)
    {
        // 대미지 텍스트 생성 (적의 위치보다 조금 위쪽)
        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up * 5f, Quaternion.identity);
        // 데이터 전달
        popup.GetComponent<DamagePopup>().Setup((int)damage);
        Hp -= damage;
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Hit());
        }
    }

    private void Die()
    {
        SummonManager.instance.DropXP(gameObject);
        SummonManager.instance.returnObject(EnemyName, this.gameObject);
    }
    IEnumerator Hit()
    {
        for (int i = 0; i < childObject.Count; i++)
        {
            childObject[i].GetComponent<Renderer>().material.color = new Color(1,0.1f,0);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < childObject.Count; i++)
        {
            childObject[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
