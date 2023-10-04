using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    private int health = 100;
    private int ammo = 3;

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > 100)
        {
            health = 100;
        }
    }

    public void AddAmmo(int ammo)
    {
        this.ammo += ammo;
    }
}
