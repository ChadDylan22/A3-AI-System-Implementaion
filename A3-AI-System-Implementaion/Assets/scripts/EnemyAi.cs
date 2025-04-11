using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //Properties / Variables

    [SerializeField]
    Transform castPoint;


    [SerializeField]
    Transform player;

    [SerializeField]
   float chaseRange;

   [SerializeField]
   float moveSpeed;

    new Rigidbody2D rigidbody;

    bool isFacingLeft;

    bool isAgro = false;

     bool isSearching;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (CanSeePlayer(chaseRange)) 
        {
            //Calls function to chase player
            isAgro = true;
            ChasePlayer();
        }
        else
        {
            //Calls function to stop chasing player on a delay
            if (isAgro)
            {
                if (!isSearching) 
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 2);
                }
            }
        }

        if (isAgro)
        {
            ChasePlayer();
        }

       /* old script above is the improved version
        //Distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer < chaseRange)
        {
            //Code to chase the player
            ChasePlayer();
        }
        else 
        {
            //Code to stop chasing the player
             StopChasingPlayer();
        }
       */ 
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isFacingLeft) 
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else 
            {
                val = false;
            }
             //Debug.DrawLine(castPoint.position, endPos, Color.yellow);
        }
        else 
        {
             //Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val;

    }


    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x) 
        {
            //Enemy is to the left side of the player, so move right
            rigidbody.velocity = new Vector2 (moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }
        else 
        {
            //Enemy is to the right side of the player, so move left
            rigidbody.velocity = new Vector2 (-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    private void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rigidbody.velocity = new Vector2 (0, 0);
    }
}

