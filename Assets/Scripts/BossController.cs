using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float life = 400;
    private float maxlife = 400;
    private float randomDistanceY = 33.5f;
    
    [SerializeField] private GameObject enemySword;
    [SerializeField] private Transform respawnPoint;
    private bool isFireAttackAvailable = true;

    [SerializeField] private Slider barraVida;
    [SerializeField] private AudioSource damageSonido;

    void FixedUpdate()
    {
        
        // Para la barra de vida del boss
        barraVida.value = life / maxlife;
        if(gameObject.activeSelf)
        {

            // Get distance from objects
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            
            if ( transform.position.y >= 30f && transform.position.y < 31f)
            {
                randomDistanceY = 33.5f;
            }
            if (transform.position.y >= 33f  && transform.position.y < 34f)
            {
                randomDistanceY = 30.7f;
            }

            
            //Manten las distancias
            if (distance >= minDistance)
            {
                Vector3 newDistance = new Vector3(_player.transform.position.x,randomDistanceY , _player.transform.position.z - minDistance);

                transform.position = Vector3.MoveTowards(transform.position, newDistance , speed * Time.deltaTime);
            }
            
            if (distance <= minDistance)
            {
                Vector3 newDistance = new Vector3(_player.transform.position.x, randomDistanceY, _player.transform.position.z - maxDistance);
                transform.position = Vector3.MoveTowards(transform.position, newDistance, speed * Time.deltaTime);
            }
            
            if(distance <= maxDistance && distance >= minDistance)
            {
                if (isFireAttackAvailable)
                {
                    SwordAttack();
                    StartCoroutine(makeSwordAvailable());  
                }
                
            }
        }    
    }
    public void TakeDamage(int damage)
    {
        damageSonido.Play();
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
        if (other.gameObject.CompareTag("SpecialDamage"))
        {
            Destroy(other.gameObject);
            TakeDamage(3);
        }
    }

    private void SwordAttack()
    {
        GameObject fire = Instantiate(enemySword, respawnPoint.position, Quaternion.identity);
        fire.GetComponent<Rigidbody>().AddForce(respawnPoint.forward * 600);
        isFireAttackAvailable = false;
    }

    IEnumerator makeSwordAvailable()
    {
        yield return new WaitForSeconds(15f);
        isFireAttackAvailable = true;

    }
}
