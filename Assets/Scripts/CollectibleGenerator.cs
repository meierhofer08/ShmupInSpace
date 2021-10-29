using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] collectiblePrefabs;
    [SerializeField] private float spawnXOffset = 2;
    [SerializeField] private float spawnYMax = 4F;
    [SerializeField] private float spawnYMin = -4F;

    [SerializeField] private float initialSpawnDelay = 5.0F;
    [SerializeField] private float minDelay = 20;
    [SerializeField] private float maxDelay = 30;

    private float _nextSpawnDelay;

    private void Start()
    {
        _nextSpawnDelay = initialSpawnDelay;
    }

    private void Update()
    {
        if (_nextSpawnDelay > 0)
        {
            _nextSpawnDelay -= Time.deltaTime;
            return;
        }

        SpawnEnemy();
        _nextSpawnDelay = Random.Range(minDelay, maxDelay);
    }

    private void SpawnEnemy()
    {
        var cam = Camera.main;
        var x = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane)).x
                + spawnXOffset;
        var y = Random.Range(spawnYMin, spawnYMax);
        var index = Random.Range(0, collectiblePrefabs.Length);
        var spawnPosition = new Vector2(x, y);
        var enemy = Instantiate(collectiblePrefabs[index], this.transform);
        enemy.position = spawnPosition;
    }
}