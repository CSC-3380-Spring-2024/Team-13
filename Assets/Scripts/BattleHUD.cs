using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentHp;
    public TextMeshProUGUI maxHp;
    public TextMeshProUGUI currentATK;
    public TextMeshProUGUI currentDEF;
    public Image sword;
    public Image shield;
    public Image x;
    public Image grayout;

    Color green = new Color(.235f,.549f,.137f);
    Color red = new Color(.6666f,.0784f,.0784f);

    public Image healthBar;

    public void SetHUD(PlayerScript combatant,float lerp)
    {
        nameText.text = combatant.name;
        currentHp.text = combatant.currentHP.ToString();
        maxHp.text = combatant.maxHP.ToString();
        
        //Sets the healthbar to full
        healthBar.fillAmount = 0;
        healthBar.fillAmount = Mathf.Lerp(0,1,lerp);

        x.enabled=false;
        grayout.enabled=false;
    }

    public void SetHP(PlayerScript combatant,float lerp, int num)
    {
        //Sets the CHP text for each player/enemy
        currentHp.text = combatant.currentHP.ToString();

        //Controls the healthbar decay
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,(float)combatant.currentHP/combatant.maxHP,lerp);
        if (combatant.currentHP==0){
            if (num==1){
                StartCoroutine(EnemyDefeat());
            }
            else{
            StartCoroutine(ClearPlayerHUD());
            }
        }
        if (combatant.currentHP==0){return;}
        //Updates attack value (buff or debuff)
            {
                if (combatant.attack<combatant.baseAttack){
                    currentATK.text="-"+(int)(((double)combatant.baseAttack/combatant.attack-1.0)*100.0)+"%";
                    currentATK.color=red;
                    currentATK.enabled=true;
                    sword.enabled=true;
                }
                else if (combatant.attack>combatant.baseAttack){
                    currentATK.text="+"+(int)(((double)combatant.attack/combatant.baseAttack-1.0)*100.0)+"%";
                    currentATK.color=green;
                    currentATK.enabled=true;
                    sword.enabled=true;
                }
                else{
                    currentATK.enabled=false;
                    sword.enabled=false;
                }
            }
            //Updates defense value (buff or debuff)
            {
                if (combatant.defense<combatant.baseDefense){
                    currentDEF.text="-"+(int)(((double)combatant.baseDefense/combatant.defense-1.0)*100.0)+"%";
                    currentDEF.color=red;
                    currentDEF.enabled=true;
                    shield.enabled=true;
                }
                else if (combatant.defense>combatant.baseDefense){
                    currentDEF.text="+"+(int)(((double)combatant.defense/combatant.baseDefense-1.0)*100.0)+"%";
                    currentDEF.color=green;
                    currentDEF.enabled=true;
                    shield.enabled=true;
                }
                else{
                    currentDEF.enabled=false;
                    shield.enabled=false;
                }
            }
    }

     IEnumerator ClearPlayerHUD(){
        yield return new WaitForSeconds(.2f);
        
        shield.enabled=false;
        sword.enabled=false;
        currentDEF.enabled=false;
        currentATK.enabled=false;
        x.enabled=true;
        grayout.enabled=true;
    }

    public IEnumerator EnemyDefeat(){
        yield return new WaitForSeconds(.2f);
        shield.enabled=false;
        sword.enabled=false;
        currentDEF.enabled=false;
        currentATK.enabled=false;
        grayout.enabled=true;
    }
}
