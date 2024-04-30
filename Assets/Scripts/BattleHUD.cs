using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentHp;
    public TextMeshProUGUI maxHp;

    public Image healthBar;

    public void SetHUD(PlayerScript combatant,float lerp)
    {
        nameText.text = combatant.name;
        currentHp.text = combatant.currentHP.ToString();
        maxHp.text = combatant.maxHP.ToString();
        
        //Sets the healthbar to full
        healthBar.fillAmount = 0;
        healthBar.fillAmount = Mathf.Lerp(0,1,lerp);
    }

    public void SetHP(PlayerScript entity,float lerp)
    {
        //Sets the CHP text for each player/enemy
        currentHp.text = entity.currentHP.ToString();

        //Controls the healthbar decay
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,(float)entity.currentHP/entity.maxHP,lerp);
    }
}
