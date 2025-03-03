using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    // ������Ʈ�� ������ '����'�ϴ� ��

    private PlayerController _player;
    public PlayerController Player { get => _player; }

    public HashSet<EnemyController> Enemies { get; set; } = new HashSet<EnemyController>();

    private GameObject _playerResource;

    private GameObject _enemyDogResource;
    private GameObject _enemyHoodResource;

    private GameObject _shotResource;
    private GameObject _explosionResource;
    private GameObject _pickaxeResource;
    private GameObject _swordResource;
    private GameObject _letterResource;

    private GameObject _coinResource;

    public void ResourceAllLoad()
    {
        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);

        _enemyDogResource = Resources.Load<GameObject>(Define.EnemyDogPath);
        _enemyHoodResource = Resources.Load<GameObject>(Define.EnemyHoodPath);

        _shotResource = Resources.Load<GameObject>(Define.ShotPath);
        _explosionResource = Resources.Load<GameObject>(Define.ExplosionPath);
        _pickaxeResource = Resources.Load<GameObject>(Define.PickaxePath);
        _swordResource = Resources.Load<GameObject>(Define.SwordPath);
        _letterResource = Resources.Load<GameObject>(Define.LetterPath);

        _coinResource = Resources.Load<GameObject>(Define.CoinPath);
    }

    public T Spawn<T>(Vector3 spawnPos) where T : BaseController
    {
        Type type = typeof(T);
        // �÷��̾� ���� + Main Camera script component ����
        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent�� ����?
            PlayerController playerController = obj.GetComponent<PlayerController>();
            _player = playerController;
            // GetOrAddComponent�� ����?
            Camera.main.gameObject.AddComponent<CameraController>();
            return playerController as T;
        }
        // �� ���� ����
        else if (type == typeof(DogController))
        {
            GameObject obj = Instantiate(_enemyDogResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent�� ����?
            DogController dogController = obj.GetComponent<DogController>();
            Enemies.Add(dogController);
            return dogController as T;
        }
        // �ĵ� ���� ����
        else if (type == typeof(HoodController))
        {
            GameObject obj = Instantiate(_enemyHoodResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent�� ����?
            HoodController hoodController = obj.GetComponent<HoodController>();
            Enemies.Add(hoodController);
            return hoodController as T;
        }
        // �ҵ� ���� ����
        else if (type == typeof(SwordController))
        {
            // ���� óġ �� ���� Ȯ���� ���
            GameObject obj = Instantiate(_swordResource, spawnPos, Quaternion.identity);

            return null;
        }
        // ��� ���� ����
        else if (type == typeof(PickaxeController))
        {
            // ���� óġ �� ���� Ȯ���� ���
            GameObject obj = Instantiate(_pickaxeResource, spawnPos, Quaternion.identity);

            return null;
        }
        // �ֹ��� ���� ����
        else if (type == typeof(LetterController))
        {
            // ���� óġ �� ���� Ȯ���� ���
            GameObject obj = Instantiate(_letterResource, spawnPos, Quaternion.identity);
            return null;
        }
        else if (type == typeof(Coin))
        {
            // ���� óġ �� ���� Ȯ���� ���
            GameObject obj = Instantiate(_coinResource, spawnPos, Quaternion.identity);
            return null;
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

