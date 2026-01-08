using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetDetector : MonoBehaviour
{
    public List<Transform> detectedTargets = new List<Transform>();
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        // "Monster" 태그를 가진 오브젝트가 들어오면 리스트에 추가
        if (other.CompareTag("Enemy"))
        {
            detectedTargets.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 몬스터가 범위를 벗어나면 리스트에서 제거
        if (other.CompareTag("Enemy"))
        {
            detectedTargets.Remove(other.transform);
        }
    }
    public Transform GetClosestEnemy()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;

        // 리스트를 돌며 거리 계산
        for (int i = detectedTargets.Count - 1; i >= 0; i--)
        {
            // 리스트에 있던 몬스터가 파괴되었을 경우를 대비해 null 체크
            if (detectedTargets[i] == null)
            {
                detectedTargets.RemoveAt(i);
                continue;
            }

            float distance = Vector3.Distance(detectedTargets[i].position, currentPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = detectedTargets[i];
            }
        }
        return closest;
    }
}
