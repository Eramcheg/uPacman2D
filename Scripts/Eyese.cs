using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Trida jaka meni direction oci kazdeho ghostu.

public class Eyese : MonoBehaviour
{
    public SpriteRenderer spriteRender { get; private set; }
    public Movement_ movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    // Start is called before the first frame update
    private void Awake()
    {
        this.spriteRender= GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement_>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (this.movement.direction == Vector2.up)
        {
            this.spriteRender.sprite = this.up;
        }
        else if (this.movement.direction == Vector2.down)
        {
            this.spriteRender.sprite = this.down;
        }
        else if (this.movement.direction == Vector2.left)
        {
            this.spriteRender.sprite = this.left;
        }
        else if (this.movement.direction == Vector2.right)
        {
            this.spriteRender.sprite = this.right;
        }

    }
}
