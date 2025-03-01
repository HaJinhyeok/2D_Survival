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
    private GameObject _enemyResource;
    private GameObject _shotResource;
    private GameObject _explosionResource;
    private GameObject _pickaxeResource;
    private GameObject _swordResource;

    public void ResourceAllLoad()
    {
        _enemyResource = Resources.Load<GameObject>(Define.Enemy1Path);
        _explosionResource = Resources.Load<GameObject>(Define.ExplosionPath);
    }

    public T Spawn<T>(Vector3 spawnPos) where T : BaseController
    {
        Type type = typeof(T);
        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);
            PlayerController playerController = obj.GetComponent<PlayerController>();
            _player = playerController;

            return playerController as T;
        }
        else if (type == typeof(EnemyController))
        {
            GameObject obj = Instantiate(_enemyResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent�� ����?
            EnemyController enemyController = obj.GetComponent<EnemyController>();
            Enemies.Add(enemyController);
            return enemyController as T;
        }
        else if (type == typeof(LetterController))
        {
            // �̷��� �ϸ� �ȵɰ� ������
            // �ʵ忡 ��� Ȥ�� ���� óġ �� ����Ǵ� letter �������� �����ϴ� �뵵�� ���°� ������
            GameObject obj = Instantiate(_explosionResource, spawnPos, Quaternion.identity);
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

