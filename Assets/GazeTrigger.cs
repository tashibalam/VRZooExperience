using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GazeTrigger : MonoBehaviour
{
    // Time duration to trigger scene load after looking at an object
    public float gazeDuration = 2f;

    // Reference to the OVRCameraRig
    public OVRCameraRig ovrCameraRig;

    // Layer mask to filter which objects can be interacted with
    public LayerMask interactableLayer;

    // Internal variables
    private bool isGazing = false;
    private float gazeTimer = 0f;
    private BoxCollider gazedObjectCollider;
    private bool isAnimationPlaying = false;
    private Transform animatedObjectTransform; // Reference to the transform of the object being animated
    private Vector3 initialColliderSize; // Initial size of the Box Collider
    [SerializeField] private GameObject canvasObject; // Reference to the canvas object to toggle
    private bool isCanvasToggled = false;


    private void Start()
    {
        if (canvasObject != null)
            canvasObject.SetActive(false); // Hide the canvas initially
    }

    private void Update()
    {
        // Cast a ray from the center of the camera to detect objects being gazed at
        Ray ray = new Ray(ovrCameraRig.centerEyeAnchor.position, ovrCameraRig.centerEyeAnchor.forward);
        RaycastHit hit;

        // Perform a raycast and check if the object is interactable
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            // Check if the object has a Box Collider component
            BoxCollider boxCollider = hit.collider.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                // Start the gaze timer if not already gazing
                if (!isGazing)
                {
                    isGazing = true;
                    gazeTimer = 0f;
                    gazedObjectCollider = boxCollider;

                    // Start the activation animation and specify the scene index to load
                    StartAnimation(hit.collider.gameObject);
                }

                // Increment the timer
                gazeTimer += Time.deltaTime;

                // Check if the gaze duration has been reached
                if (gazeTimer >= gazeDuration)
                {
                    if (hit.collider.gameObject.CompareTag("LoadSceneSprite"))
                    {
                        // Get the scene index from the SceneIndexComponent of the interacted object
                        GazeSceneIndex sceneIndexComponent = hit.collider.GetComponent<GazeSceneIndex>();
                        if (sceneIndexComponent != null)
                        {
                            int sceneIndex = sceneIndexComponent.sceneIndex;
                            LoadNewScene(sceneIndex);
                        }
                    }
                    else if (hit.collider.gameObject.CompareTag("ToggleCanvasSprite") && !isCanvasToggled)
                    {
                        ToggleCanvas();
                        isCanvasToggled = true;
                    }

                }
            }
            else
            {
                // Reset the gaze timer if the object is not interactable
                ResetGazeTimer();
            }
        }
        else
        {
            // Reset the gaze timer if no object is being gazed at
            ResetGazeTimer();
        }
    }

    private void ResetGazeTimer()
    {
        isGazing = false;
        gazeTimer = 0f;

        // Stop the activation animation for the currently gazed object
        StopAnimation();

        // Reset the gazed object collider and animated object transform references
        if (gazedObjectCollider != null)
        {
            gazedObjectCollider.size = initialColliderSize;
            gazedObjectCollider = null;
        }

        animatedObjectTransform = null;
        isCanvasToggled = false;

    }

    private void LoadNewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void ToggleCanvas()
    {

        if (canvasObject != null)
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
        }
    }

    private void StartAnimation(GameObject targetObject)
    {
        if (!isAnimationPlaying)
        {
            isAnimationPlaying = true;
            animatedObjectTransform = targetObject.transform;

            // Get the initial size of the Box Collider
            BoxCollider boxCollider = targetObject.GetComponent<BoxCollider>();
            initialColliderSize = boxCollider.size;

            // Scale animation
            StartCoroutine(ScaleAnimation(1f, 2f, 0.5f));
        }
        else
        {
            // If the object doesn't have a BoxCollider, stop the animation and reset variables
            StopAnimation();
        }
    }

    private void StopAnimation()
    {
        if (isAnimationPlaying && animatedObjectTransform != null && gazedObjectCollider != null)
        {
            isAnimationPlaying = false;

            // Reset the scale of the object's transform
            animatedObjectTransform.localScale = Vector3.one;

            // Reset the size of the Box Collider
            gazedObjectCollider.size = initialColliderSize;
        }
    }

    private IEnumerator ScaleAnimation(float startScale, float targetScale, float duration)
    {
        float elapsedTime = 0f;

        // Ensure that the necessary objects and variables are not null
        if (animatedObjectTransform == null || gazedObjectCollider == null)
        {
            yield break; // Exit the coroutine if any required objects are null
        }

        Vector3 initialScale = animatedObjectTransform.localScale;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);

        while (elapsedTime < duration)
        {
            if (animatedObjectTransform == null || gazedObjectCollider == null)
            {
                yield break; // Exit the coroutine if any required objects become null
            }

            float t = elapsedTime / duration;
            float scale = Mathf.Lerp(startScale, targetScale, t);

            // Apply the scale animation to the object's transform
            animatedObjectTransform.localScale = Vector3.Lerp(initialScale, targetScaleVector, t);

            // Scale the Box Collider size
            Vector3 colliderSize = initialColliderSize * scale;
            gazedObjectCollider.size = colliderSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}