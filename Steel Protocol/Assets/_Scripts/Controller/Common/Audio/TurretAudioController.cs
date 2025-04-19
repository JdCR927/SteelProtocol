using UnityEngine;
using SteelProtocol.Controller.Manager;

namespace SteelProtocol.Controller.Common.Audio
{
    [RequireComponent(typeof(TurretController))]
    public class TurretAudioController : MonoBehaviour
    {
        [SerializeField] private float turretVolume = 1f;

        private TurretController turret;
        private AudioSource turretAudioSource;

        private bool wasMoving = false;

        private void Start()
        {
            turret = GetComponent<TurretController>();

            if (turret == null)
                Debug.LogError("Missing TurretController for TurretAudioController");
        }

        private void FixedUpdate()
        {
            float speedX = turret.GetYaw();
            float speedY = turret.GetPitch();

            CheckTurretMovement(speedX, speedY);
        }

        // ToDo: Sounds keep playing despite the turret not moving. Fix this.
        private void CheckTurretMovement(float speedX, float speedY)
        {
            bool isMoving = Mathf.Abs(speedX) > 0.1f || Mathf.Abs(speedY) > 0.1f;

            if (isMoving && !wasMoving)
            {
                turretAudioSource = AudioManager.Instance.PlayLoopedSFX("Turret", turretVolume);
            }
            else if (!isMoving && wasMoving)
            {
                AudioManager.Instance.StopLoopedSFX(turretAudioSource);
                turretAudioSource = null;
            }

            wasMoving = isMoving;
        }
    }
}
