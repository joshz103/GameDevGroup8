using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPushbackBoss : MonoBehaviour
{
    public EnemyBehaviorBoss enemy;
    private float pushPower = 0.05f;
    //Enemy pushback

    public void OnTriggerStay(Collider collision)
    {
        if (!enemy.isDead())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector3 pushDir = transform.position - collision.transform.position;
                pushDir.y *= 0;
                collision.GetComponent<CharacterController>().Move(-pushDir * pushPower);
            }
        }
    }
}
