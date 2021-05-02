using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Path : MonoBehaviour
{
    Enemy_Wave_Config wave_config = null;
    //[SerializeField] float enemy_speed = 2f;
        
    List<Transform> way_points = null;
    int way_point_index = 0; // index of the waypoint in the list

    private void Start()
    {
        way_points = wave_config.Get_Enemy_Path_Waypoints();
        transform.position = way_points[way_point_index].transform.position;
    }

    private void Update()
    {
        Enemy_Move();
    }

    private void Enemy_Move()
    {
        if(way_point_index <= way_points.Count-1)
        {
            var target_position = way_points[way_point_index].transform.position;
            var enemy_pace = wave_config.Get_Enemy_Speed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target_position, enemy_pace);

            if(transform.position == target_position)
            {
                way_point_index++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Set_Wave_Config(Enemy_Wave_Config set_wave_config)
    {
        this.wave_config = set_wave_config;
    }
}
