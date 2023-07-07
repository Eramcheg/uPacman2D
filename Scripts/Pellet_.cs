using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Trida slouzi k tomu, aby fungovali pellety ktere Pacman ji.

public class Pellet_ : MonoBehaviour
{
    public int points = 10;
    protected virtual void Eat()
    {
        //this.gameObject.SetActive(false);
        FindObjectOfType<GameManager_>().PelletEaten(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
             Eat();
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
