using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool canOpen = false;
    public int index = 0;
    [SerializeField] private Animator animatorDoor;
    
    private string actualState;

    const string cerrandose = "DoorClosed";
    const string abriendose = "DoorOpen";
    const string cerrado = "Closed";
    

     void ChangeAnimationState(string newState)
    {
        if (actualState == newState) return;
        animatorDoor.Play(newState);
        actualState = newState;
    }

    

    public void ChangeDoorState()
    {
        canOpen = !canOpen;
        if(canOpen)
        {
            ChangeAnimationState(abriendose);
        }
        else
        {
            ChangeAnimationState(cerrandose);
        }
    }

   

   
}
