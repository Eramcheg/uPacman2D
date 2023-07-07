using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Trida ktera slouzi k fungovani Portalu mezi strany mapy.


public class Portal_ : MonoBehaviour
{
    public Transform connection;    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        collision.transform.position = position;
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
