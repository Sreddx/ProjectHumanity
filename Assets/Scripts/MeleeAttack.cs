using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour

{
    public float range = 100f;
    public float Knockback = 250;
    public GameObject orientation;
    public GameObject Enemy;
    Collider weapon;
    Transform weaponTransform; 


    // Start is called before the first frame update
    void Start()
    {
        /*
        weapon = GetComponent<Collider>();
        weaponTransform = gameObject.transform;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            /*weapon.enabled = true;
            Rotate weapon and make it bigger
            weaponTransform.Rotate(0, 99, 0);
            weaponTransform.localScale = new Vector3(0.50f, 1f, 0.50f);
            */
            ShootRayCast();
        }
        
        else if (Input.GetButtonUp("Fire1")){
            /*weapon.enabled = false;
            Return weapon to default values
            weaponTransform.Rotate(0, 55, 0);
            weaponTransform.localScale = new Vector3(0.10f, 0.50f, 0.23f);
            */
        }
        
    }

    void ShootRayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, orientation.transform.forward);
        
        if(Physics.Raycast(ray, out hit, range))
        {
            if(hit.transform.TryGetComponent<Enemy>(out Enemy ts)){
                ts.GetHit(ray.direction);
                Debug.Log("Hit");
            }
        }
    }

    /*private void Punch(){
        Enemy.transform.position += transform.forward * Time.deltaTime * Knockback;

    }*/
}
