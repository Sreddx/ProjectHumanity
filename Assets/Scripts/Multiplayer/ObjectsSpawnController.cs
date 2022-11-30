using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;
using Fusion.Sockets;


public class ObjectsSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _camPrefab;
    private List<Transform> _camerasInGame = new List<Transform>();

    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    public UnityEvent OnPlayerSpawned;
    public UnityEvent<Transform> OnCameraSpawned;

    public void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        Vector3 spawnPos = transform.position;
        NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity, player);
        _spawnedCharacters.Add(player, networkPlayerObject);
        
        networkPlayerObject.transform.GetChild(2).GetComponent<RemoveParent>().SpawnedPlayers.Add(player, networkPlayerObject);
        networkPlayerObject.transform.GetChild(2).GetComponent<RemoveParent>().CurrentPlayerRef = player;
        
    }
    public void SpawnCam(NetworkRunner runner, NetworkObject player)
    {
        Vector3 spawnPos = transform.position;
        NetworkObject playerCam = runner.Spawn(_camPrefab, Vector3.zero, Quaternion.identity);
        //for each cam in game disable all except current player cam
        if(_camerasInGame.Count > 0){
            foreach(Transform cam in _camerasInGame){
                cam.gameObject.SetActive(false);
            }
        }
        _camerasInGame.Add(playerCam.transform);
        //OnCameraSpawned?.Invoke(player.transform);

        playerCam.transform.GetChild(1).GetComponent<MultiplayerThirdPersonCam>().LookForPlayer(player.transform);
    }

    
}
