// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

// # Project 
using DG.Tweening;

public class ScreenTouch : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField]
    private float       zoomViewSize;
    [SerializeField]
    private float       zoomingSpeed;

    private Vector3     defaultCameraPosition;
    private float       defaultCameraViewSize;

    private bool        isZooming;
    private bool        isZoomIn;

    private void Awake()
    {
        defaultCameraPosition = Camera.main.transform.position;
        defaultCameraViewSize = Camera.main.orthographicSize;

        Debug.Log(defaultCameraViewSize);

        isZooming             = false;
        isZoomIn              = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray              = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit     = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (!isZooming && !isZoomIn && hit.collider.CompareTag("Screen"))
            {
                isZooming = true;
                isZoomIn = !isZoomIn;

                ZoomInScreen(hit.transform);
            }
            else if (!isZooming && isZoomIn && (hit.collider == null || hit.collider.CompareTag("Finish")))
            {
                isZooming = true;
                isZoomIn = !isZoomIn;

                ZoomOutScreen();
            }
        }
    }

    private void ZoomInScreen(Transform detectedScreen)
    {
        Vector3 zoomPosition = new Vector3
        (
            detectedScreen.position.x, 
            detectedScreen.position.y, 
            Camera.main.transform.position.z
        );

        Sequence sequence = DOTween.Sequence();

        Tween tr0 = Camera.main.transform.DOMove(zoomPosition, zoomingSpeed);
        Tween tr1 = Camera.main.DOOrthoSize(zoomViewSize, zoomingSpeed);

        sequence.Append(tr0);
        sequence.Join(tr1);

        sequence.Play().OnComplete(() =>
        {
            Camera.main.transform.position = zoomPosition;
            Camera.main.orthographicSize   = zoomViewSize;
            isZooming = false;
        });
    }

    private void ZoomOutScreen()
    {
        Sequence sequence = DOTween.Sequence();

        Tween tr0 = Camera.main.transform.DOMove(defaultCameraPosition, zoomingSpeed);
        Tween tr1 = Camera.main.DOOrthoSize(defaultCameraViewSize, zoomingSpeed);

        sequence.Append(tr0);
        sequence.Join(tr1);

        sequence.Play().OnComplete(() =>
        {
            Camera.main.transform.position = defaultCameraPosition;
            Camera.main.orthographicSize   = defaultCameraViewSize;
            isZooming = false;
        });
    }
}