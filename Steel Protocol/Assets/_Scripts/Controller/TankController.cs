using UnityEngine;
using SteelProtocol.Controller.Common;
using SteelProtocol.Controller.Common.Weapons;

namespace SteelProtocol.Controller
{
    public class TankController : MonoBehaviour
    {
        [Header("Tank Components")]
        [SerializeField] private MovementController movement;
        [SerializeField] private TurretController aiming;
        [SerializeField] private WeaponController weapon;
        [SerializeField] private HealthController health;

/*
        public void TakeDamage(float amount)
        {
            health?.TakeDamage(amount);
        }

        public void FireWeapon()
        {
            weapon?.TryFire();
        }

        // Maybe OnDeath? Notify manager, spawn explosion, etc.

        */
    }

}
