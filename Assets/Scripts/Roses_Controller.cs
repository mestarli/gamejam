using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roses_Controller : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
