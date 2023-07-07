using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//Trida slouzi k tomu aby kazdy ghost znal kde muze menit direction. Slouzi jako pomocni trida pro nektere AI pro hledajici funkci.


public class Node_ : MonoBehaviour
{
    public LayerMask obstacle;
    public List<Vector2> availableDir { get; private set; }
    //public Node_[] neighbours;
    public Node_ Parent { get; set; }
    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost { get { return GCost + HCost; } }
    void Start()
    {
        this.availableDir = new List<Vector2>();
        CheckAvailableDir(Vector2.up);
        CheckAvailableDir(Vector2.down);
        CheckAvailableDir(Vector2.left);
        CheckAvailableDir(Vector2.right);
    }

    private void CheckAvailableDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, dir, 1f, obstacle);

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null)
        {
            availableDir.Add(dir);
            
            //node.direction = direction;
           

        }
    }
    
}
