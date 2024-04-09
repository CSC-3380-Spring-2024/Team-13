using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHP;
    public int attack;
    public int baseAttack;
    public int defense;
    public int baseDefense;
    public int maxHP;
    public int enemyTurnCount;

    public Text hpText; // Reference to the UI Text element

    private void Start()
    {
        UpdateHPText();
    }

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

        UpdateHPText(); // Update the HP text whenever the player takes damage
    }

    private void UpdateHPText()
    {
        hpText.text = "HP: " + currentHP; // Update the text element with the current HP
    }
}
