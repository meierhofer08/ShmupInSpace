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
    [SerializeField] private float stage1MinEnemyDelay = 1.0F;
    [SerializeField] private float stage1MaxEnemyDelay = 3.0F;

    [SerializeField] private int stage2Threshold = 1000;
    [SerializeField] private float stage2MinEnemyDelay = 1.0F;
    [SerializeField] private float stage2MaxEnemyDelay = 2.0F;

    [SerializeField] private int stage3Threshold = 3000;
    [SerializeField] private float stage3MinEnemyDelay = 0.5F;
    [SerializeField] private float stage3MaxEnemyDelay = 1.5F;
    

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
        CalcNextSpawnDelay();
    }

    private void CalcNextSpawnDelay()
    {
        var score = ScoreBehaviour.Instance.GetCurrentScore();
        
        float minDelay;
        float maxDelay;
        if (score < stage2Threshold)
        {
            minDelay = stage1MinEnemyDelay;
            maxDelay = stage1MaxEnemyDelay;
        }
        else if (score < stage3Threshold)
        {
            minDelay = stage2MinEnemyDelay;
            maxDelay = stage2MaxEnemyDelay;
        }
        else
        {
            minDelay = stage3MinEnemyDelay;
            maxDelay = stage3MaxEnemyDelay;
        }
        
        _nextSpawnDelay = Random.Range(minDelay, maxDelay);
    }

    private void SpawnEnemy()
    {
        var cam = Camera.main;
        var x = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane)).x
                + spawnXOffset;
        var y = Random.Range(spawnYMin, spawnYMax);
        var spawnPosition = new Vector2(x, y);
        var enemy = Instantiate(enemyPrefab, this.transform);
        enemy.position = spawnPosition;
    }
}