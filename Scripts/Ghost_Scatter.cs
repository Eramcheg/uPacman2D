using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




//Taktika ghostu kdyz on bezi nekdy, dela pauzu mezi utoky na pacmana. 

public class Ghost_Scatter : Ghost_Behaviour
{

    // EASY LOGIC OF SCATTER

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("FJI@J");

            Node_ node = collision.GetComponent<Node_>();
       // Debug.Log(node);
            if (node != null && this.enabled && !this.ghost.frightened.enabled)
            {
                //Debug.Log("FJI@J2");
                int index = Random.Range(0, node.availableDir.Count);
                //Debug.Log(index);
                if (node.availableDir[index] == -this.ghost.movement.direction && node.availableDir.Count > 1)
                {
                    index = (index + 1) % node.availableDir.Count;

                }
            //Debug.Log(index);
            this.ghost.movement.SetDirection(node.availableDir[index]);
            //Debug.Log("Exit");
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
