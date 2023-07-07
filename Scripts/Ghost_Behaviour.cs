using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost_))]

//Zakladni trida o Obecnem chovani ghostu. Duration kazdeho hovani. Reerence Ghostu. Co delat kdyzh enable, co delat kdyzh disable. 
public abstract class Ghost_Behaviour : MonoBehaviour
{
    public Ghost_ ghost { get; private set; }
    public float duration;
    private void Awake()
    {
        this.ghost = GetComponent<Ghost_>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }
    public virtual void Enable(float duration)
    {

        this.enabled = true;
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
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
