using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float health = 50;

    public void Use(GameObject target)
    {
        var life = target.GetComponent<LivingEntity>();

        if(life != null)
        {
            life.RestoreHealth(health);
        }

        Destroy(gameObject);
    }
}