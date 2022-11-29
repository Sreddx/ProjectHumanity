using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    
    
    public float HorizontalInput;
    public float VerticalInput;
    public bool Jump;
    public bool Sprint;
    public byte Buttons;
    public Vector3 Direction;
}
