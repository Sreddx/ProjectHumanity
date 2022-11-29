using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;
using Fusion.Sockets;


public class ObjectsSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    public UnityEvent OnPlayerSpawned;

    public void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        Vector3 spawnPos = transform.position;
        NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity, player);
        _spawnedCharacters.Add(player, networkPlayerObject);
        OnPlayerSpawned?.Invoke();
    }
    
}
