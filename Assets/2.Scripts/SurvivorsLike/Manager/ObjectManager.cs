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
    private GameObject _enemyResource;
    private GameObject _shotResource;
    private GameObject _explosionResource;
    private GameObject _pickaxeResource;
    private GameObject _swordResource;
    
    public void ResourceAllLoad()
    {
        _enemyResource = Resources.Load<GameObject>(Define.Enemy1Path);
    }
    
    public T Spawn<T>(Vector3 spawnPos) where T : MonoBehaviour
    {
        Type type = typeof(T);
        if(type==typeof(EnemyController))
        {
            GameObject obj = Instantiate(_enemyResource, spawnPos, Quaternion.identity);
            // GetOrAddComponent로 수정?
            EnemyController enemyController = obj.GetComponent<EnemyController>();
            Enemies.Add(enemyController);
            return enemyController as T;
        }
        return null;
    }

    public void DeSpwan<T>(T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(false);
    }
}

