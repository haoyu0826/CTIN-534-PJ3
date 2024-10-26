using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance { get; set; }
    public bool onTarget = false;

    [SerializeField] private TextMeshProUGUI textToDisplay;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;

                textToDisplay.text = interactable.textToDisplay;
                textToDisplay.gameObject.SetActive(true);
            }
            else
            {
                onTarget = false;
                textToDisplay.gameObject.SetActive(false);
            }
        }
        else
        {
            onTarget = false;
            textToDisplay.gameObject.SetActive(false);
        }
    }
}