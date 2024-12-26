using UnityEngine;

public class GunBasics : MonoBehaviour
{
    public float damage = 20;

    void Update()
    {
        LookAtMouse();
        FireBullet();
    }

    void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //angle = Mathf.Clamp(angle, -90f, 90f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FireBullet()
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Attack]))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

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
                    Debug.Log("Ray hit: " + hit.collider.name);
                    HitSomeone(hit.collider.gameObject);
                    return;
                }
            }
        }
    }

    void HitSomeone(GameObject gameObject)
    {
        Transform currentTransform = gameObject.transform;

        while (currentTransform != null)
        {
            if (currentTransform.GetComponent<IDamageable>() != null)
            {
                currentTransform.GetComponent<IDamageable>().TakeDamage(damage);
                return; // Exit once we find the PlayerConstants component
            }
        }
    }
}
