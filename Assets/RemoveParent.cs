using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class RemoveParent : MonoBehaviour
{
    public int _playerID;
    private void Awake() {
        // yield return new WaitForSeconds(1f);
        // foreach(KeyValuePair<PlayerRef, NetworkObject> entry in SpawnedPlayers)
        // {
        //     if(entry.Key != CurrentPlayerRef){
        //         entry.Value.gameObject.SetActive(false);
        //     }
        // }
        //Change cam name to player Ref
        //transform.name = _playerID.ToString();
        //transform.SetParent(null);
    }
}
