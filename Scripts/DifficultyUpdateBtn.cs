using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Tato trida slouzi k tomu, aby menit slozitost ghostu, kteru chce uzivatel. Meni jenom cas chovani kazdeho hostu, aby hra byla zajivavejsi.
public class DifficultyUpdateBtn : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameManager_ gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gm.ghosts.Length; i++)
        {
            if (dropdown.itemText.text == "Easy")
            {
                gm.ghosts[i].frightened.duration = 10;
                gm.ghosts[i].scatter.duration = 9;
                if (i == 0) 
                gm.ghosts[i].chase.duration = 30;
                else
                    gm.ghosts[i].chase.duration = 20;
            }
            else if(dropdown.itemText.text == "Medium")
            {
                gm.ghosts[i].frightened.duration = 8;
                gm.ghosts[i].scatter.duration = 7;
                if (i == 0)
                    gm.ghosts[i].chase.duration = 35;
                else
                    gm.ghosts[i].chase.duration = 30;

            }
            else
            {

                gm.ghosts[i].frightened.duration = 6;

                if (i == 0)
                {
                    gm.ghosts[i].chase.duration = 100;
                    gm.ghosts[i].scatter.duration = 2;
                }
                else
                {
                    gm.ghosts[i].chase.duration = 40;
                    gm.ghosts[i].scatter.duration = 5;
                }

            }
        }
    }
}
