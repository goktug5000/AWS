using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public void SetMe(float dmg)
    {
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (!collision.transform.IsChildOf(PlayerConstants.currentPlayer.transform))
            {
                GunBasics.HitSomeone(collision.gameObject, damage);
                Destroy(this.gameObject);
            }
        }
    }
}
