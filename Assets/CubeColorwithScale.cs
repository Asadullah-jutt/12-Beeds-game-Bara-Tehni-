using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CubeColorwithScale : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Notification ;

    
    public float NotificationShowtime = 5f;

    public Boolean internetreq = false;

    public float colorChangeSpeed = 2f; // Speed of color change
    public float hoverScaleFactor = 1.2f; // Factor by which the cube scales when hovered over
    public float emissionIntensity = 1f; // Intensity of emission

    private Color originalColor; // Original color of the cube
    private Renderer cubeRenderer; // Reference to the cube's renderer
    private Vector3 originalScale; // Original scale of the cube
    private bool isHovering = false; // Flag to track if mouse is hovering over the cube

    public GameObject enable ;
    public GameObject disable ;



    void Start()
    {
        // Get the cube's renderer component
        cubeRenderer = GetComponent<Renderer>();
        // Store the original color of the cube
        originalColor = cubeRenderer.material.color;
        // Store the original scale of the cube
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        
        isHovering = true; // Mouse is hovering over the cube
        // Start the color transition coroutine
        StartCoroutine(ChangeColorAndScaleAndEmission(GetRandomColor(), hoverScaleFactor));
    }

    void OnMouseExit()
    {
        isHovering = false; // Mouse is not hovering over the cube
        // Start the color transition coroutine
        StartCoroutine(ChangeColorAndScaleAndEmission(originalColor, 1f));
    }

    // Coroutine to gradually change the cube's color, scale, and emission
    IEnumerator ChangeColorAndScaleAndEmission(Color targetColor, float scaleFactor)
    {
        Color startColor = cubeRenderer.material.color;
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * colorChangeSpeed;

            // Interpolate color
            cubeRenderer.material.color = Color.Lerp(startColor, targetColor, t);
            // Interpolate emission color
            cubeRenderer.material.SetColor("_EmissionColor", targetColor * emissionIntensity);
            // Interpolate scale
            if(enable != null)
                transform.localScale = Vector3.Lerp(startScale, originalScale * scaleFactor, t);

            yield return null;
        }
    }

    private void DisableGameObject()
    {
        if (Notification != null)
        {
            // Deactivate the GameObject
            Notification.SetActive(false);
            Debug.Log("GameObject has been deactivated.");
        }
    }

    void check2Player()
    {
        
        UIMongodb.Instance.Check2PlayerStatus();
        
    }

    // Function to generate a random color
    Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void OnMouseDown()
    {
        
        if(internetreq == true)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error: No internet connection.");
                Notification.SetActive(true);
                Invoke("DisableGameObject", NotificationShowtime);

                // Handle the error (e.g., display an error message to the user)
                return;
            }
        }
        // Print a message when the cube is clicked
        if(enable != null)
        {
            enable.SetActive(true);
            disable.SetActive(false);
        }
    }

}
