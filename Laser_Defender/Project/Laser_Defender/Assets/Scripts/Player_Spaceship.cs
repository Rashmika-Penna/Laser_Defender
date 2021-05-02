using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spaceship : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float move_speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;
    [SerializeField] AudioClip death_explosion = null;
    [SerializeField] [Range(0, 1)] float death_explosion_volume = 0.5f;


    [Header("Laser")]
    [SerializeField] float projectile_speed = 10f;
    [SerializeField] float laser_firing_period = 0.5f;
    [SerializeField] GameObject laser_prefab = null;
        
    Coroutine laser_firing_coroutine;

    float min_x, max_x;
    float min_y, max_y;

    private void Start()
    {
        Set_Game_Boundaries();
    }

    private void Update()
    {
        Move_Spaceship();
        Spaceship_shoot();
    }

    private void Move_Spaceship()
    {
        // Moving along the x-axis
        var delta_x = Input.GetAxis("Horizontal") * Time.deltaTime * move_speed; // delta_x is of type "float" becoz Input.GetAxis() returns a float value.  
        var new_x_position = Mathf.Clamp(transform.position.x + delta_x, min_x, max_x); // Therefore, it is like getting the spaceship's current position and adding delta_x to move the distance. Eg: (0,0) + (5,0) [delta_x].
        //transform.position = new Vector2(new_x_position, transform.position.y); // Since the movement is only along the x-axis, the "y" coordinate is "transform.position.y". It's like asking it to stay whereever it is, on the y-axis

        // Moving along the y-axis
        var delta_y = Input.GetAxis("Vertical") * Time.deltaTime * move_speed;
        var new_y_position = Mathf.Clamp(transform.position.y + delta_y, min_y, max_y);
        transform.position = new Vector2(new_x_position, new_y_position);
    }

    private void Set_Game_Boundaries()
    {
        Camera game_camera = Camera.main;

        // x-axis Boundaries 
        min_x = game_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        max_x = game_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //y-axis Boundaries
        min_y = game_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        max_y = game_camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    //private void Spaceship_shoot()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        GameObject laser = Instantiate(laser_prefab, transform.position, Quaternion.identity) as GameObject; // 1. Quaternion.identity = no rotation  2. "as GameObject" becoz, it was just an object. By converting it into a Gameobject, it lets us work more with it.
    //        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectile_speed); // Giving velocity to the laser 
    //    }
    //}

    // Firing continuously
    private void Spaceship_shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            laser_firing_coroutine = StartCoroutine(Spaceship_Shoot_Continuous());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(laser_firing_coroutine);
        }
    }

    IEnumerator Spaceship_Shoot_Continuous()
    {
        while(true)
        {
            GameObject laser = Instantiate(laser_prefab, transform.position, Quaternion.identity) as GameObject; // 1. Quaternion.identity = no rotation  2. "as GameObject" becoz, it was just an object. By converting it into a Gameobject, it lets us work more with it.
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectile_speed); // Giving velocity to the laser 
            yield return new WaitForSeconds(laser_firing_period);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage_Dealer damage_dealer = other.gameObject.GetComponent<Damage_Dealer>();

        if(!damage_dealer)
        {
            return;
        }

        Process_Hit(damage_dealer);
    }

    private void Process_Hit(Damage_Dealer damage_dealer)
    {
        health -= damage_dealer.Get_Damage();
        damage_dealer.On_Hit();

        if(health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(death_explosion, Camera.main.transform.position, death_explosion_volume);
        }
    }
}
