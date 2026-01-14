using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using System.Collections;

public class SummonSkulll : MonoBehaviour
{
    public GameObject projectilePrefab;
    public List<GameObject> summonPoint = new List<GameObject>();
    public List<GameObject> summonPoints = new List<GameObject>();
    public IEnumerator Summon()
    {
        for (int i = 0; i < summonPoint.Count; i++)
        {
            summonPoints.Add(Instantiate(projectilePrefab, summonPoint[i].transform.position, Quaternion.identity));
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < summonPoint.Count; i++)
        {
            Destroy(summonPoints[i]);
        }
    }
}
