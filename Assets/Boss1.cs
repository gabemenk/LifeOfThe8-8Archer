using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    //serialize the fields so they can be seen in unity editor
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 5;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] int bossHealth = 10;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource audio2;
    [SerializeField] Vector3 scaleChange;
    [SerializeField] GameObject controller;
    [SerializeField] int level;
    [SerializeField] bool deathSoundPlayed = false;

    // Start is called before the first frame update
    //make sure the boss has a rigid body
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        //audio = this.GetComponent<AudioSource>();
        scaleChange = new Vector3(-0.001f, -0.001f, -0.001f);
        InvokeRepeating("GrowShrink", 0.5f, 0.1f/level);
        if (controller == null)
            controller = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth == 2 && level == 3 && !deathSoundPlayed)
        {
            AudioSource.PlayClipAtPoint(audio2.clip, transform.position);
            deathSoundPlayed = true;
        }
        if (bossHealth == 0)
        {
            controller.GetComponent<ScoreTimer>().NextLevel();
        }
    }

    //we will be using FixedUpdate for the boss's movement so that it isn't tied to frame rate
    private void FixedUpdate()
    {
        //make the boss move across the screen
        if(isFacingRight)
            rigid.velocity = new Vector2(5*level, 0);
        else
            rigid.velocity = new Vector2(-5*level, 0);
    }

    //method to flip the sprite based on where it is facing
    private void Flip()
    {
        //flip the sprite and record that in the boolean
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    //method to flip the boss whenever it hits a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            Flip();
    }

    //method for boss to interract with arrows
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "arrow")
            bossHealth--;
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);
    }

    //method to grow/shrink boss
    void GrowShrink()
    {
        // when the sphere scale extends 1.0f.
        if (this.transform.localScale.y < 0.1f || this.transform.localScale.y > 1.0f)
            scaleChange = -scaleChange;
        this.transform.localScale += scaleChange;
    }
}
