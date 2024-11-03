using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("openDoor", true);
            audioSource.clip = openDoor;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("openDoor", false);
            audioSource.clip = closeDoor;
            audioSource.Play();
        }
    }
}