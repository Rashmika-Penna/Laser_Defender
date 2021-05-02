using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50f;
    [SerializeField] float laser_shot_counter;
    [SerializeField] float min_time_between_laser_shots = 0.2f;
    [SerializeField] float max_time_between_laser_shots = 3f;
    [SerializeField] float enemy_projectile_speed = 10f;
    //[SerializeField] float explosion_destroy_time = 1f;
    [SerializeField] GameObject enemy_laser_prefab = null;
    [SerializeField] GameObject explosion_particle_effect = null;
    [SerializeField] AudioClip death_explosion = null;
    [SerializeField] [Range(0,1)] float death_explosion_volume = 0.5f;

    private void Start()
    {
        // Starting the timer as soon as the game starts
        laser_shot_counter = Random.Range(min_time_between_laser_shots, max_time_between_laser_shots);
    }

    private void Update()
    {
        // Shoot and starting the shotcounter again and then shooting and restarting the shot counter 
        Count_Down_And_Shoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage_Dealer damage_dealer = other.gameObject.GetComponent<Damage_Dealer>();
        
        if(!damage_dealer)
        {
            return;
        }

        Process_The_Death(damage_dealer);
    }

    private void Process_The_Death(Damage_Dealer damage_dealer)
    {
        health -= damage_dealer.Get_Damage();
        damage_dealer.On_Hit();

        if (health <= 0)
        {
            Death();
        }
    }

    private void Count_Down_And_Shoot()
    {
        // Since it is a count-down, the value of the shot counter is being reduced.
        laser_shot_counter -= Time.deltaTime;

        if(laser_shot_counter <= 0f)
        {
            Fire();
            laser_shot_counter = Random.Range(min_time_between_laser_shots, max_time_between_laser_shots);
        }
    }

    private void Fire()
    {
        GameObject enemy_laser = Instantiate(enemy_laser_prefab, transform.position, Quaternion.identity) as GameObject;
        enemy_laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemy_projectile_speed);
    }   

    private void Death()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosion_particle_effect, transform.position, transform.rotation);
        //Destroy(explosion_particle_effect, explosion_destroy_time);
        AudioSource.PlayClipAtPoint(death_explosion, Camera.main.transform.position, death_explosion_volume);
    }
}
