using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp;
    [SerializeField] private float hpMax;
    [SerializeField] private float hpRegen;
    private bool isImmune;
    private bool dead;

    public virtual float HP
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, HP_Max);
    }
    public virtual float HP_Max
    {
        get => hpMax;
        set => hpMax = Mathf.Max(0, value);
    }
    public virtual float HP_Regen
    {
        get => hpRegen;
        set => hpRegen = Mathf.Max(0, value);
    }
    public virtual bool IsImmune
    {
        get => isImmune;
        set => isImmune = value;
    }
    public virtual bool Dead
    {
        get => dead;
        set => dead = value;
    }
    public virtual void TakeDamage(float damage)
    {
        if (IsImmune || Dead)
        {
            return;
        }

        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }
    public virtual void Regen()
    {
        if (!Dead)
        {
            if (HP < HP_Max)
            {
                HP += HP_Regen * Time.deltaTime;
            }
            else
            {
                HP = HP_Max;
            }
        }
    }
    public abstract void Die();
    void Update()
    {
        Regen();
    }
}
