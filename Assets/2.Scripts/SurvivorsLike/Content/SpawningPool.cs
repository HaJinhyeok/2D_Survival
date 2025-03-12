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

    public SpawnInfoStruct SpawnInfo = new SpawnInfoStruct()
    {
        SpawnLimit = Define.InitSpawnLimit,
        SpawnInterval = 1f
    };

    protected override void Initialize()
    {
        base.Initialize();
        Perks.Initialize();

        ObjectManager.Instance.ResourceAllLoad();

        ObjectManager.Instance.Spawn<PlayerController>(Vector2.zero);
        // ObjectManager.Instance.Spawn<PlayerController>(new Vector2(-399, -399));

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

                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                    case 1:
                        PoolManager.Instance.GetObject<DogController>(spawnPos);
                        break;
                    case 2:
                    case 3:
                        PoolManager.Instance.GetObject<HoodController>(spawnPos);
                        break;
                    case 4:
                        PoolManager.Instance.GetObject<SlimeController>(spawnPos);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public Vector2 GetRandomPositionAround(Vector2 origin, float min = 20f, float max = 30f)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(min, max);
        Vector2 spawnPos = new Vector2(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
        spawnPos += origin;
        if (spawnPos.x > Define.MapHalfSize - 1)
        {
            spawnPos.x = 2 * Define.MapHalfSize - spawnPos.x;
        }
        else if (spawnPos.x < -Define.MapHalfSize + 1)
        {
            spawnPos.x = -2 * Define.MapHalfSize - spawnPos.x;
        }
        if (spawnPos.y > Define.MapHalfSize - 1)
        {
            spawnPos.y = 2 * Define.MapHalfSize - spawnPos.y;
        }
        else if (spawnPos.y < -Define.MapHalfSize + 1)
        {
            spawnPos.y = -2 * Define.MapHalfSize - spawnPos.y;
        }

        return spawnPos;
    }
}
