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

        if (distanceToPlayer > stopDistance)
        {
            Vector3 direction = (player.position - model_ghost.transform.position).normalized;
            model_ghost.transform.position += direction * approachSpeed * Time.deltaTime;
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
