using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class End1 : InteractableObject
{
    [SerializeField] private Image blackImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject end_dialogue;
    [SerializeField] private float dialogueDuration = 5f;

    protected override void ProcessObject()
    {
        PlayerManager.instance.pm.canMove = false;
        PlayerManager.instance.mm.canMove = false;

        blackImage.gameObject.SetActive(true);

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(DisplayEndDialogue());
    }

    private IEnumerator DisplayEndDialogue()
    {
        end_dialogue.gameObject.SetActive(true);

        yield return new WaitForSeconds(dialogueDuration);

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}