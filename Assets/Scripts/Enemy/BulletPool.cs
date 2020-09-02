using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //***********************//
    // This component creates an object pool for BULLETS and manages their reusing.
    // Get() method used for getting the reference for the first platform in queue.
    // ReturnToPool() method returns the game object back to queue and deactivates it.
    // It's called on timer on the ReturnToQueue component, but you should call it instead of usual Destroy() method.
    //***********************//

    [SerializeField] private GameObject bullet;
    [SerializeField] private int count = 10;
    private Queue<GameObject> bullets = new Queue<GameObject>();
    public static BulletPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CreatePool(count);
    }

    //create queue of BULLET objects with COUNT amount
    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newPlatform = Instantiate(bullet, transform);
            newPlatform.gameObject.SetActive(false);
            bullets.Enqueue(newPlatform);
        }
    }

    //returns used objects to pool
    public void ReturnToPool(GameObject instance)
    {
        instance.gameObject.SetActive(false);
        bullets.Enqueue(instance);
    }

    //returns first bullet in queue and dequeues it
    public GameObject Get()
    {
        if (bullets.Count == 0)
        {
            CreatePool(1);
        }

        return bullets.Dequeue();
    }
}
