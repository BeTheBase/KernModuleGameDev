using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Player behaviour with all player shooting related functionality
/// </summary>
public class PlayerShooting : AbstractAvatarClass
{
    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; set => cooldown = value; }

    [SerializeField] private float timeBetween;
    public float TimeBetween { get => timeBetween; set => timeBetween = value; }

    [SerializeField] private string currentProjectileName;
    public string CurrentProjectileName { get => currentProjectileName; set => currentProjectileName = value; }

    [SerializeField] private WeaponData usingWeaponData;
    public WeaponData UsingWeaponData { get => usingWeaponData; set => usingWeaponData = value; }


    private Weapon currentWeapon { get; set; }


    private void Awake()
    {
        //Create a weapon and give its weapon data
        currentWeapon = new Weapon(UsingWeaponData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(PlayerInput.FireKey) && currentWeapon.WeaponReady())
            Fire();
    }

    private void Fire()
    {
        Coroutine cooldownRoutine = StartCoroutine(currentWeapon.WaitForCooldown(Cooldown, TimeBetween));
    }

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
