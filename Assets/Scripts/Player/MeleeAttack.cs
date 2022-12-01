using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour

{
    //Attack properties
    public float range = 100f;
    public float Knockback = 250;
    [SerializeField] private int _lightDamage;
    [SerializeField] private int _heavyDamage;
    public bool lightMelee;
    public bool heavyMelee;


    public GameObject orientation;
    public GameObject Enemy;
    Collider weapon;
    Transform weaponTransform; 


    public int LightMeleeDamage
    {
        get { return _lightDamage; }
        set { _lightDamage = value; }
    }

     public int HeavyMeleeDamage
    {
        get { return _heavyDamage; }
        set { _heavyDamage = value; }
    }


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
            Debug.Log("O");
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
