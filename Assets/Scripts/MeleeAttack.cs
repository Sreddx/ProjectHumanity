using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour

{
    Collider weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            weapon.enabled = true;
        }
        
        else if (Input.GetButtonUp("Fire1")){
            weapon.enabled = false;
        }
        
    }
}
