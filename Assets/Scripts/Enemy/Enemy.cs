using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public UnitHealth _enemyHealth = new UnitHealth(100,100); //Instantiate Unit health class for enemy health
    public MeleeAttack attack;
    public Rigidbody rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
         
    }

    IEnumerator blink(){
        for(int i = 0; i < 3; i++){
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GetHit(Vector3 direction){

        EnemyTakeDamage(); //Call method to take damage
        StartCoroutine(blink()); //Call blink for visual damage feedback
        if(attack.lightMelee == true){

            Vector3 force = direction * 5 + Vector3.up * 1;
            //Debug.Log(force);
            rb.AddForce(force, ForceMode.Impulse);
            this.transform.parent = null;

        }else{
            Vector3 force = direction * 2 + Vector3.up * 5;
            //Debug.Log(force);
            rb.AddForce(force, ForceMode.Impulse);
            this.transform.parent = null;
        }
        
    }
    private void EnemyTakeDamage() {
        if(attack.lightMelee == true){
            _enemyHealth.DmgUnit(attack.lightDamage);
        }else{
            _enemyHealth.DmgUnit(attack.heavyDamage);
        }

        if(_enemyHealth.Health <= 0){
            Destroy(gameObject);
        }
        
    }
}
