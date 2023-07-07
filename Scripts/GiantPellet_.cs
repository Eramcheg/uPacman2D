using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trida Velty Pellet. Dava muznost pacmanu jist Ghostu.
public class GiantPellet_ : Pellet_
{
    public float duration = 8f;

    protected override void Eat()
    {
        //this.gameObject.SetActive(false);
        FindObjectOfType<GameManager_>().GiantPelletEaten(this);
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
