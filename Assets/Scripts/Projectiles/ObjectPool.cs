using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private List<GameObject> pool = new List<GameObject>();
    private int amountToPool = 2;
    
    [SerializeField] GameObject fireBallPrefab;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(fireBallPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        return null;
    }
}
