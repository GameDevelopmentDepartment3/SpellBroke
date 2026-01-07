using UnityEngine;

public class BombMonsterAttack : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("attack01", true);
            anim.SetBool("walk", false);
        }
    }
}
