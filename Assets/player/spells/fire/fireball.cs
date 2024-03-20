using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class fireball : MonoBehaviour
{
    Rigidbody body;
    private float pushPower = 50f;
    GameObject player;
    public GameObject explosion;

    public GameObject AOEDamage;

    private playerstats stats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pushDir = transform.position - player.transform.position;
        pushDir.y = 0f;
        body = GetComponent<Rigidbody>();
        body.AddForce(pushDir * pushPower );
        StartCoroutine(despawn());
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(AOEDamage, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(AOEDamage, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
