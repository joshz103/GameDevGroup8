using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyHP hp;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();
    }

    public void checkIfDead()
    {
        if (hp.getHP() <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }






}
