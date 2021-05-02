using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] List<Enemy_Wave_Config> enemy_wave_config = null;
    [SerializeField] bool looping = false;
    int enemy_wave_config_index = 0;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(Spawn_All_Waves());// "(current_wave)" becoz to tell the enemy which "WAVE" it is a part of
        }
        while (looping);
    }

    private IEnumerator Spawn_All_Enemies_In_Wave(Enemy_Wave_Config wave_config)
    {
        for(int enemy_count = 0; enemy_count < wave_config.Get_Number_Of_Enemies(); enemy_count++)
        {
            var new_enemy = Instantiate(wave_config.Get_Enemy_Prefab(), wave_config.Get_Enemy_Path_Waypoints()[0].transform.position, Quaternion.identity); // Getting the enemy prefab from the wave config script. Calling the "waypoint" method for the tranform position. Quaternion.identity becoz rotation isn't needed.
            new_enemy.GetComponent<Enemy_Path>().Set_Wave_Config(wave_config);
            yield return new WaitForSeconds(wave_config.Get_Time_Between_Spawns());
        }
    }

    private IEnumerator Spawn_All_Waves()
    {
        for(int wave_index = enemy_wave_config_index; wave_index < enemy_wave_config.Count; wave_index++)
        {
            var current_wave = enemy_wave_config[wave_index];
            yield return StartCoroutine(Spawn_All_Enemies_In_Wave(current_wave));
        }
    }
}
