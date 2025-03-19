using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    // 오브젝트를 실제로 '스폰'하는 곳

    private PlayerController _player;
    public PlayerController Player { get => _player; }

    public HashSet<EnemyController> Enemies { get; set; } = new HashSet<EnemyController>();

    private GameObject _playerResource;

    private GameObject _enemyDogResource;
    private GameObject _enemyHoodResource;
    private GameObject _enemySlimeResource;
    private GameObject _enemyGolemResource;
    private GameObject _enemyReinforcedGolemResource;

    private GameObject _shotResource;
    private GameObject _golemShotResource;
    private GameObject _explosionResource;
    private GameObject _bleedingResource;

    private GameObject _pickaxeResource;
    private GameObject _swordResource;
    private GameObject _auraResource;

    private GameObject _magnetResource;
    private GameObject _coinResource;
    private GameObject _breadResource;
    private GameObject _exp1Resource;
    private GameObject _exp2Resource;
    private GameObject _exp3Resource;
    private GameObject _exp4Resource;
    private GameObject _exp5Resource;

    public void ResourceAllLoad()
    {
        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);

        _enemyDogResource = Resources.Load<GameObject>(Define.EnemyDogPath);
        _enemyHoodResource = Resources.Load<GameObject>(Define.EnemyHoodPath);
        _enemySlimeResource = Resources.Load<GameObject>(Define.EnemySlimePath);
        _enemyGolemResource = Resources.Load<GameObject>(Define.EnemyGolemPath);
        _enemyReinforcedGolemResource = Resources.Load<GameObject>(Define.EnemyReinforcedGolemPath);

        _shotResource = Resources.Load<GameObject>(Define.ShotPath);
        _golemShotResource = Resources.Load<GameObject>(Define.GolemShotPath);
        _explosionResource = Resources.Load<GameObject>(Define.ExplosionPath);
        _bleedingResource = Resources.Load<GameObject>(Define.BleedingPath);

        _pickaxeResource = Resources.Load<GameObject>(Define.PickaxePath);
        _swordResource = Resources.Load<GameObject>(Define.SwordPath);
        _auraResource = Resources.Load<GameObject>(Define.AuraPath);


        _magnetResource = Resources.Load<GameObject>(Define.MagnetPath);
        _coinResource = Resources.Load<GameObject>(Define.CoinPath);
        _breadResource = Resources.Load<GameObject>(Define.BreadPath);
        _exp1Resource = Resources.Load<GameObject>(Define.Exp1Path);
        _exp2Resource = Resources.Load<GameObject>(Define.Exp2Path);
        _exp3Resource = Resources.Load<GameObject>(Define.Exp3Path);
        _exp4Resource = Resources.Load<GameObject>(Define.Exp4Path);
        _exp5Resource = Resources.Load<GameObject>(Define.Exp5Path);
    }

    public T Spawn<T>(Vector3 spawnPos, Quaternion quaternion = default) where T : BaseController
    {
        Type type = typeof(T);
        // 플레이어 스폰 + Main Camera script component 부착
        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);
            PlayerController playerController = obj.GetComponent<PlayerController>();
            _player = playerController;
            Camera.main.gameObject.AddComponent<CameraController>();
            return playerController as T;
        }
        #region MonsterSpawn
        // 개 몬스터 스폰
        else if (type == typeof(DogController))
        {
            GameObject obj = Instantiate(_enemyDogResource, spawnPos, Quaternion.identity);
            DogController dogController = obj.GetComponent<DogController>();
            Enemies.Add(dogController);
            return dogController as T;
        }
        // 후드 몬스터 스폰
        else if (type == typeof(HoodController))
        {
            GameObject obj = Instantiate(_enemyHoodResource, spawnPos, Quaternion.identity);
            HoodController hoodController = obj.GetComponent<HoodController>();
            Enemies.Add(hoodController);
            return hoodController as T;
        }
        // 슬라임 몬스터 스폰
        else if (type == typeof(SlimeController))
        {
            GameObject obj = Instantiate(_enemySlimeResource, spawnPos, Quaternion.identity);
            SlimeController slimeController = obj.GetComponent<SlimeController>();
            Enemies.Add(slimeController);
            return slimeController as T;
        }
        // 골렘 몬스터 스폰
        else if (type == typeof(GolemController))
        {
            GameObject obj = Instantiate(_enemyGolemResource, spawnPos, Quaternion.identity);
            GolemController golemController = obj.GetComponent<GolemController>();
            Enemies.Add(golemController);
            return golemController as T;
        }
        // 강화 골렘 몬스터 스폰
        else if (type == typeof(ReinforcedGolemController))
        {
            GameObject obj = Instantiate(_enemyReinforcedGolemResource, spawnPos, Quaternion.identity);
            ReinforcedGolemController reinforcedGolemController = obj.GetComponent<ReinforcedGolemController>();
            Enemies.Add(reinforcedGolemController);
            return reinforcedGolemController as T;
        }
        #endregion

        #region WeaponSpawn
        // 소드 무기 스폰
        else if (type == typeof(SwordController))
        {
            GameObject obj = Instantiate(_swordResource, spawnPos, Quaternion.identity);
            SwordController swordController = obj.GetComponent<SwordController>();

            return swordController as T;
        }
        // 곡괭이 무기 스폰
        else if (type == typeof(PickaxeController))
        {
            GameObject obj = Instantiate(_pickaxeResource, spawnPos, Quaternion.identity);
            PickaxeController pickaxeController = obj.GetComponent<PickaxeController>();

            return pickaxeController as T;
        }
        // 오오라 스폰
        else if (type == typeof(AuraController))
        {
            GameObject obj = Instantiate(_auraResource, spawnPos, Quaternion.identity);
            AuraController auraController = obj.GetComponent<AuraController>();

            return auraController as T;
        }
        #endregion

        #region ItemSpawn
        // 자석 스폰
        else if (type == typeof(Magnet))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_magnetResource, spawnPos, Quaternion.identity);
            Magnet magnet = obj.GetComponent<Magnet>();

            return magnet as T;
        }
        // 코인 스폰
        else if (type == typeof(Coin))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_coinResource, spawnPos, Quaternion.identity);
            Coin coin = obj.GetComponent<Coin>();

            return coin as T;
        }
        // 체력 회복 빵 스폰
        else if (type == typeof(Bread))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_breadResource, spawnPos, Quaternion.identity);
            Bread bread = obj.GetComponent<Bread>();

            return bread as T;
        }
        // lv1 exp 아이템 스폰
        else if (type == typeof(Exp_Lv1))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_exp1Resource, spawnPos, Quaternion.identity);
            Exp_Lv1 expItem = obj.GetComponent<Exp_Lv1>();

            return expItem as T;
        }
        // lv2 exp 아이템 스폰
        else if (type == typeof(Exp_Lv2))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_exp2Resource, spawnPos, Quaternion.identity);
            Exp_Lv2 expItem = obj.GetComponent<Exp_Lv2>();

            return expItem as T;
        }
        // lv3 exp 아이템 스폰
        else if (type == typeof(Exp_Lv3))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_exp3Resource, spawnPos, Quaternion.identity);
            Exp_Lv3 expItem = obj.GetComponent<Exp_Lv3>();

            return expItem as T;
        }
        // lv4 exp 아이템 스폰
        else if (type == typeof(Exp_Lv4))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_exp4Resource, spawnPos, Quaternion.identity);
            Exp_Lv4 expItem = obj.GetComponent<Exp_Lv4>();

            return expItem as T;
        }
        // lv5 exp 아이템 스폰
        else if (type == typeof(Exp_Lv5))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_exp5Resource, spawnPos, Quaternion.identity);
            Exp_Lv5 expItem = obj.GetComponent<Exp_Lv5>();

            return expItem as T;
        }
        #endregion

        #region ShotSpawn
        else if (type == typeof(GolemShotController))
        {
            GameObject obj = Instantiate(_golemShotResource, spawnPos, quaternion);
            GolemShotController golemShotController = obj.GetComponent<GolemShotController>();
            return golemShotController as T;
        }
        #endregion

        return null;
    }

    public void DeSpwan<T>(T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(false);
    }

    public void ExplosionEffect()
    {
        Instantiate(_explosionResource, ObjectManager.Instance.Player.transform.position, Quaternion.identity);
    }

    public void BleedingEffect()
    {
        Instantiate(_bleedingResource, ObjectManager.Instance.Player.transform.position, Quaternion.identity);
    }

    protected override void Clear()
    {
        base.Clear();
        Enemies.Clear();
        Resources.UnloadUnusedAssets();
    }
}