using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Trida ktera dela vse Eventy. Konec hry, zacatek hry, Pacman snedl Ghosta. Ghost snedl Pacmana. Pacman Snedl pellet. 
public class GameManager_ : MonoBehaviour
{

    public GameObject gameovertext;
    public GameObject panel;
    public GameObject presskeytext;
    public int level = 0;
    public float levelMul = 0.1f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public List<Image> images = new List<Image>();
    public Ghost_[] ghosts;
    public Pacman_ pacman;
    public Transform pellets;
    public int ghostMult { get; private set; }
    // Start is called before the first frame update
    public int score { get; private set; }
    public int lives { get; private set; }
    void Start()
    {   
        NewGame();
    }
    // Update is called once per frame
    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown){
            NewGame();
        }
    }
    private void NewGame()
    {
        gameovertext.active = false;
        panel.active = false;
        presskeytext.active  = false;
        SetScore(0);
        scoreText.text = "0 points";
        foreach(Image im in images)
        {
            im.enabled = true;
        }
        SetLives(3);
        level += 1;
        levelText.text = "Level: " + level.ToString();
        Invoke(nameof(NewRound), 0.5f);
        //NewRound();
    }
    private void NewRound()
    {

        foreach (Transform pellet in this.pellets)
        { 
            pellet.gameObject.SetActive(true);
        }
        ResetObjects();        

    }
    private void ResetObjects()
    {
        ResetGhostMult();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetGhost();

        }
        this.pacman.ResetPacman();
        
       // this.pacman.transform.position.Set(0,-3.5f,-2);
    }
    private void GameOver()
    {
        level = 0;
        gameovertext.active = true;
        panel.active = true;
        presskeytext.active = true;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);

        }
        this.pacman.gameObject.SetActive(false);

    }
    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int Lives)
    {
        this.lives = Lives;
    }

    public void GhostEaten(Ghost_ ghost)
    {
        int points = ghost.points * ghostMult;
        SetScore(this.score + ghost.points * this.ghostMult);
        this.ghostMult += 1;
    }
    public void PacmanEaten()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);

        }
        pacman.DeathSequence();
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            images[lives].enabled = false;
            Invoke(nameof(ResetObjects), 3.0f);
        }
        else
        {
            images[lives].enabled = false;
            GameOver();
        }
    }
    public void PelletEaten(Pellet_ pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + (pellet.points + (int)(pellet.points*((float)(level-1) * levelMul))));
        scoreText.text = this.score.ToString() + " points";
        if (!HasRemainingPellets())
        {

            this.pacman.gameObject.SetActive(false);
            level += 1;
            levelText.text = "Level: " + level.ToString();
            Invoke(nameof(NewRound), 3.0f);
        }

    }
    public void GiantPelletEaten(GiantPellet_ pellet)
    {
        for(int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Disable();
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMult), pellet.duration);
    }

    public bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);

        }
        return false;
    }
    private void ResetGhostMult()
    {
        this.ghostMult = 1;
    }
   
}
