using UnityEngine;

public class LaunchAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // ���ư� ��ü ������
    public Transform firePoint;         // ���� ���� (ĳ���� ��)
    public Transform player;            // �÷��̾��� ��ġ
    public float speed = 10f;
    public void Launch()
    {
        // 1. ������Ʈ ����
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // 2. �÷��̾ ���� ���� ��� (Ÿ�� ��ġ - �� ��ġ)
        Vector3 direction = (player.position - firePoint.position).normalized;

        // 3. ����ü�� �ӵ� �ο� (Rigidbody ��� ��)
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}
