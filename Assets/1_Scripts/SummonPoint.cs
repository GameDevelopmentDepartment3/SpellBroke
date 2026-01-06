using System.Collections;
using UnityEngine;

public class SummonPoint : MonoBehaviour
{
    public float summonDelay;
    public string summonMonsterName;
    void Start()
    {
        StartCoroutine(SummonCoroutine());
    }

    IEnumerator SummonCoroutine()
    {
        while (true)
        {
            var monster = SummonManager.instance.GetObject(summonMonsterName);
            monster.transform.position = this.transform.position;
            yield return new WaitForSeconds(summonDelay);
        }
    }
}
