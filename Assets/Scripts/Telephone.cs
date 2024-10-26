using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Telephone : InteractableObject
{
    [SerializeField] private GameObject dialogue;
    private string textDisplayed;

    private void Start()
    {
        textDisplayed = textToDisplay;
    }

    protected override void ProcessObject()
    {
        base.ProcessObject();

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

        isChecked = true;
    }
}
