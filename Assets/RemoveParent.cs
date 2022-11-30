using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class RemoveParent : MonoBehaviour
{
    public Dictionary<PlayerRef, NetworkObject> SpawnedPlayers = new Dictionary<PlayerRef, NetworkObject>();
    public PlayerRef CurrentPlayerRef;
    private IEnumerator Start() {
        yield return new WaitForSeconds(1f);
        foreach(KeyValuePair<PlayerRef, NetworkObject> entry in SpawnedPlayers)
        {
            if(entry.Key != CurrentPlayerRef){
                entry.Value.gameObject.SetActive(false);
            }
        }
        transform.SetParent(null);
    }
}
