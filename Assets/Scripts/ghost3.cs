using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost3 : MonoBehaviour
{
    [SerializeField] private GameObject model_ghost;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(model_ghost);
    }
}
