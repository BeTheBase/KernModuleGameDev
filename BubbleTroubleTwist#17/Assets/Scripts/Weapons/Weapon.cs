using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class that stores each weapon data
/// </summary>
[System.Serializable]
public class WeaponData
{
    [SerializeField] private GameObject projectileGameObject;
    public GameObject ProjectileGameObject { get => projectileGameObject; set => projectileGameObject = value; }

    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField] private int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField]private float amount;
    public float Amount { get => amount; set => amount = value; }

    [SerializeField] private float height = 50f;
    public float Height { get => height; set => value = height; }

    [SerializeField] private float shootSpeed = 5f;
    public float ShootSpeed { get => shootSpeed; set => value = shootSpeed; }
}

/// <summary>
/// Weapon class with all weapon functionality
/// </summary>
public class Weapon 
{
    public WeaponData ThisWeaponData { get; set; }

    public bool ready = true;

    private float amount = 10;

    public Weapon(WeaponData _thisWeaponData)
    {
        ThisWeaponData = _thisWeaponData;
    }
   

    public void FireWeapon()
    {
        GameObject projectile = ObjectPoolerLearning.Instance.SpawnFromPool<Projectile>(ThisWeaponData.FirePoint.position, Quaternion.identity).gameObject;
        if (projectile != null)
        {
            projectile.transform.LerpTransform(projectile.GetComponent<MonoBehaviour>(), projectile.transform.position + Vector3.up * ThisWeaponData.Height, ThisWeaponData.ShootSpeed);
            projectile.gameObject.GetComponent<Projectile>().SetDamage(ThisWeaponData.Damage);
        }
    }

    public IEnumerator WaitForCooldown(float _time, float _timeBetween)
    {
        amount = ThisWeaponData.Amount;
        ready = false;
        while (amount > 1)
        {
            FireWeapon();
            yield return new WaitForSeconds(_timeBetween);
            amount--;
        }
        yield return new WaitForSeconds(_time);
        ready = true;
        amount = ThisWeaponData.Amount;
    }

    public bool WeaponReady()
    {
        return ready;
    }
}
