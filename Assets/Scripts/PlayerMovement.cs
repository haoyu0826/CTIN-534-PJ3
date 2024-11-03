using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region PlayerMovement
    [SerializeField] private float speed = 5f;
    private float gravity = -9.81f;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving = false;
    public bool canMove = true;
    #endregion

    #region Flashlight Control
    [SerializeField] private Light flashlight;
    [SerializeField] private float flashlightFadeDuration = 0.5f;
    public float flashlightBattery = 100f;
    [SerializeField] private float batteryConsumptionRate;

    private Coroutine flashlightCoroutine;
    private bool isFlashlightOn = false;

    [SerializeField] private AudioSource footstepAudioSource;     // 用于脚步声的AudioSource
    [SerializeField] private AudioSource flashlightAudioSource;   // 用于手电筒开关声的AudioSource
    #endregion

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        flashlight.enabled = false;
        flashlight.intensity = 0f;
    }

    void Update()
    {
        if (canMove)
        {
            // Player movement
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * vertical + right * horizontal).normalized;

            isMoving = moveDirection != Vector3.zero;

            controller.Move(moveDirection * speed * Time.deltaTime);

            if (!isGrounded)
                velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }

        // 控制步行音效
        HandleWalkingAudio();

        // Flashlight toggle control
        if (Input.GetKeyDown(KeyCode.Space))
            ToggleFlashlight();

        // Update flashlight direction
        if (flashlight != null && cameraTransform != null)
        {
            flashlight.transform.position = transform.position;
            flashlight.transform.forward = cameraTransform.forward;
        }

        // Update flashlight battery
        if (isFlashlightOn && flashlightBattery > 0)
        {
            if (canMove)
                flashlightBattery -= batteryConsumptionRate * Time.deltaTime;

            if (flashlightBattery <= 0)
            {
                flashlightBattery = 0;
                TurnOffFlashlight();
            }
        }
    }

    private void HandleWalkingAudio()
    {
        // 如果玩家在移动且没有播放音效，则播放步行音效
        if (isMoving && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        // 如果玩家停止移动且音效正在播放，则停止步行音效
        else if (!isMoving && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Pause();
        }
    }

    private void ToggleFlashlight()
    {
        if (flashlightBattery > 0)
        {
            // Toggle flashlight on/off
            isFlashlightOn = !isFlashlightOn;

            if (flashlightCoroutine != null)
                StopCoroutine(flashlightCoroutine);

            if (isFlashlightOn)
                flashlightCoroutine = StartCoroutine(FadeLight(flashlight, 0f, 1f, flashlightFadeDuration));
            else
                flashlightCoroutine = StartCoroutine(FadeLight(flashlight, flashlight.intensity, 0f, flashlightFadeDuration));

            // 播放手电筒开关声音
            flashlightAudioSource.Play();
        }
        else
            TurnOffFlashlight();
    }

    private void TurnOffFlashlight()
    {
        if (isFlashlightOn)
        {
            isFlashlightOn = false;

            if (flashlightCoroutine != null)
                StopCoroutine(flashlightCoroutine);

            flashlightCoroutine = StartCoroutine(FadeLight(flashlight, flashlight.intensity, 0f, flashlightFadeDuration));
        }
    }

    IEnumerator FadeLight(Light light, float startIntensity, float targetIntensity, float fadeDuration)
    {
        float elapsedTime = 0f;
        light.enabled = true;

        while (elapsedTime < fadeDuration)
        {
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        light.intensity = targetIntensity;

        if (targetIntensity == 0f)
            light.enabled = false;

        flashlightCoroutine = null;
    }
}
