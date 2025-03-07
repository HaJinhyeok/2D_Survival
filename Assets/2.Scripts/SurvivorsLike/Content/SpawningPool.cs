using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SpawnInfoStruct
{
    public int SpawnLimit;
    public float SpawnInterval;

}

public class SpawningPool : Singleton<SpawningPool>
{
    //
    // 몬스터를 스폰하는 '행위'를 관리하는 곳
    //

    public GameObject LevelUpPanel;

    public SpawnInfoStruct SpawnInfo = new SpawnInfoStruct()
    {
        SpawnLimit = 20,
        SpawnInterval = 1f
    };

    protected override void Initialize()
    {
        base.Initialize();
        Perks.Initialize();

        ObjectManager.Instance.ResourceAllLoad();

        ObjectManager.Instance.Spawn<PlayerController>(Vector2.zero);

        // GameManager.Instance.OnScoreChanged += NextLevel;

        StartCoroutine(CoSpawnEnemy());
    }

    IEnumerator CoSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnInfo.SpawnInterval);

            for (int i = 0; i < SpawnInfo.SpawnLimit; i++)
            {
                Vector2 spawnPos = GetRandomPositionAround(ObjectManager.Instance.Player.transform.position);

                int rand = Random.Range(0, 3);
                if (rand == 0)
                    PoolManager.Instance.GetObject<DogController>(spawnPos);
                if (rand == 1)
                    PoolManager.Instance.GetObject<HoodController>(spawnPos);
                if (rand == 2)
                    PoolManager.Instance.GetObject<SlimeController>(spawnPos);
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
