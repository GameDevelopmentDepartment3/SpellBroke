using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float _currentHP;
    public string enemyName;
    public GameObject damagePopupPrefab;
    public float Hp
    {
        get { return _currentHP; }
        private set
        {
            if (value <= 0)
            {
                _currentHP = 0;
                SummonManager.instance.returnObject(enemyName, this.gameObject);
            }
            else if (value > maxHP)
                _currentHP = maxHP;
            else
                _currentHP = value;
            Debug.Log($"Enemy {_currentHP}/{maxHP} HP remaining.");
        }
    }
    void Start()
    {
        _currentHP = maxHP;
    }
    public void TakeDamage(float damage)
    {
        // 대미지 텍스트 생성 (적의 위치보다 조금 위쪽)
        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        // 데이터 전달
        popup.GetComponent<DamagePopup>().Setup((int)damage);
        Hp -= damage;
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }
}
