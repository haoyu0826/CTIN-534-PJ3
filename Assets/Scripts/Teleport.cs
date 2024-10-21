using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teleport_end;
    private CharacterController characterController;

    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterController.enabled = false;
        player.transform.position = teleport_end.transform.position;
        characterController.enabled = true;
    }
}
