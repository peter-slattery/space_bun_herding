using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Timer = 3;
    public SpriteRenderer bombRenderer;
    public float blinkRate = 0.2f;
    public float explosionLength = 2;

    public GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Detonate(Timer));
    }

    IEnumerator Detonate(float waitTime)
    {
        var endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            bombRenderer.color = Color.black;
            yield return new WaitForSeconds(blinkRate);
            bombRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkRate);
        }

        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        GameObject bomb = GameObject.Instantiate(ExplosionPrefab);
        bomb.transform.position = this.transform.position;
        bomb.transform.parent = this.transform;

        yield return new WaitForSeconds(explosionLength);
        Object.Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
