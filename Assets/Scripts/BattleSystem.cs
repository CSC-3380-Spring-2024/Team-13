using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Random = UnityEngine.Random;
using JetBrains.Annotations;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, BETWEENMOVES};
public enum PartyTurn { MCTURN, SECONDTURN, THIRDTURN, NONE};

public class BattleSystem : MonoBehaviour
{
    PlayerScript playerUnit1;
    PlayerScript playerUnit2;
    PlayerScript playerUnit3;
    PlayerScript enemyUnit1;
    PlayerScript enemyUnit2;
    PlayerScript enemyUnit3;

    //refrences the text on the UI buttons
    public Text DialogueText;
    public TextMeshProUGUI SkillText;
    public TextMeshProUGUI SupportText;
    public TextMeshProUGUI SpecialText;
    public TextMeshProUGUI ActionText;

    public TextMeshProUGUI AttackButtonAltText;
    public TextMeshProUGUI SkillButtonAltText;
    public TextMeshProUGUI SupportButtonAltText;
    public TextMeshProUGUI SpecialButtonAltText;
    public TextMeshProUGUI ActionButtonAltText;

    //refrences the HUDSs
    public BattleHUD playerHUD1;
    public BattleHUD playerHUD2;
    public BattleHUD playerHUD3;
    public BattleHUD enemyHUD1;

    //refrences the Background image;
    public Image background;
    public int battleCount;

    public BattleState state;
    public PartyTurn turn;

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;

    public Image enemyImage;
    public Sprite upset1;
    public Sprite upset2;
    public Sprite upset3;
    public Sprite upset4;
    public Sprite laughing1;
    public Sprite laughing2;
    public Sprite laughing3;
    public Sprite laughing4;

    public Transform playerBattleStation;
    public Transform enemyBattleStation1;
    public Transform enemyBattleStation2;
    public Transform enemyBattleStation3;

    //Used for healthbar decay
    float lerpSpeed;

    void Start()
    {
        battleCount = 1; 
        state = BattleState.START;
        StartCoroutine(setupBattle(battleCount)); 
    }

    void Update()
    {
      UpdatePlayerHealth();
      lerpSpeed = 3f * Time.deltaTime;
    }


    IEnumerator setupBattle(int battleNum)
    {
        GameObject playerGO1 = Instantiate(playerPrefab1, playerBattleStation);
        playerUnit1 = playerGO1.GetComponent<PlayerScript>();
        GameObject playerGO2 = Instantiate(playerPrefab2, playerBattleStation);
        playerUnit2 = playerGO2.GetComponent<PlayerScript>();
        GameObject playerGO3 = Instantiate(playerPrefab3, playerBattleStation);
        playerUnit3 = playerGO3.GetComponent<PlayerScript>();

        switch (battleNum){
        case 1: 
            {
                GameObject enemyGO = Instantiate(enemyPrefab1, enemyBattleStation1);
                enemyUnit1 = enemyGO.GetComponent<PlayerScript>();
                enemyImage.sprite = upset1;
                break;
            }
        case 2: 
            {
                GameObject enemyGO = Instantiate(enemyPrefab2, enemyBattleStation1);
                enemyUnit1 = enemyGO.GetComponent<PlayerScript>();
                enemyImage.sprite = upset2;
                break;
            }
        case 3: 
            {
                GameObject enemyGO = Instantiate(enemyPrefab2, enemyBattleStation1);
                enemyUnit1 = enemyGO.GetComponent<PlayerScript>();
                enemyImage.sprite = upset3;
                break;
            }       
        case 4: 
            {
                GameObject enemyGO = Instantiate(enemyPrefab4, enemyBattleStation1);
                enemyUnit1 = enemyGO.GetComponent<PlayerScript>();
                enemyImage.sprite = upset4;
                break;
            }
        default:
            {
                GameObject enemyGO = Instantiate(enemyPrefab1, enemyBattleStation1);
                enemyUnit1 = enemyGO.GetComponent<PlayerScript>();
                enemyImage.sprite = upset1;
                break;
            }
        }

        DialogueText.text = "You were attacked by " + enemyUnit1.name + "!";
        playerHUD1.SetHUD(playerUnit1,lerpSpeed-2);
        playerHUD2.SetHUD(playerUnit2,lerpSpeed-2);
        playerHUD3.SetHUD(playerUnit3,lerpSpeed-2);
        enemyHUD1.SetHUD(enemyUnit1,lerpSpeed-2);
        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        turn = PartyTurn.NONE;
        StartCoroutine(PlayerTurn());
    }
    
    IEnumerator PlayerTurn()
    {
        DialogueText.text = "It's your turn!";
        yield return new WaitForSeconds(3f);
        turn = PartyTurn.NONE;

        //determines which party member will act first, they get no turn if hp < 0
        if (playerUnit1.currentHP > 0)
        {
            turn = PartyTurn.MCTURN;
            UpdateButtonText();
        }
        else if (playerUnit2.currentHP > 0)
        {
            DialogueText.text = "Quincy is unconscious!";
            yield return new WaitForSeconds(4f);
            turn = PartyTurn.SECONDTURN;
            UpdateButtonText();
        }
        else if (playerUnit3.currentHP > 0)
        {
            DialogueText.text = "Qwynn is unconscious!";
            yield return new WaitForSeconds(4f);
            turn = PartyTurn.THIRDTURN;
            UpdateButtonText();
        }
        else 
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
    }

    IEnumerator EnemyTurn(int turnsLeft)
    {
        DialogueText.text = "It's the enemy's turn!";
        yield return new WaitForSeconds(2f);
        var random = new System.Random();
        int rand = random.Next(0, 5);

        //Test 
        //rand=3;
        
        int numTurns=turnsLeft;

        //If enemy is debuffed
        if (enemyUnit1.attack < enemyUnit1.baseAttack || enemyUnit1.defense < enemyUnit1.baseDefense)
        {
            switch (rand){
                case 0:
                {
                   //removes debuffs from enemy and heals enemy
                    enemyUnit1.currentHP += 200;
                    RemoveDebuffs(enemyUnit1);
                    DialogueText.text = "The enemy removed all debuffs on themself and healed!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 1:
                {
                    //attacks all
                    playerUnit1.TakeDamage(enemyUnit1.attack - playerUnit1.defense);
                    playerUnit2.TakeDamage(enemyUnit1.attack - playerUnit2.defense);
                    playerUnit3.TakeDamage(enemyUnit1.attack - playerUnit3.defense);
                    DialogueText.text = "The enemy attacked everyone!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 2:
                {
                    //heals one player
                    PlayerScript target = enemyTarget();
                    target.currentHP +=150;
                    DialogueText.text = "The enemy healed a player for " + "150" + "HP";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 3:
                {
                    //attacks one player
                    PlayerScript target = enemyTarget();
                    target.TakeDamage(enemyUnit1.attack * 2 - target.defense);
                    DialogueText.text = "The enemy dealt " + (enemyUnit1.attack * 2 - target.defense) + " damage to" + target.name;
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 4:
                {
                    //removes enemy debuffs and buffs enemy
                    RemoveDebuffs(enemyUnit1);
                    IncreaseAttack(enemyUnit1, 0.4);
                    IncreaseDefense(enemyUnit1, 0.4);
                    DialogueText.text = "The enemy removed all debuffs and increased their attack and defense!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 5:
                {
                    //if any player is buffed
                    if (((playerUnit1.attack > playerUnit1.baseAttack) || (playerUnit2.attack > playerUnit2.baseAttack) || (playerUnit3.attack > playerUnit3.baseAttack) || (playerUnit1.defense > playerUnit1.baseDefense) || (playerUnit2.defense > playerUnit2.baseDefense) || (playerUnit3.defense > playerUnit3.baseDefense)) && (enemyUnit1.attack < enemyUnit1.baseAttack || enemyUnit1.defense < enemyUnit1.baseDefense))
                    { 
                        //removes buffs from all players
                        RemoveBuffs(playerUnit1);
                        RemoveBuffs(playerUnit2);
                        RemoveBuffs(playerUnit3);
                        DialogueText.text = "The enemy removed all of your buffs!";
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(CheckPartyHP(numTurns));
                        break;
                    }
                    else
                    {
                        //brutally attacks one player
                        PlayerScript target = enemyTarget();
                        target.TakeDamage(enemyUnit1.attack*4 - target.defense);
                        DialogueText.text = "The enemy dealt a whopping" + (enemyUnit1.attack - target.defense) + " damage";
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(CheckPartyHP(numTurns));
                        break;
                    }
                }
            }
        }
        
        //If any player is buffed
        else if ((playerUnit1.attack > playerUnit1.baseAttack) || (playerUnit2.attack > playerUnit2.baseAttack) || (playerUnit3.attack > playerUnit3.baseAttack) || (playerUnit1.defense > playerUnit1.baseDefense) || (playerUnit2.defense > playerUnit2.baseDefense) || (playerUnit3.defense > playerUnit3.baseDefense))
        {
            switch (rand)
            {
                case 0: case 3:
                {
                    //removes buffs from all players
                    RemoveBuffs(playerUnit1);
                    RemoveBuffs(playerUnit2);
                    RemoveBuffs(playerUnit3);
                    DialogueText.text = "The enemy removed all of your buffs!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 1:
                {
                    //attacks all
                    playerUnit1.TakeDamage(enemyUnit1.attack - playerUnit1.defense);
                    playerUnit2.TakeDamage(enemyUnit1.attack - playerUnit2.defense);
                    playerUnit3.TakeDamage(enemyUnit1.attack - playerUnit3.defense);
                    DialogueText.text = "The enemy attacked everyone!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 2:
                {
                    //debuffs all players
                    enemyUnit1.currentHP += 200;
                    ReduceAttack(playerUnit1, 0.2);
                    ReduceAttack(playerUnit2, 0.2);
                    ReduceAttack(playerUnit3, 0.2);
                    ReduceDefense(playerUnit1, 0.2);
                    ReduceDefense(playerUnit2, 0.2);
                    ReduceDefense(playerUnit3, 0.2);
                    DialogueText.text = "The enemy slightly healed themself and lowered your stats!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
                case 4 :case 5:
                {
                    //buffs enemy
                    IncreaseAttack(enemyUnit1, 0.4);
                    IncreaseDefense(enemyUnit1, 0.4);
                    DialogueText.text = "The enemy increased their attack and defense!";
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(CheckPartyHP(numTurns));
                    break;
                }
            }
        }
        else
        {
            switch (rand)
            {
                case 0: case 1:
                //the enemy does a basic attack
                PlayerScript target = enemyTarget();
                target.TakeDamage(enemyUnit1.attack - target.defense);
                DialogueText.text = "The enemy dealt " + (enemyUnit1.attack - target.defense) + " damage";
                yield return new WaitForSeconds(2f);
                StartCoroutine(CheckPartyHP(numTurns));
                break;

                case 2:
                //attacks all
                playerUnit1.TakeDamage(enemyUnit1.attack - playerUnit1.defense);
                playerUnit2.TakeDamage(enemyUnit1.attack - playerUnit2.defense);
                playerUnit3.TakeDamage(enemyUnit1.attack - playerUnit3.defense);
                DialogueText.text = "The enemy attacked everyone!";
                yield return new WaitForSeconds(2f);
                StartCoroutine(CheckPartyHP(numTurns));
                break;

                case 3:case 4:
                //buffs enemy
                IncreaseAttack(enemyUnit1, 0.4);
                IncreaseDefense(enemyUnit1, 0.4);
                DialogueText.text = "The enemy increased their attack and defense!";
                yield return new WaitForSeconds(2f);
                StartCoroutine(CheckPartyHP(numTurns));
                break;

                case 5:
                //debuffs all players
                enemyUnit1.currentHP += 200;
                ReduceAttack(playerUnit1, 0.2);
                ReduceAttack(playerUnit2, 0.2);
                ReduceAttack(playerUnit3, 0.2);
                ReduceDefense(playerUnit1, 0.2);
                ReduceDefense(playerUnit2, 0.2);
                ReduceDefense(playerUnit3, 0.2);
                DialogueText.text = "The enemy slightly healed themself and lowered your stats!";
                yield return new WaitForSeconds(2f);
                StartCoroutine(CheckPartyHP(numTurns));
                break;
            }
        }
    }

    IEnumerator EndBattle()
    {
        battleCount++;
        if(state == BattleState.WON)
        {
            DialogueText.text = "You won!";
            yield return new WaitForSeconds(2f);
            
            
            switch (battleCount){
                case 2: 
                {
                    enemyImage.sprite = laughing1;
                    break;
                }
                case 3:
                {
                    enemyImage.sprite = laughing2;
                    break;
                }
                case 4:
                {
                    enemyImage.sprite = laughing3;
                    break;
                }  
                case 5:
                {
                    enemyImage.sprite = laughing4;
                    battleCount = 1;
                    break;
                }
            }
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("SampleScene");
        }
        else if(state == BattleState.LOST)
        {
            DialogueText.text = "You weren't funny enough...";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Game Over");
        }
    }

    //Code for attack
    IEnumerator PlayerAttack()
    {
        PartyTurn tempTurn = PartyTurn.NONE;
        switch (turn){
            case PartyTurn.MCTURN:
            {
                //Lifesteal attack

                tempTurn = PartyTurn.MCTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage((int)(playerUnit1.attack * 1.5) - enemyUnit1.defense);
                int drainedhp = (int)(playerUnit1.attack * 1.5) - enemyUnit1.defense;
                if ((playerUnit1.currentHP + (int)(drainedhp * 0.2)) > playerUnit1.maxHP)
                {
                    playerUnit1.currentHP = playerUnit1.maxHP;
                }
                else
                {
                    playerUnit1.currentHP += (int)(drainedhp * 0.2);
                }
                DialogueText.text = "Quincy trampled the enemy with a balloon horse, restoring some HP in the process! " + drainedhp.ToString() + " damage dealt!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.SECONDTURN:
            {
                //Team heal + attack

                tempTurn = PartyTurn.SECONDTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage(playerUnit2.attack - enemyUnit1.defense);
                PlayerScript target = getLowestHP(playerUnit1, playerUnit2, playerUnit3);
                if (target.currentHP < target.maxHP)
                {
                    if ((target.currentHP + 150) > target.maxHP)
                    {
                        target.currentHP = target.maxHP;
                    }
                    else
                    {
                        target.currentHP += 150;
                    }
                }
                DialogueText.text = "Qwynn ran them over with a clown car while delivering healing food to a teammate! " + playerUnit2.attack + " damage dealt!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.THIRDTURN:
            {
                //Buff team

                tempTurn = PartyTurn.THIRDTURN;
                turn = PartyTurn.NONE;
                IncreaseAttack(playerUnit3, 0.4);
                IncreaseAttack(playerUnit1, 0.4);
                IncreaseAttack(playerUnit2, 0.4);
                DialogueText.text = "Quandale attack with made a spear out of balloons, raising everyone's attack in the process!";
                yield return new WaitForSeconds(2f);
                break;
            }
        }
        CheckNextTurn(tempTurn);
    }

    //Code for skills
    IEnumerator PlayerSkill()
    {
        PartyTurn tempTurn = PartyTurn.NONE;
        switch (turn){
            case PartyTurn.MCTURN:
            {
                //Deal mega damage with balloon swords

                tempTurn = PartyTurn.MCTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage(playerUnit1.attack * 5 - enemyUnit1.defense);
                DialogueText.text = "Quincy attacked with a flurry of balloon swords! " + (playerUnit1.attack * 5 - enemyUnit1.defense) + " damage dealt!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.SECONDTURN:
            {
                //Deal damage with grenade

                tempTurn = PartyTurn.SECONDTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage(playerUnit2.attack - enemyUnit1.defense);
                DialogueText.text = "Qwynn threw a confetti grenade, dealing " + (playerUnit2.attack - enemyUnit1.defense) + " damage and lowering their defense!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.THIRDTURN:
            {
                //Debuff enemy

                tempTurn = PartyTurn.THIRDTURN;
                turn = PartyTurn.NONE;
                if (!(enemyUnit1.attack < enemyUnit1.baseAttack))
                {
                    enemyUnit1.attack = (int)(enemyUnit1.attack * 0.8);
                }
                DialogueText.text = "Quandale tricked them with a water flower, lowering the enemy's attack!";
                yield return new WaitForSeconds(2f);
                break;
            }
        }
        CheckNextTurn(tempTurn);
    }

    //Code for Actions
    IEnumerator PlayerAction()
    {
        PartyTurn tempTurn = PartyTurn.NONE;
        switch (turn){
            case PartyTurn.MCTURN:
            {
                //Deal damage with bubbles

                tempTurn = PartyTurn.MCTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage((int)(playerUnit1.attack * 1.5) - enemyUnit1.defense);
                ReduceDefense(enemyUnit1, 0.1);
                DialogueText.text = "Quincy fired a bunch of bubbles, dealing " + ((int)(playerUnit1.attack * 1.5) - enemyUnit1.defense) + " damage and lowering the enemy's defense!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.SECONDTURN:
            {
                //Attack through enemy defense

                tempTurn = PartyTurn.SECONDTURN;
                turn = PartyTurn.NONE;
                enemyUnit1.TakeDamage(playerUnit2.attack * 3);
                DialogueText.text = "Qwynn punched the enemy like they were a boulder, ignoring defense! " + playerUnit2.attack * 3 + " damage dealt!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.THIRDTURN:
            {
                //Heal lowest teammate

                tempTurn = PartyTurn.THIRDTURN;
                turn = PartyTurn.NONE;
                PlayerScript target = getLowestHP(playerUnit1, playerUnit2, playerUnit3);
                if (target.currentHP < target.maxHP)
                {
                    if ((target.currentHP + playerUnit3.attack) > target.maxHP)
                    {
                        target.currentHP = target.maxHP;
                    }
                    else
                    {
                        target.currentHP += playerUnit3.attack;
                    }
                }
                DialogueText.text = "Quandale embarrassed himself with an unfunny joke, healing his teammate!";
                yield return new WaitForSeconds(2f);
                break;
            }
        }
        CheckNextTurn(tempTurn);
    }

    //Code for Supports
    IEnumerator PlayerSupport()
    {
        PartyTurn tempTurn = PartyTurn.NONE;
        switch (turn){
            case PartyTurn.MCTURN:
            {
                //Increase team defense

                tempTurn = PartyTurn.MCTURN;
                turn = PartyTurn.NONE;
                DialogueText.text = "Quincy mewed to raise his confidence, increasing his party's defense!";
                IncreaseDefense(playerUnit1, 0.2);
                IncreaseDefense(playerUnit2, 0.2);
                IncreaseDefense(playerUnit3, 0.2);
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.SECONDTURN:
            {
                //Remove team debuffs

                tempTurn = PartyTurn.SECONDTURN;
                turn = PartyTurn.NONE;
                RemoveDebuffs(playerUnit1);
                RemoveDebuffs(playerUnit2);
                RemoveDebuffs(playerUnit3);
                DialogueText.text = "Qwynn told a refreshing joke, removing all debuffs on the party!";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.THIRDTURN:
            {
                //Remove enemy buffs

                tempTurn = PartyTurn.THIRDTURN;
                turn = PartyTurn.NONE;
                RemoveBuffs(enemyUnit1);
                DialogueText.text = "Quandale spolied JJK for the enemy, removing their buffs!";
                yield return new WaitForSeconds(2f);
                break;
            }
        }
        CheckNextTurn(tempTurn);
    }

    //Code for skills
    IEnumerator PlayerSpecial()
    {
        PartyTurn tempTurn = PartyTurn.NONE;
        switch (turn){
            case PartyTurn.MCTURN:
            {
                //Self-attack

                tempTurn = PartyTurn.MCTURN;
                turn = PartyTurn.NONE;
                playerUnit1.currentHP = 1;
                DialogueText.text = "Quincy sacrificed his own HP because he thought it was funny! Why would you use this move?";
                yield return new WaitForSeconds(2f);
                break;
            }
            case PartyTurn.SECONDTURN:
            {
                //If you roll a 7 you win!

                tempTurn = PartyTurn.SECONDTURN;
                turn = PartyTurn.NONE;
                if (7==Random.Range(1,20)){
                    enemyUnit1.TakeDamage(enemyUnit1.maxHP);}
                else{
                    DialogueText.text = "Qwynn did nothing! Thanks for nothing Qwynn! Love u <3";
                    yield return new WaitForSeconds(2f);}
                break;
            }
            case PartyTurn.THIRDTURN:
            {   
                //Self-embarrassment; Debuff all players

                tempTurn = PartyTurn.THIRDTURN;
                turn = PartyTurn.NONE;
                ReduceAttack(playerUnit1, 0.5);
                ReduceAttack(playerUnit2, 0.5);
                ReduceAttack(playerUnit3, 0.5);
                DialogueText.text = "We don't talk about Quandale... let's just skip his turn...";
                yield return new WaitForSeconds(2f);
                break;
            }
        }
        CheckNextTurn(tempTurn);
    }

    //Peforms the attack function
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN || turn == PartyTurn.NONE)
            return;
        if (turn == PartyTurn.MCTURN)
        {
            StartCoroutine(PlayerAttack());
        }
        if (turn == PartyTurn.SECONDTURN)
        {
            StartCoroutine(PlayerAttack());
        }
        if (turn == PartyTurn.THIRDTURN)
        {
            StartCoroutine(PlayerAttack());
            state = BattleState.ENEMYTURN;
        }
    }

    //Performs the skill function
    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN || turn == PartyTurn.NONE)
            return;
        if (turn == PartyTurn.MCTURN)
        {
            StartCoroutine(PlayerSkill());
        }
        if (turn == PartyTurn.SECONDTURN)
        {
            StartCoroutine(PlayerSkill());
        }
        if (turn == PartyTurn.THIRDTURN)
        {
            StartCoroutine(PlayerSkill());
            state = BattleState.ENEMYTURN;
        }
    }

    //Performs the action function
    public void OnActionButton()
    {
        if (state != BattleState.PLAYERTURN || turn == PartyTurn.NONE)
            return;
        if (turn == PartyTurn.MCTURN)
        {
            StartCoroutine(PlayerAction());
        }
        if (turn == PartyTurn.SECONDTURN)
        {
            StartCoroutine(PlayerAction());
        }
        if (turn == PartyTurn.THIRDTURN)
        {
            StartCoroutine(PlayerAction());
            state = BattleState.ENEMYTURN;
        }
    }

    //Performs the support function
    public void OnSupportButton()
    {
        if (state != BattleState.PLAYERTURN || turn == PartyTurn.NONE)
            return;
        if (turn == PartyTurn.MCTURN)
        {
            StartCoroutine(PlayerSupport());
        }
        if (turn == PartyTurn.SECONDTURN)
        {
            StartCoroutine(PlayerSupport());
        }
        if (turn == PartyTurn.THIRDTURN)
        {
            StartCoroutine(PlayerSupport());
            state = BattleState.ENEMYTURN;
        }
    }

    //Performs the support function
    public void OnSpecialButton()
    {
        if (state != BattleState.PLAYERTURN || turn == PartyTurn.NONE)
            return;
        if (turn == PartyTurn.MCTURN)
        {
            StartCoroutine(PlayerSpecial());
        }
        if (turn == PartyTurn.SECONDTURN)
        {
            StartCoroutine(PlayerSpecial());
        }
        if (turn == PartyTurn.THIRDTURN)
        {
            StartCoroutine(PlayerSpecial());
            state = BattleState.ENEMYTURN;
        }
    }

    //finds the party member with the lowest hp
    public PlayerScript getLowestHP(PlayerScript p1, PlayerScript p2, PlayerScript p3)
    {
        if (p1.currentHP < p2.currentHP && p1.currentHP < playerUnit3.currentHP)
        {
            if(p1.currentHP > 0)    {return p1;}

            else if(p2.currentHP < p3.currentHP)
            {
                if(p2.currentHP > 0)    {return p2;}

                else    {return p3;}
            }
            else    {return p1;}
        }
        else if (p2.currentHP < p1.currentHP && p2.currentHP < p3.currentHP)
        {
            if (p2.currentHP > 0)   {return p2;}

            else if (p1.currentHP < p3.currentHP)
            {
                if (p1.currentHP > 0)   {return p1;}

                else    {return p3;}
            }
            else    {return p1;}
        }
        else if (p3.currentHP < p2.currentHP && p3.currentHP < p1.currentHP)
        {
            if (p3.currentHP > 0)   {return p3;}

            else if (p1.currentHP < p2.currentHP)
            {
                if (p1.currentHP > 0)   {return p1;}

                else    {return p2;}
            }
            else    {return p1;}
        }
        else    {return p1;}
    }

    //enemy chooses which player to target
    public PlayerScript enemyTarget()
    {
        List<PlayerScript> partymems = new List<PlayerScript>();
        if (playerUnit1.currentHP > 0)
        {
            partymems.Add(playerUnit1);
        }
        if (playerUnit2.currentHP > 0)
        {
            partymems.Add(playerUnit2);
        }
        if (playerUnit3.currentHP > 0)
        {
            partymems.Add(playerUnit3);
        }

        //enemy randomly decides which remaining party member to attack
        var random = new System.Random();
        int targetnum = random.Next(0, partymems.Count);
        PlayerScript target = partymems[targetnum];
        partymems.Clear();
        return target;
    }

    //reduces target defense by a ratio
    public void ReduceDefense(PlayerScript target, double ratio)
    {
        int newDefense = target.defense - (int)(target.baseDefense * ratio);
        if(newDefense < 0)
        {
            target.defense = 0;
        }
        else
        {
            target.defense = newDefense;
        }
    }

    //reduces target attack by a ratio
    public void ReduceAttack(PlayerScript target, double ratio)
    {
        int newAttack = target.attack - (int)(target.baseAttack * ratio);
        if (newAttack < 0)
        {
            target.attack = 0;
        }
        else
        {
            target.attack = newAttack;
        }
    }

    public void IncreaseDefense(PlayerScript target, double ratio)
    {
        int newDefense = target.defense + (int)(target.baseDefense * ratio);
        target.defense = newDefense;
    }

    public void IncreaseAttack(PlayerScript target, double ratio)
    {
        int newAttack = target.attack + (int)(target.baseAttack * ratio);
        target.attack = newAttack;
    }

    public void RemoveDebuffs(PlayerScript target)
    {
        target.attack = target.baseAttack;
        target.defense = target.baseDefense;
    }

    public void RemoveBuffs(PlayerScript target)
    {
        target.attack = target.baseAttack;
        target.defense = target.baseDefense;
    }

    //Checks who's turn is next (Player or enemy)
    public void CheckNextTurn(PartyTurn tempTurn){
        
        //checks wether you killed the enemy
        if (enemyUnit1.currentHP <= 0)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        
        //determines which member goes next or if it's the enemies turn
        else
        {
            if (tempTurn == PartyTurn.MCTURN)
            {
                if (playerUnit2.currentHP > 0)
                {
                    state = BattleState.PLAYERTURN;
                    turn = PartyTurn.SECONDTURN;
                    UpdateButtonText();
                }
                else if (playerUnit3.currentHP > 0)
                {
                    state = BattleState.PLAYERTURN;
                    turn = PartyTurn.THIRDTURN;
                    UpdateButtonText();
                }
            }
            else if (tempTurn == PartyTurn.SECONDTURN)
            {
                if (playerUnit3.currentHP > 0)
                {
                    state = BattleState.PLAYERTURN;
                    turn = PartyTurn.THIRDTURN;
                    UpdateButtonText();
                }
            }
            else if (tempTurn == PartyTurn.THIRDTURN)
            {
                state = BattleState.ENEMYTURN;
                turn = PartyTurn.NONE;
                UpdateButtonText();
                StartCoroutine(EnemyTurn(enemyUnit1.enemyTurnCount));
            }

        }
    }

    //Updates player HUD with new player health
    public void UpdatePlayerHealth(){
        playerHUD1.SetHP(playerUnit1, lerpSpeed);
        playerHUD2.SetHP(playerUnit2, lerpSpeed);
        playerHUD3.SetHP(playerUnit3, lerpSpeed);
        enemyHUD1.SetHP(enemyUnit1, lerpSpeed);
    }  

    //Checks if all party members are dead
    IEnumerator CheckPartyHP(int turnsleft){
        int turns = turnsleft;
        if (playerUnit1.currentHP <= 0 && playerUnit2.currentHP <= 0 && playerUnit3.currentHP <= 0){
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            //lets the enemy move again if they have remaining turns
            if (turns > 0)
            {
                yield return new WaitForSeconds(2f);
                DialogueText.text = "The enemy moves again!";
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn(turns-1));
            }
            else
            {
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
            }
        }
    }

    //Sets the text for UI buttons
    public void UpdateButtonText(){
        switch (turn){
            case PartyTurn.NONE:
                AttackButtonAltText.text= "Attack";
                SkillText.text = "Skill";
                SkillButtonAltText.text="Skill";
                ActionText.text = "Action";
                ActionButtonAltText.text="Action";
                SupportText.text = "Support";
                SupportButtonAltText.text="Support";
                SpecialText.text = "Special";
                SpecialButtonAltText.text="Special";
                break;
            case PartyTurn.MCTURN:
                DialogueText.text = "Quincy moves";
                AttackButtonAltText.text= "Big Damage + Lifesteal";
                SkillText.text = "Balloon Flurry";
                SkillButtonAltText.text="Mega Damage";
                ActionText.text = "Bubble Bazooka";
                ActionButtonAltText.text="Big Damage + Lower Enemy Defense";
                SupportText.text = "Looksmaxx";
                SupportButtonAltText.text="Increase Team Defense";
                SpecialText.text = "Scary Clown";
                SpecialButtonAltText.text="I Wouldn't Recommend...";
                break;
            case PartyTurn.SECONDTURN:
                DialogueText.text = "Qwynn moves!";
                AttackButtonAltText.text= "Small Damage + Heal team";
                SkillText.text = "Confetti Grenade";
                SkillButtonAltText.text="Medium Damage";
                ActionText.text = "Resident Funny";
                ActionButtonAltText.text="Mega Damage";
                SupportText.text = "Refreshing Joke";
                SupportButtonAltText.text="Remove Team's Debuffs";
                SpecialText.text = "Nah, I'd Win";
                SpecialButtonAltText.text="Test Your Luck";
                break;
            case PartyTurn.THIRDTURN:
                DialogueText.text = "Quandale moves!";
                AttackButtonAltText.text= "Buff Team's Attack";
                SkillText.text = "Water Flower";
                SkillButtonAltText.text="Lower Enemy's Attack";
                ActionText.text = "Tragic Display";
                ActionButtonAltText.text="Heal Lowest Teammate";
                SupportText.text = "Manga Spoiler";
                SupportButtonAltText.text="Remove Enemy Buffs";
                SpecialText.text = "Final Form";
                SpecialButtonAltText.text="Quandale, Please Don't";
                break;
        } 
    }
}