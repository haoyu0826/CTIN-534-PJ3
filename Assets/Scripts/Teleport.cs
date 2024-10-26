using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teleport_fail_end;
    [SerializeField] private GameObject teleport_success_end;
    private CharacterController characterController;

    [SerializeField] private List<InteractableObject> interactables;

    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterController.enabled = false;

        foreach (var interactable in interactables)
        {
            if (interactable.isChecked == false)
            {
                player.transform.position = teleport_fail_end.transform.position;
                characterController.enabled = true;
                return;
            }
        }

        player.transform.position = teleport_success_end.transform.position;
        characterController.enabled = true;
    }
}
