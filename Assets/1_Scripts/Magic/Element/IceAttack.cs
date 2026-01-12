using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IceAttack : MonoBehaviour
{
    public int maxIceStack = 3;
    public int currentIceStack = 0;
    public float iceDuration = 3f;
    public GameObject fireExplosionPrefab;
    public Image IceRing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Ice());
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    IEnumerator Ice()
    {
        while (currentIceStack > 0)
        {
            yield return new WaitForSeconds(1f);
            iceDuration -= 1f;
            if (iceDuration <= 0f)
            {
                iceDuration = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
