using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VamSpawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public List<GameObject> EnemyList = new List<GameObject>();

    const int _spawnLimit = 5;
    const float _spawnRange = 4;
    GameObject _enemyPool;
    Vector2[] _initPos = {  new Vector2(-10, -5), new Vector2(-10, 5),
                            new Vector2(10, -5), new Vector2(10, 5)};

    void Start()
    {
        _enemyPool = new GameObject("EnemyPool");
        for (int i = 0; i < Enemy.Length; i++)
        {
            StartCoroutine(CoSpawnEnemy(i));
        }
    }

    IEnumerator CoSpawnEnemy(int idx)
    {
        int spawnNum;
        yield return new WaitForSeconds(0.5f);

        // 각각의 스폰 지점 근방에서 다섯 마리씩 스폰
        for (int i = 0; i < _initPos.Length; i++)
        {
            spawnNum = _spawnLimit;
            if (spawnNum <= 0)
                break;
            for (int j = 0; j < EnemyList.Count; j++)
            {
                if (!EnemyList[j].activeSelf)
                {
                    EnemyList[j].SetActive(true);
                    EnemyList[j].transform.position = 
                        new Vector2(Random.Range(_initPos[i].x - _spawnRange / 2, _initPos[i].x + _spawnRange / 2),
                                    Random.Range(_initPos[i].y - _spawnRange / 2, _initPos[i].y + _spawnRange / 2)); ;
                    spawnNum--;
                }
            }

            for (int j = 0; j < spawnNum; j++)
            {
                GameObject enemy = Instantiate(Enemy[idx], 
                        new Vector2(Random.Range(_initPos[i].x - _spawnRange / 2, _initPos[i].x + _spawnRange / 2),
                                    Random.Range(_initPos[i].y - _spawnRange / 2, _initPos[i].y + _spawnRange / 2)),
                                    Quaternion.identity);
                enemy.transform.parent = _enemyPool.transform;
                EnemyList.Add(enemy);
            }
        }

        StartCoroutine(CoSpawnEnemy(idx));
    }
}
