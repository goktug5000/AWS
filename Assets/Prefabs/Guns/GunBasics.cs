using UnityEngine;

public class GunBasics : MonoBehaviour
{
    public float damage = 20;
    public int gunType;
    public GameObject bulletObj;

    void Update()
    {
        LookAtMouse();
        Attack();

    }

    void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Attack]))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (gunType == 0)
            {
                FireBullet(mousePosition);
            }
            else
            {
                ThrowBullet(mousePosition);
            }
        }
    }

    void ThrowBullet(Vector3 mousePosition) //TODO: bunu networkde yap
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Attack]))
        {
            var newBullet = Instantiate(bulletObj, transform.position, Quaternion.identity);
            Vector3 direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            newBullet.GetComponent<Bullet>().SetMe(damage);
            newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.right * 10, ForceMode2D.Impulse);
        }
    }

    void FireBullet(Vector3 mousePosition) //TODO: bunu networkde yap
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Attack]))
        {
            Vector2 direction = mousePosition - transform.position;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.transform.IsChildOf(PlayerConstants.currentPlayer.transform))
                    {
                        continue;
                    }
                    HitSomeone(hit.collider.gameObject, damage);
                    return;
                }
            }
        }
    }

    public static void HitSomeone(GameObject gameObject, float damage)
    {
        Transform currentTransform = gameObject.transform;
        for (int i = 0; i < 5; i++)
        {
            if (currentTransform.GetComponent<IDamageable>() != null)
            {
                currentTransform.GetComponent<IDamageable>().TakeDamage(damage);
                return;
            }
            currentTransform = gameObject.transform.parent;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 direction = mousePosition - transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * 100f);

    }
}
