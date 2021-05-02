using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy_Wave_Configuration")]

public class Enemy_Wave_Config : ScriptableObject
{
    [SerializeField] GameObject enemy_prefab = null;
    [SerializeField] GameObject enemy_path_prefab = null;
    [SerializeField] float time_between_spawns = 0.5f;
    [SerializeField] float spawn_random_factor = 0.3f; // To create a randomness in spawning the enemies so that the game isn't very predictable
    [SerializeField] int number_of_enemies = 5;
    [SerializeField] float enemy_speed = 2f;

    // "Public" methods which "return" the variable declared.
    // So that the other scripts can access them.

    public GameObject Get_Enemy_Prefab()
    {
        return enemy_prefab;
    }

    public List<Transform> Get_Enemy_Path_Waypoints()
    {
        var wave_way_points = new List<Transform>();
        
        foreach(Transform child in enemy_path_prefab.transform)
        {
            wave_way_points.Add(child);
        }

        return wave_way_points;
    }

    public float Get_Time_Between_Spawns()
    {
        return time_between_spawns;
    }

    public float Get_Spawn_Random_Factor()
    {
        return spawn_random_factor;
    }

    public float Get_Enemy_Speed()
    {
        return enemy_speed;
    }

    public int Get_Number_Of_Enemies()
    {
        return number_of_enemies;
    }

}
