using UnityEngine;

[System.Serializable]
public class PlayerHealth : Health
{
    public RectTransform HPRect;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHpBar();
    }

    public override void Regen()
    {
        base.Regen();
        UpdateHpBar();
    }

    public void UpdateHpBar()
    {
        var hpPercentage = HP / HP_Max;
        HPRect.localScale = new Vector3(hpPercentage, 1, 1);
    }

    public override void Die()
    {
        Debug.Log("Player öldü");
    }
}
