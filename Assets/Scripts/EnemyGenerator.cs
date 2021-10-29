using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float spawnXOffset = 2;
    [SerializeField] private float spawnYMax = 4.2F;
    [SerializeField] private float spawnYMin = -4.8F;

    [SerializeField] private float initialSpawnDelay = 2.0F;
    [SerializeField] private float minEnemyDelay = 1.0F;
    [SerializeField] private float maxEnemyDelay = 3.0F;

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
        _nextSpawnDelay = Random.Range(minEnemyDelay, maxEnemyDelay);
    }

    private void SpawnEnemy()
    {
        var cam = Camera.main;
        var x = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane)).x
                + spawnXOffset;
        var y = Random.Range(spawnYMin, spawnYMax);
        var spawnPosition = new Vector2(x, y);
        Debug.Log($"Spawning enemy at {spawnPosition}");
        var enemy = Instantiate(enemyPrefab, this.transform);
        enemy.position = spawnPosition;
    }
}