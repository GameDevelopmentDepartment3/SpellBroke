using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonManager : MonoBehaviour
{
    public static SummonManager instance;

    public TargetDetector detector;

    [SerializeField]
    private GameObject[] ObjectPoolingPrefabs;
    private Dictionary<string, GameObject> poolingObjectPrefabs = new Dictionary<string, GameObject>();

    private Dictionary<string, Queue<GameObject>> poolingObjectQueues = new Dictionary<string, Queue<GameObject>>();

    private List<GameObject> activeObjects = new List<GameObject>();

    public GameObject[] summonPoint;
    public float summonPointMoveDelay;
    
    [SerializeField] private GameObject xpOrbPrefab;
    private void InitializeObject(int initCount)
    {
        for (int i = 0; i < ObjectPoolingPrefabs.Length; i++)
        {
            poolingObjectPrefabs.Add(ObjectPoolingPrefabs[i].name, ObjectPoolingPrefabs[i]);
            poolingObjectQueues.Add(ObjectPoolingPrefabs[i].name, new Queue<GameObject>());
            for (int j = 0; j < initCount; j++)
            {
                poolingObjectQueues[ObjectPoolingPrefabs[i].name].Enqueue(CreateNewObject(ObjectPoolingPrefabs[i].name));
            }
        }
    }
    private GameObject CreateNewObject(string objectName)
    {
        var newObj = Instantiate(poolingObjectPrefabs[objectName], transform, true);
        newObj.SetActive(false);
        return newObj;
    }
    public GameObject GetObject(string objectName)
    {
        if (instance.poolingObjectQueues[objectName].Count > 0)
        {
            var obj = instance.poolingObjectQueues[objectName].Dequeue();
            obj.transform.SetParent(null);
            obj.SetActive(true);
            activeObjects.Add(obj);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject(objectName);
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            activeObjects.Add(newObj);
            return newObj;
        }
    }
    public void returnObject(string objectName, GameObject obj)
    {
        for (int i = detector.detectedTargets.Count - 1; i >= 0; i--)
        {
            Debug.Log($"Checking detected target at index {i}");
            if (detector.detectedTargets[i].gameObject == obj)
            {
                Debug.Log($"Removing {obj.name} from detected targets");
                detector.detectedTargets.Remove(detector.detectedTargets[i]);
            }
        }
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueues[objectName].Enqueue(obj);
        activeObjects.Remove(obj);
    }
    public void returnAll()
    {
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            returnObject(activeObjects[i].name.Replace("(Clone)", "").Trim(), activeObjects[i]);
        }
        activeObjects.Clear();
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            InitializeObject(3);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(MoveSummonPoint());
    }
    IEnumerator MoveSummonPoint()
    {
        while (true)
        {
            for (int i = 0; i < summonPoint.Length; i++)
            {
                summonPoint[i].transform.position = new Vector3(Random.Range(100,880), summonPoint[i].transform.position.y, Random.Range(100,880));
            }
            yield return new WaitForSeconds(summonPointMoveDelay);
        }
    }
    
    // xp
    public void DropXP(GameObject target)
    {
        GameObject orb = GetObject("XPOrb");
        orb.transform.position = target.transform.position;
    }
}
