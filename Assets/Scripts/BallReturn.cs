using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher ballLauncher;
    //private bool firstBallReturned = false;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballLauncher.ReturnBall();
        collision.collider.gameObject.SetActive(false);
        //Destroy(collision.collider.gameObject);

        ballLauncher.SetNewStartPosition(collision.collider.transform.position);
        //if(!firstBallReturned)
        //{
        //    firstBallReturned = true;
        //}
    }
}
