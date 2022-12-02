using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;


public class DisableOtherCameras : NetworkBehaviour
{
    public int _playerRef;
    [SerializeField] public GameObject camNetObj;
    public ObjectsSpawnController objSpawner;

    private void Awake() {
        
        
    }
    public void DisableOtherPlayerCameras()
    {
        //Get player count
        objSpawner = GameObject.Find("ObjectsSpawnController").GetComponent<ObjectsSpawnController>();

        // Debug.Log("DisableOtherPlayerCameras");
        // Debug.Log("Player Ref: "+ _playerRef.ToString());
        // Debug.Log("Cam Net Obj: "+ camNetObj.GetComponent<NetworkObject>().InputAuthority);
        // GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        // foreach(GameObject cam in cameras){
        //     //If camera doesn't have the same input authority as the player

        //     if(cam.GetComponent<NetworkObject>().InputAuthority != _playerRef){
        //         cam.SetActive(false);
        //     }
        // }




        //Get input authority from network object
        
        



        // Debug.Log("DisableOtherPlayerCameras");
        // Debug.Log("This player joined: " + player);
        // //Find all cameras in game with tag MainCamera
        // //Disable all except the one with the same name as the player

        Debug.Log("DisableOtherPlayerCameras");
        Debug.Log("Player Ref: "+ objSpawner.PlayerCount.ToString());
        var plyrString = objSpawner.PlayerCount.ToString();
        gameObject.transform.GetChild(2).transform.name = plyrString;
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach(GameObject cam in cameras){
            //If camera doesn't have the same input authority as the player

            if(cam.GetComponent<RemoveParent>()._playerID != objSpawner.PlayerCount){
                cam.SetActive(false);
            }
        }
    }
}
