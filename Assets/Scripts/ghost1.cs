using System.Collections;
using UnityEngine;

public class ghost1 : MonoBehaviour
{
    [SerializeField] private GameObject model_ghost;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject collider_door;

    private void Start()
    {
        model_ghost.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateGhost());
        }
    }

    private IEnumerator ActivateGhost()
    {
        model_ghost.SetActive(true);
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        model_ghost.SetActive(false);
        Destroy(model_ghost);
        Destroy(collider_door);
        Destroy(this);
    }
}