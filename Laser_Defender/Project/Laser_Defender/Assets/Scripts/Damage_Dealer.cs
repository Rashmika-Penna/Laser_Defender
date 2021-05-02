using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Dealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    
    public int Get_Damage()
    {
        return damage;
    }

    public void On_Hit()
    {        
        Destroy(gameObject); 
    }
}
