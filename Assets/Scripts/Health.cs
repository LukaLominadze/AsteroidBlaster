using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    const string OBSTACLE = "Obstacle";

    public float GetHealth()
    {
        return health;
    }

    public void DamageObject(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            AudioManager.audioDict["Explosion"].Play();

            if(gameObject.tag == OBSTACLE)
            {
                GameLogic.SetScore();
            }

            Destroy(gameObject);
        }
        else
        {
            AudioManager.audioDict["Damage"].Play();
        }
    }
}
