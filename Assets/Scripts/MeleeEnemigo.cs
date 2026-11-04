using UnityEngine;

public class MeleeEnemigo : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        FindPlayer();

       // _maxHealth = 5;
        
    }

    void ChasePlayer()
    {

    }

    public override void Attack()
    {
       // base.Attack();
    }
}
