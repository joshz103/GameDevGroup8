using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class offense_up_collectable : MonoBehaviour
{
    private playerstats stats;
    public GameObject offense_up;

    // Start is called before the first frame update
    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collected offense up");
        if (other.gameObject.CompareTag("Player"))
        {
            stats.addOffense(1f);
            Destroy(offense_up);
        }
    }
}
