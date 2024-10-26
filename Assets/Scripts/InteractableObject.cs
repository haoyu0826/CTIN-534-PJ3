using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string textToDisplay;
    public bool isChecked = false;

    public string GetTextToDisplay()
    {
        return textToDisplay;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.instance.onTarget)
        {
            ProcessObject();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    protected virtual void ProcessObject()
    {

    }
}
