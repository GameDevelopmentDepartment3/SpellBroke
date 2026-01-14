using UnityEngine;

public class Damage : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f); // 2초 후에 오브젝트 파괴
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatsManager playerStats = other.GetComponent<PlayerStatsManager>();
            if (playerStats != null)
            {
                float damageAmount = 10f; // 예시로 20의 데미지를 입힘
                playerStats.AddHP(-damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
