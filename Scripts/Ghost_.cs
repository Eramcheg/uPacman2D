using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zakladni informace o ghostu. Pointy, vse chovani ghostu, pozice.
public class Ghost_ : MonoBehaviour
{   

    public int points = 200;
    public Movement_ movement { get; private set; }
    public Ghost_Frightened frightened { get; private set; } //Kdyz ghost se boji 
    public Ghost_Home home { get; private set; }          //Kdyz ghost doma
    public Ghost_Chase chase { get; private set; }        //Kdyz ghost jde za pacmanem
    public Ghost_Scatter scatter { get; private set; }    //kdyz ghost jde nekdy aby premyslet nad pozici pacmanu
    public Ghost_Behaviour initial_behaviour;             //Obecne chovani ghostu 
    public Transform target;                              //Pacman 

    private void Awake()

    {
        this.movement = GetComponent<Movement_>();
        this.home = GetComponent<Ghost_Home>();
        this.scatter= GetComponent<Ghost_Scatter>();
        this.frightened= GetComponent<Ghost_Frightened>();
        this.chase= GetComponent<Ghost_Chase>();

    }
    private void Start()
    {
        ResetGhost();   
    }

    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    public void ResetGhost()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if (this.home != this.initial_behaviour)
        {
            this.home.Disable();
        }
        if(this.initial_behaviour!= null) { initial_behaviour.Enable(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)    //Jestli ghost se boji - pacman bude jist ghosta
            {
                FindObjectOfType<GameManager_>().GhostEaten(this);
            }
            else   //Jestli ghost se neboji, ghost bude jist pacmana
            {
                FindObjectOfType<GameManager_>().PacmanEaten();
            }
        }
    }
}
