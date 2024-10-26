using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToClick : InteractableObject
{
    private Animator anim;
    private int clickCount = 0;
    [SerializeField] private int requiredClicks = 5;
    private AudioSource audioSource;
    [SerializeField] private AudioClip doorLock;
    [SerializeField] private AudioClip doorUnlock;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private AudioSource asToShutDown;
    private bool isDoorOpened = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void ProcessObject()
    {
        base.ProcessObject();

        if (isDoorOpened) return;

        if (clickCount < requiredClicks)
        {
            clickCount++;
            audioSource.clip = doorLock;
            audioSource.Play();
        }

        if (clickCount == requiredClicks)
        {
            textToDisplay = "";
            anim.SetBool("openDoor", true);
            audioSource.clip = doorUnlock;
            audioSource.Play();

            isDoorOpened = true;
            if (asToShutDown != null)
                asToShutDown.Stop();

            PlayerManager.instance.pm.canMove = false;
            PlayerManager.instance.mm.canMove = false;
            dialogue.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Button closeButton = dialogue.GetComponentInChildren<Button>();
            if (closeButton != null)
            {
                closeButton.onClick.RemoveAllListeners();
                closeButton.onClick.AddListener(CloseDialogue);
            }
        }
    }

    private void CloseDialogue()
    {
        dialogue.SetActive(false);

        PlayerManager.instance.pm.canMove = true;
        PlayerManager.instance.mm.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isChecked = true;
    }
}
