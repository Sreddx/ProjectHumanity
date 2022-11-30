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
    [SerializeField] Collider _enemyHitbox;

    private void OnEnable() {
        OnDeath.AddListener(EnemyKilled);
    }
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
         
    }
    
    void Update()
    {
        EnemyAttackPlayer(_enemyHitbox);   
    }

    IEnumerator blink(){
        for(int i = 0; i < 3; i++){
            _enemyRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            _enemyRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GetHit(Vector3 direction){

        EnemyTakeDamage(); //Call method to take damage
        
    }


    public void EnemyTakeDamage() {
        StartCoroutine(blink()); 
        _enemyHealth.DmgUnit(25);
        Debug.Log(_enemyHealth.Health);
       
        if(_enemyHealth.Health <= 0){
            OnDeath?.Invoke();
        }
        
    }

    public void EnemyAttackPlayer(Collider col){
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitboxes"));
        foreach (var c in cols)
        {
            if(c.tag == "Player"){
                Debug.Log("Hit player XD");
            }
        }
    }

    private void EnemyKilled(){
        Debug.Log("Enemy Killed");
        Destroy(gameObject);
    }

    private void OnDisable() {
        OnDeath?.RemoveListener(EnemyKilled);
    }
}
