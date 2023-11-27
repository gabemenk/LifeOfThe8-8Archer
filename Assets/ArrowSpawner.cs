using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] public GameObject DBarrow;
    Transform arrowSpawn;
    [SerializeField] int numArrows = 0;
    [SerializeField] float movement = 0;
    ArcherMovement c;
    float ReloadTimer = 0;
    bool JustFired = true;
    [SerializeField] AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponentInParent<ArcherMovement>();
        DBarrow = GameObject.FindGameObjectWithTag("arrow");
        arrowSpawn = this.transform;
        audio = DBarrow.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //if the fire 1 key is pressed we spawn an arrow
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (c != null)
                movement = c.movement;

            if (JustFired)
            {

                //while the timer is greater than 0, decrease it by increments of deltaTime
                if (ReloadTimer > 0)
                {
                    ReloadTimer -= Time.deltaTime;
                }

                //If the timer gets below zero, set it to zero
                if (ReloadTimer < 0)
                {
                    ReloadTimer = 0;
                }

                //Once the timer has officially reached 0
                if (ReloadTimer == 0)
                {
                    numArrows++;
                    JustFired = true;
                    ReloadTimer = 1.5f;
                    ShootArrow();
                }
            }

        }

    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(DBarrow, arrowSpawn.position, arrowSpawn.rotation);
        Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);

        if (arrowRigidbody != null)
        {
            //use if statements to determine what direction the arrow points in
            if(movement > 0)
            {
                Vector2 arrowVelocity = new Vector2(2, 2);

                // Apply the velocity to the arrow
                arrow.transform.Rotate(0, 0, 45);
                arrowRigidbody.velocity = arrowVelocity;
            }else if(movement < 0){
                Vector2 arrowVelocity = new Vector2(-2, 2);

                // Apply the velocity to the arrow
                arrow.transform.Rotate(0, 0, 45);
                arrowRigidbody.velocity = arrowVelocity;
            }
            else
            {
                Vector2 arrowVelocity = new Vector2(0, 3);

                // Apply the velocity to the arrow
                arrow.transform.Rotate(0, 0, 90);
                arrowRigidbody.velocity = arrowVelocity;
            }


            
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the arrow prefab.");
        }
    }
}
