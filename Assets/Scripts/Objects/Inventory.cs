using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public static bool[] keys = new bool[6];

    private void Start()
    {
        keys[0] = true;
    }
}
