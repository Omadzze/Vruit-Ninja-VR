using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // лист со всеми геймобъектами которые мы будем инстантиатить
    public GameObject[] prefabs;
    // место в которых мы хотим делать инстатиат этих объектов
    public Transform[] spawnPoints;

    public float spawnTimer = 2;

    private float timer;

    public float velocityIntensite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            // randomly select transform and prefabs that will be instantiated
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
            GameObject spawnedPrefab = Instantiate(randomPrefab, randomPoint.position, randomPoint.rotation);

            timer -= spawnTimer;

            // velocity - поднимает наши фрукты наверх в воздух
            Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
            rb.velocity = randomPoint.forward * velocityIntensite;
        }
    }
}
