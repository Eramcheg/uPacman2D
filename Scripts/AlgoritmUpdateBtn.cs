using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Tato trida slouzi k tomu, aby menit algoritmus ghostu, ktery chce uzivatel v settingu.
public class AlgoritmUpdateBtn : MonoBehaviour
{

    public TMP_Dropdown dropdown;
    private List<string> algorithms = new List<string>();
    public GameManager_ gm;
    // Start is called before the first frame update
    void Start()
    {
        algorithms.Add("Simple");
        algorithms.Add("Simple+");
        algorithms.Add("BFS");

    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<gm.ghosts.Length;i++)
        {
            gm.ghosts[i].chase.algorithm = algorithms[dropdown.value];
        }
    }
}
