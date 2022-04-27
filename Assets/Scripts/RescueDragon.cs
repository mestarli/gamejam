using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueDragon : MonoBehaviour
{
    [SerializeField] private GameObject jaula;
    [SerializeField] private GameObject Lokijaula;
    [SerializeField] private GameObject Loki;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            jaula.gameObject.SetActive(false);
            Lokijaula.gameObject.SetActive(false);
            Loki.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
