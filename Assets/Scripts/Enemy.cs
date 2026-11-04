using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _maxHealth;
    private float _attackDamage;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindPlayer()
    {

    }

    public void Patrol()
    {


    }

    public virtual void Attack()
    {
        Debug.Log("Ataque generico");
    }

}
