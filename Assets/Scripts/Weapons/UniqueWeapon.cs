using UnityEngine;

public class UniqueWeapon : WeaponBase
{
    protected UniqueWeaponData UniqueWeaponData => WeaponData as UniqueWeaponData;
    private MouseFollow mouseFollow;
    private CharacterBase characterBase;
    private float nextFireTime = 0f;

    private void Start()
    {
        mouseFollow = GetComponentInParent<MouseFollow>();
        characterBase = GetComponentInParent<CharacterBase>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Attack(mouseFollow.MouseTracker());
            nextFireTime = Time.time + 1f / WeaponData.FireRate;
        }
    }
    public override void Attack(Vector3 position)
    {
        Vector2 direction = (position - characterBase.transform.position).normalized;
        GameObject bulletObj = Instantiate(UniqueWeaponData.bulletPrefab, characterBase.transform.position + characterBase.transform.right, Quaternion.identity);

        UniqueBullet uniqueBullet = bulletObj.GetComponent<UniqueBullet>();
        if (uniqueBullet != null)
        {
            uniqueBullet.UniqueWeapon = this;
            uniqueBullet.SetDirection(direction);
        }

        Destroy(bulletObj, 3f);
    }
}
