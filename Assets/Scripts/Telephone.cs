using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Telephone : InteractableObject
{
    [SerializeField] private GameObject dialogue;
    private string textDisplayed;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioToPlay;
    [SerializeField] private Light[] lightToShutDown;

    private void Start()
    {
        textDisplayed = textToDisplay;
    }

    protected override void ProcessObject()
    {
        base.ProcessObject();

        audioSource.Stop();

        PlayerManager.instance.pm.canMove = false;
        PlayerManager.instance.mm.canMove = false;

        textToDisplay = "";

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

    private void CloseDialogue()
    {
        dialogue.SetActive(false);

        PlayerManager.instance.pm.canMove = true;
        PlayerManager.instance.mm.canMove = true;

        textToDisplay = textDisplayed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (audioToPlay != null)
        {
            audioSource.clip = audioToPlay;
            audioSource.Play();
        }

        if (lightToShutDown != null)
        {
            for (int i = 0; i < lightToShutDown.Length; i++)
            {
                lightToShutDown[i].enabled = false;
            }
        }

        isChecked = true;
    }
}
