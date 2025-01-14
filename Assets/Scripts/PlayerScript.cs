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


    public void SetEasyDifficulty()
{
    PlayerPrefs.SetInt("difficulty", 1); //1 for Easy difficulty
}

public void SetMediumDifficulty()
{
    PlayerPrefs.SetInt("difficulty", 2); //2 for Medium difficulty
}

public void SetHardDifficulty()
{
    PlayerPrefs.SetInt("difficulty", 3); //3 for Hard difficulty
}

public void SetInsaneDifficulty()
{
    PlayerPrefs.SetInt("difficulty", 4); //4 for Insane difficulty
}
}

