using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EntityStats
    {
        public float Health { get; private set; }
        [SerializeField] public float MaxHealth { get; private set; }
        [SerializeField] public float Damage { get; private set; }
        [SerializeField] public float Speed { get; private set; }

        public EntityStats(float maxHealth, float damage, float speed)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
            Damage = damage;
            Speed = speed;
        }

        public void SetHealth(float health)
        {
            Health = health;
        }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
    }
}