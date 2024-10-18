using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private Transform torch;
    private Transform cameraTransform;
    private Vector3 offset;
    private Quaternion initialRotation;

    void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        initialRotation = torch.localRotation;
        offset = torch.position - transform.position;
    }

    void LateUpdate()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);

        Vector3 rotatedOffset = cameraRotation * offset;
        torch.position = transform.position + rotatedOffset;

        torch.rotation = cameraRotation * initialRotation;
    }
}
