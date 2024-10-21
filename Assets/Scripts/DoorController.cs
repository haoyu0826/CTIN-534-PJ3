using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            anim.SetBool("openDoor", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            anim.SetBool("openDoor", false);
    }
}

