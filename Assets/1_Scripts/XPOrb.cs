using UnityEngine;


public class XPOrb : MonoBehaviour
{
    public int xpAmount = 10;
    public float moveSpeed = 25f;

    private Transform player;

    void OnEnable()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        LevelScore.instance.AddXp(xpAmount);

        gameObject.SetActive(false);
    }
}