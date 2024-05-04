using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int currentHP;
    public int attack;
    public int baseAttack;
    public int defense;
    public int baseDefense;
    public new string name;
    public int maxHP;
    public int enemyTurnCount;
    public int ID;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            currentHP += 0;
        }
        else if (currentHP - damage < 0)
        {
            currentHP = 0;
        }
        else
        {
            currentHP -= damage;
        }
    }


    public void SetPlayerHP(int newCurrentHP, int newMaxHP)
    {
        currentHP = newCurrentHP;
        maxHP = newMaxHP;
    }
    
    public void SetEasyDifficulty()
    {
       SetPlayerHP(2000,2000);
    }

    public void SetMediumDifficulty()
    {
        SetPlayerHP(1500,1500);
    }

    public void SetHardDifficulty() 
    {
        SetPlayerHP(1000,1000);
    }

    public void SetInsaneDifficulty() 
    {
        SetPlayerHP(500,500);
    }
    
}

