using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonManager : MonoBehaviour
{
    public static SummonManager instance;

    [SerializeField]
    private GameObject[] ObjectPoolingPrefabs;
    private Dictionary<string, GameObject> poolingObjectPrefabs = new Dictionary<string, GameObject>();

    private Dictionary<string, Queue<GameObject>> poolingObjectQueues = new Dictionary<string, Queue<GameObject>>();

    private List<GameObject> activeObjects = new List<GameObject>();

    public GameObject[] summonPoint;
    public float summonPointMoveDelay;

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
            DontDestroyOnLoad(gameObject);
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
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            instance.returnAll();
        }
    }
    IEnumerator MoveSummonPoint()
    {
        while (true)
        {
            for (int i = 0; i < summonPoint.Length; i++)
            {
                Vector2 randomPoint = Random.insideUnitCircle * 400f;
                Debug.Log($"Summon Point {i} moved to ({randomPoint.x}, {randomPoint.y})");
                summonPoint[i].transform.position = new Vector3(randomPoint.x, summonPoint[i].transform.position.y, randomPoint.y);
            }
            yield return new WaitForSeconds(summonPointMoveDelay);
        }
    }
}
