using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty Settings", menuName = "Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
   [Range(0f, 1f)]
   public float playerHPPercentage = 1f;
}
