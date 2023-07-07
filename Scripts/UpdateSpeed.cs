using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



//Tato trida slouzi k tomu, aby menit speed, kteru chce uzivatel

public class UpdateSpeed : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI text;
    public GameManager_ gm;
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (Math.Round(slider.value,1)).ToString();
        float speed = (float)(Math.Round(slider.value, 1));
        for (int i = 0; i < gm.ghosts.Length; i++)
        {
            gm.ghosts[i].movement.speed = speed;
        }
    }
}
