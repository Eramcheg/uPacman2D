using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Chovani ghostu doma. 
public class Ghost_Home : Ghost_Behaviour
{
    public Transform vchod;
    public Transform vychod;


    private void OnEnable()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
         
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction); 
        }
    }


    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 pos = this.transform.position;
        float dur = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < dur)
        {
            Vector3 newPos = Vector3.Lerp(pos,this.vchod.position, elapsed/dur);
            newPos.z = pos.z; 
            this.ghost.transform.position = newPos;
            elapsed += Time.deltaTime; //Kazdy frame coroutin se hra

            yield return null; 
        }

        elapsed= 0.0f;

        while (elapsed < dur)
        {
            Vector3 newPos = Vector3.Lerp(this.vchod.position, this.vychod.position, elapsed / dur);
            newPos.z = pos.z;
            this.ghost.transform.position = newPos;
            elapsed += Time.deltaTime; //Kazdy frame coroutin se hra

            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f:1.0f, 0.0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
