using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 mouseStartpoint, mouseEndpoint;
    private LaunchPreview launchPreview;
    private List<Ball> balls = new List<Ball>();
    private int ballsReady;

    [SerializeField]
    private Ball ballPrefab;
    private BlockSpawner blockSpawner;

    internal void SetNewStartPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void Awake()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        launchPreview = GetComponent<LaunchPreview>();
    }

    public void ReturnBall()
    {
        if(++ballsReady == balls.Count)
        {
            blockSpawner.SpawnNewRow();
        }
    }

    private void Update()
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

    private void StartDrag(Vector3 worldPosition)
    {
        mouseStartpoint = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        mouseEndpoint = worldPosition;

        var direction = mouseEndpoint - mouseStartpoint;
        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void EndDrag()
    {
        CreateBall();
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        var direction = Vector3.Normalize(mouseEndpoint - mouseStartpoint);
        launchPreview.Clear();

        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction * 100f);

            yield return new WaitForSeconds(0.1f);
        }
        ballsReady = 0;
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        balls.Add(ball);

        ballsReady++;
    }
}
