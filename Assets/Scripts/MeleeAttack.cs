using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour

{
    public float range = 100f;
    public float Knockback = 250;
    public GameObject orientation;
    public GameObject Enemy;
    [SerializeField] public float lightDamage;
    [SerializeField] public float heavyDamage;
    public bool lightMelee;
    public bool heavyMelee;


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
        Action();
    }

    void Action(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            lightMelee = true;
            ShootRayCast();
            lightMelee = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse4)){
            heavyMelee = true;
            ShootRayCast();
            heavyMelee = false;
            
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
