using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trida o chovani, kdyzh pacman snedl Velky pellet a ted' muze jist ghosty. Kazdy ghost se boji pacmana a jde od pacmana conejdal.
public class Ghost_Frightened : Ghost_Behaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    public override void Enable(float dur)
    {
        base.Enable(dur);
        this.body.enabled = false; 
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;
        Invoke(nameof(Flash),duration/2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite_>().Restart();
        }
    }
    private void Eaten()
    {
        eaten = true;
        this.ghost.SetPosition(ghost.home.vchod.position);
        ghost.home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
               Eaten();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("FJI@J");

        Node_ node = collision.GetComponent<Node_>();
        // Debug.Log(node);
        if (node != null && this.enabled )
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDir in node.availableDir)
            {
                Vector3 newPos = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
                float distance = (this.ghost.target.position - newPos).sqrMagnitude;//
                if (distance > maxDistance)
                {
                    direction = availableDir;
                    maxDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
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
