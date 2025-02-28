using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawningPool : MonoBehaviour
{
    // 몬스터를 스폰하는 '행위'를 관리하는 곳

    public GameObject[] Enemy;
    public List<GameObject> EnemyList = new List<GameObject>();

    const int _spawnLimit = 5;
    const float _spawnRange = 4;
    const float _minDistance = 20;
    const float _maxDistance = 30;
    GameObject _enemyPool;

    GameObject _player;

    Coroutine _coSpawningPool;
    WaitForSeconds _spawnInterval = new WaitForSeconds(1f);

    void Start()
    {
        //_enemyPool = new GameObject("EnemyPool");
        //for (int i = 0; i < Enemy.Length; i++)
        //{
        //    StartCoroutine(CoSpawnEnemy(i));
        //}
        ObjectManager.Instance.ResourceAllLoad();

        _player = GameObject.FindGameObjectWithTag("Player");

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
                Vector2 spawnPos = GetRandomPositionAround(_player.transform.position);

                PoolManager.Instance.GetObject<EnemyController>(spawnPos);
            }
        }

    }

    IEnumerator CoSpawnEnemy(int idx)
    {
        int spawnNum = _spawnLimit * 2;
        yield return new WaitForSeconds(1f);

        // 근방 원 안에서 랜덤 스폰 10마리
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (spawnNum <= 0)
                break;
            if (!EnemyList[i].activeSelf)
            {
                EnemyList[i].SetActive(true);
                EnemyList[i].transform.position = GetRandomPositionAround(_player.transform.position, _minDistance, _maxDistance);
                spawnNum--;
            }
        }

        for (int i = 0; i < spawnNum; i++)
        {
            GameObject enemy = Instantiate(
                Enemy[idx],
                GetRandomPositionAround(_player.transform.position, _minDistance, _maxDistance),
                Quaternion.identity
                );
            enemy.transform.parent = _enemyPool.transform;
            EnemyList.Add(enemy);
        }

        StartCoroutine(CoSpawnEnemy(idx));
    }

    public Vector2 GetRandomPositionAround(Vector2 origin, float min = 10f, float max = 20f)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(min, max);
        Vector2 spawnPos = new Vector2(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));

        return origin + spawnPos;
    }
}
