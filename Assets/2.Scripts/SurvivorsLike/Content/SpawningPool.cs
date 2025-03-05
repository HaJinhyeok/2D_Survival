using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawningPool : MonoBehaviour
{
    //
    // 몬스터를 스폰하는 '행위'를 관리하는 곳
    //

    public GameObject LevelUpPanel;

    const int _stageInterval = 300;
    const float _spawnRange = 4;
    const float _minDistance = 20;
    const float _maxDistance = 30;

    int _spawnLimit = 20;
    float _spawnInterval = 1f;
    int _nextLevel = 200;
    int _rank = 0;

    void OnEnable()
    {
        Perks.Initialize();

        ObjectManager.Instance.ResourceAllLoad();

        ObjectManager.Instance.Spawn<PlayerController>(Vector2.zero);

        GameManager.Instance.OnScoreChanged += NextLevel;

        StartCoroutine(CoSpawnEnemy());
    }

    IEnumerator CoSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            for (int i = 0; i < _spawnLimit; i++)
            {
                Vector2 spawnPos = GetRandomPositionAround(ObjectManager.Instance.Player.transform.position);

                int rand = Random.Range(0, 2);
                if (rand == 0)
                    PoolManager.Instance.GetObject<DogController>(spawnPos);
                if (rand == 1)
                    PoolManager.Instance.GetObject<HoodController>(spawnPos);
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

    public void NextLevel()
    {
        if (GameManager.Instance.Score >= _nextLevel)
        {
            _nextLevel += _stageInterval * ++_rank;
            // _spawnInterval = Mathf.Max(_spawnInterval - 0.1f, 0.3f);
            _spawnLimit = Mathf.Min(_spawnLimit + 10, 100);

            // Level Up Perks
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
            LevelUpPanel.SetActive(true);
        }
    }
}
