using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReindeerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float movement;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] GameObject controller;
    [SerializeField] Vector3 scaleChange;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex -1 ;
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (controller == null)
            controller = GameObject.FindGameObjectWithTag("GameController");
        scaleChange = new Vector3(level, level, level);
        this.transform.localScale += scaleChange;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void FixedUpdate()
    {
        //make the sprite move across the screen
        if (isFacingRight)
            rigid.velocity = new Vector2(5, 0);
        else
            rigid.velocity = new Vector2(-5, 0);
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

    //method for sprite to interract with arrows
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "arrow")
            controller.GetComponent<ScoreTimer>().ReindeerHit();
    }
}
