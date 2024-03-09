using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageVisual : MonoBehaviour
{
    public SpriteRenderer visual;

    Color full = new Color(255, 255, 255, 255);
    Color invisible = new Color(255, 255, 255, 0);

    // Start is called before the first frame update
    void Start()
    {
        fadeOutInstant();
    }

    public void fadeIn()
    {
        visual.color = full;
    }

    public void fadeOutInstant()
    {
        visual.color = invisible;
    }

    IEnumerator fadeOut()
    {
        Color c = full;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            visual.color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void damageFlash()
    {
        Debug.Log("Damage flash initialized!");
        //StopCoroutine(fadeOut());
        StopAllCoroutines();
        fadeIn();
        StartCoroutine(fadeOut());
    }

}
