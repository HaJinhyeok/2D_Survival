using UnityEngine;
using System.Collections;

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
        SpawnInterval = 2f
    };

    protected override void Initialize()
    {
        base.Initialize();
        Perks.Initialize();

        ObjectManager.Instance.ResourceAllLoad();

        ObjectManager.Instance.Spawn<PlayerController>(Vector2.zero);

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

    Vector2 GetRandomPositionAround(Vector2 origin, float min = 20f, float max = 30f)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(min, max);
        Vector2 spawnPos = new Vector2(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
        spawnPos += origin;

        //if (spawnPos.x > Define.MapHalfSize - 1)
        //{
        //    spawnPos.x = 2 * Define.MapHalfSize - spawnPos.x;
        //}
        //else if (spawnPos.x < -Define.MapHalfSize + 1)
        //{
        //    spawnPos.x = -2 * Define.MapHalfSize - spawnPos.x;
        //}
        //if (spawnPos.y > Define.MapHalfSize - 1)
        //{
        //    spawnPos.y = 2 * Define.MapHalfSize - spawnPos.y;
        //}
        //else if (spawnPos.y < -Define.MapHalfSize + 1)
        //{
        //    spawnPos.y = -2 * Define.MapHalfSize - spawnPos.y;
        //}

        return spawnPos;
    }

    public void OnBossWave(int wave, Vector2 origin)
    {
        if (wave == 5)
        {
            // golem wave
            for (int i = 0; i < 360; i += 4)
            {
                Vector2 spawnPos = new Vector2(25f * Mathf.Cos(i * Mathf.Deg2Rad), 25f * Mathf.Sin(i * Mathf.Deg2Rad));
                spawnPos += origin;
                PoolManager.Instance.GetObject<GolemController>(spawnPos);
            }
        }
        else if (wave == 10)
        {
            // reinforced golem wave
            for (int i = 0; i < 360; i += 5)
            {
                Vector2 spawnPos = new Vector2(25f * Mathf.Cos(i * Mathf.Deg2Rad), 25f * Mathf.Sin(i * Mathf.Deg2Rad));
                spawnPos += origin;
                PoolManager.Instance.GetObject<ReinforcedGolemController>(spawnPos);
            }
        }
        else if (wave == 15)
        {
            // reinforced golem with shooting attack wave
            for (int i = 0; i < 360; i += 20)
            {
                Vector2 spawnPos = new Vector2(25f * Mathf.Cos(i * Mathf.Deg2Rad), 25f * Mathf.Sin(i * Mathf.Deg2Rad));
                spawnPos += origin;
                PoolManager.Instance.GetObject<ReinforcedGolemController>(spawnPos);
            }
        }
    }
}
