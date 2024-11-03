using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ghost2 : MonoBehaviour
{
    [SerializeField] private GameObject ghostModel;
    [SerializeField] private AudioSource as_ghost;

    private bool isFirstTrigger;

    private void Start()
    {
        ghostModel.SetActive(false);
        isFirstTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isFirstTrigger)
        {
            isFirstTrigger = false;
            ghostModel.GetComponentInChildren<GhostFollowPlayer>().ActivateGhost();
        }
    }
}