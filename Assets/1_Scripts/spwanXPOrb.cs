using UnityEngine;

public class spwanXPOrb : MonoBehaviour
{
    [SerializeField] private GameObject xpOrbPrefab;

    public void ReturnObject(GameObject target)
    {
        GameObject orb = Instantiate(xpOrbPrefab);
        orb.transform.position = target.transform.position;
        orb.SetActive(true);
    }
}
