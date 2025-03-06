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

    private GameObject _shotResource;
    private GameObject _explosionResource;
    private GameObject _pickaxeResource;
    private GameObject _pickaxeDroppedResource;
    private GameObject _swordResource;
    private GameObject _swordDroppedResource;
    private GameObject _letterResource;

    private GameObject _coinResource;
    private GameObject _breadResource;

    public void ResourceAllLoad()
    {
        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);

        _enemyDogResource = Resources.Load<GameObject>(Define.EnemyDogPath);
        _enemyHoodResource = Resources.Load<GameObject>(Define.EnemyHoodPath);
        _enemySlimeResource = Resources.Load<GameObject>(Define.EnemySlimePath);

        _shotResource = Resources.Load<GameObject>(Define.ShotPath);
        _explosionResource = Resources.Load<GameObject>(Define.ExplosionPath);
        _pickaxeResource = Resources.Load<GameObject>(Define.PickaxePath);
        _pickaxeDroppedResource = Resources.Load<GameObject>(Define.PickaxeDroppedPath);
        _swordResource = Resources.Load<GameObject>(Define.SwordPath);
        _swordDroppedResource = Resources.Load<GameObject>(Define.SwordDroppedPath);
        _letterResource = Resources.Load<GameObject>(Define.LetterPath);

        _coinResource = Resources.Load<GameObject>(Define.CoinPath);
        _breadResource = Resources.Load<GameObject>(Define.BreadPath);
    }

    public T Spawn<T>(Vector3 spawnPos) where T : BaseController
    {
        Type type = typeof(T);
        // 플레이어 스폰 + Main Camera script component 부착
        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent로 수정?
            PlayerController playerController = obj.GetComponent<PlayerController>();
            _player = playerController;
            // GetOrAddComponent로 수정?
            Camera.main.gameObject.AddComponent<CameraController>();
            return playerController as T;
        }
        // 개 몬스터 스폰
        else if (type == typeof(DogController))
        {
            GameObject obj = Instantiate(_enemyDogResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent로 수정?
            DogController dogController = obj.GetComponent<DogController>();
            Enemies.Add(dogController);
            return dogController as T;
        }
        // 후드 몬스터 스폰
        else if (type == typeof(HoodController))
        {
            GameObject obj = Instantiate(_enemyHoodResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent로 수정?
            HoodController hoodController = obj.GetComponent<HoodController>();
            Enemies.Add(hoodController);
            return hoodController as T;
        }
        // 소드 무기 스폰
        else if (type == typeof(SwordController))
        {
            GameObject obj = Instantiate(_swordResource, spawnPos, Quaternion.identity);
            SwordController swordController = obj.GetComponent<SwordController>();

            return swordController as T;
        }
        // 소드 아이템 스폰
        else if (type == typeof(Sword))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_swordDroppedResource, spawnPos, Quaternion.identity);
            Sword sword = obj.GetComponent<Sword>();

            return sword as T;

        }
        // 곡괭이 무기 스폰
        else if (type == typeof(PickaxeController))
        {
            GameObject obj = Instantiate(_pickaxeResource, spawnPos, Quaternion.identity);
            PickaxeController pickaxeController = obj.GetComponent<PickaxeController>();

            return pickaxeController as T;
        }
        // 곡괭이 아이템 스폰
        else if (type == typeof(Pickaxe))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_pickaxeDroppedResource, spawnPos, Quaternion.identity);
            Pickaxe pickaxe = obj.GetComponent<Pickaxe>();

            return pickaxe as T;
        }
        // 주문서 무기 스폰
        else if (type == typeof(LetterController))
        {
            // 몬스터 처치 시 일정 확률로 드랍
            GameObject obj = Instantiate(_letterResource, spawnPos, Quaternion.identity);
            LetterController letterController = obj.GetComponent<LetterController>();

            return letterController as T;
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
}

