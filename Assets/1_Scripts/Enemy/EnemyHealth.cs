using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    public string EnemyName;
    public GameObject childObject;
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
        childObject = this.gameObject.transform.GetChild(1).gameObject;
    }
    void OnEnable() => Hp = maxHP;
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
        Hp -= damage;
        StartCoroutine(Hit());
    }

    private void Die()
    {
        SummonManager.instance.DropXP(gameObject);
        SummonManager.instance.returnObject(EnemyName, this.gameObject);
    }
    IEnumerator Hit()
    {
        Color color = childObject.GetComponent<Renderer>().material.color;
        this.childObject.GetComponent<Renderer>().material.color = new Color(1,0.1f,0.1f);
        yield return new WaitForSeconds(0.1f);
        this.childObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
