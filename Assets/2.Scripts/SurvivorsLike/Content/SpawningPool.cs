using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawningPool : MonoBehaviour
{
    //
    // 몬스터를 스폰하는 '행위'를 관리하는 곳
    //

    public GameObject[] Enemy;

    const int _spawnLimit = 30;
    const float _spawnRange = 4;
    const float _minDistance = 20;
    const float _maxDistance = 30;

    Coroutine _coSpawningPool;
    WaitForSeconds _spawnInterval = new WaitForSeconds(1f);
    int _nextStage = 1;

    void Start()
    {
        ObjectManager.Instance.ResourceAllLoad();

        // _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        ObjectManager.Instance.Spawn<PlayerController>(Vector2.zero);
        ObjectManager.Instance.Spawn<SwordController>(Vector2.zero);

        if (_coSpawningPool == null)
        {
            _coSpawningPool = StartCoroutine(CoSpawnEnemy());
        }
    }

    IEnumerator CoSpawnEnemy()
    {
        while (true)
        {
            yield return _spawnInterval;

            for (int i = 0; i < _spawnLimit; i++)
            {
                Vector2 spawnPos = GetRandomPositionAround(ObjectManager.Instance.Player.transform.position);

                PoolManager.Instance.GetObject<EnemyController>(spawnPos);
            }
        }
    }
        
    public Vector2 GetRandomPositionAround(Vector2 origin, float min = 10f, float max = 20f)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(min, max);
        Vector2 spawnPos = new Vector2(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));

        return origin + spawnPos;
    }
}
