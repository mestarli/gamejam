using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(DestroyProjectile());
    }
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
        
    }
}
