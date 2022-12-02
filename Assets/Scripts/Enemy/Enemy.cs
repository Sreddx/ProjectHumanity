using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    
    public UnitHealth _enemyHealth = new UnitHealth(100,100); //Instantiate Unit health class for enemy health
    public MeleeAttack attack;
    public Rigidbody rb;
    [SerializeField] private Renderer _enemyRenderer;
    [SerializeField] private UnityEvent OnDeath;

    private void OnEnable() {
        OnDeath.AddListener(EnemyKilled);
    }
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
         
    }

    IEnumerator blink(){
        for(int i = 0; i < 3; i++){
            _enemyRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            _enemyRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /*public void GetHit(Vector3 direction){

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
        
    }*/
    
    public void EnemyTakeDamage() {
        _enemyHealth.DmgUnit(10);

        if(_enemyHealth.Health <= 0){
            OnDeath?.Invoke();
        }
        
    }

    

    private void EnemyKilled(){
        Debug.Log("Enemy Killed");
        Destroy(gameObject);
    }

    private void OnDisable() {
        OnDeath?.RemoveListener(EnemyKilled);
    }

    private void OnCollisionEnter(Collision other) {
        //If get hit by laser attack
        if(other.gameObject.name == "shot_prefab(Clone)"){
            EnemyTakeDamage();
            //Destroy shot 
            //Destroy(other.gameObject);
            StartCoroutine(blink());
        }
        

    }
}
