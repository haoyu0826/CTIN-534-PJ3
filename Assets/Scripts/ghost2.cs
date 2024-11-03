using System.Collections;
using UnityEngine;

public class ghost2 : MonoBehaviour
{
    [SerializeField] private GameObject ghostModel;
    [SerializeField] private AudioSource as_ghost;

    private void Start()
    {
        ghostModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ghostModel.GetComponentInChildren<GhostFollowPlayer>().ActivateGhost();
        }
    }
}