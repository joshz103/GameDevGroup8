using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class TestSpawnText : MonoBehaviour
{
    [SerializeField]private Transform damagePopup;

    public TextMeshPro text;

    private Vector3 textPos;
    private Vector3 offset = new Vector3(0,2,0);
    private Vector3 offset2;
    

    public float lifetime;

    public PlayerAttackDamage playerDMG;

    // Start is called before the first frame update
    void Start()
    {
        textPos = GameObject.FindGameObjectWithTag("PlayerHitbox").transform.position;
        //playerDMG = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackDamage>();
        playerDMG = GameObject.FindGameObjectWithTag("PlayerHitbox").GetComponent<PlayerAttackDamage>();
        text = transform.GetComponent<TextMeshPro>();
        gameObject.transform.position = textPos + offset;
        offset2 = gameObject.transform.position + offset;
        StartCoroutine(MoveOverSeconds(gameObject, offset2, 2f));
        text.text = playerDMG.getRecentDamageStr();
        Destroy(gameObject, lifetime);
        //Instantiate(damagePopup,Vector3.zero, Quaternion.identity);
        //currentScale = textMeshTransform.localScale;
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }



}
