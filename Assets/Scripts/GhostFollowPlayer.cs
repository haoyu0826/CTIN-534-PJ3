using System.Collections;
using UnityEngine;

public class GhostFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject model_ghost;
    [SerializeField] private float approachSpeed = 2f;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private AudioClip jumpscareClip;
    [SerializeField] private AudioSource ghostAudio;

    private bool isFollowing = false;
    private bool isJumpScared = false;

    private void Update()
    {
        if (isFollowing && !isJumpScared)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(model_ghost.transform.position, player.position);

        Vector3 directionToPlayer = (player.position - model_ghost.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        model_ghost.transform.rotation = Quaternion.Slerp(model_ghost.transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (distanceToPlayer > stopDistance)
        {
            model_ghost.transform.position += directionToPlayer * approachSpeed * Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isJumpScared)
            {
                StartCoroutine(JumpScare());
            }
        }
    }

    private IEnumerator JumpScare()
    {
        isJumpScared = true;
        isFollowing = false;

        model_ghost.transform.position = player.position - player.forward * 0.5f;
        ghostAudio.clip = jumpscareClip;
        ghostAudio.loop = false;
        ghostAudio.Play();

        yield return new WaitForSeconds(ghostAudio.clip.length);

        RemoveGhost();
    }

    private void RemoveGhost()
    {
        isFollowing = false;
        isJumpScared = false;
        model_ghost.SetActive(false);
    }

    public void ActivateGhost()
    {
        isFollowing = true;
        model_ghost.SetActive(true);
        ghostAudio.Play();
    }
}
