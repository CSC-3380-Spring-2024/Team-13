using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player collides with an enemy enter into BattleScene
        if (collision.gameObject.tag == "Enemy")
        {
            SceneHistory.LoadScene("BattleScene");
        }
    }
}
