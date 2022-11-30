using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    
    public UnitHealth _enemyHealth = new UnitHealth(100,100); //Instantiate Unit health class for enemy health
    public Rigidbody rb;
    [SerializeField] private Renderer _enemyRenderer;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent<int> OnEnemyAttacked;
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
        //StartCoroutine(EnemyAttackPlayer(_enemyHitbox));   
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
        //Freeze transform
        transform.position = transform.position;
        //Add force to rigidbody
        rb.AddForce(direction *5);
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

    private void EnemyAttackPlayer(Collider col){
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitboxes"));
        foreach (var c in cols)
        {
            if(c.tag == "Player"){
                OnEnemyAttacked?.Invoke(10);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        EnemyAttackPlayer(_enemyHitbox);
    }

    private void EnemyKilled(){
        Debug.Log("Enemy Killed");
        Destroy(gameObject);
    }

    private void OnDisable() {
        OnDeath?.RemoveListener(EnemyKilled);
    }
}
