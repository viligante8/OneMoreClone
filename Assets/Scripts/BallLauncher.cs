using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 mouseStartpoint, mouseEndpoint;
    private LaunchPreview launchPreview;

    [SerializeField]
    private GameObject ballPrefab;

    private void Awake()
    {
        launchPreview = GetComponent<LaunchPreview>();
    }

    void Update()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void EndDrag()
    {
        var direction = Vector3.Normalize(mouseEndpoint - mouseStartpoint);

        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(-direction * 1000f);
        launchPreview.Clear();
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        mouseEndpoint = worldPosition;

        var direction = mouseEndpoint - mouseStartpoint;
        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        mouseStartpoint = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }
}
