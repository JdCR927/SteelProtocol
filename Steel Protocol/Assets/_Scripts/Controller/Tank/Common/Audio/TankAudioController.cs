using UnityEngine;
using SteelProtocol.Input;
using SteelProtocol.Manager;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Controller.Tank.Common.Audio
{
    public class TankAudioController : MonoBehaviour
    {
        [Range(0f, 1f), SerializeField] private float engineVolume = 0.2f;
        [Range(0f, 1f), SerializeField] private float trackVolume = 0.5f;
        [Range(0f, 1f), SerializeField] private float turretVolume = 0.5f;

        private MovementController movement;
        private IInputSource input;

        private TankAudioSource engineSound;
        private TankAudioSource trackSound;
        private TankAudioSource turretSound;

        private float lastYRotation;
        private bool wasMoving = false;

        private TankAudioPool pool;

        private void Awake()
        {
            pool = GetComponent<TankAudioPool>();
            movement = GetComponent<MovementController>();
            input = GetComponent<IInputSource>();
        }

        private void Start()
        {
            movement = GetComponent<MovementController>();
            input = GetComponent<IInputSource>();
            lastYRotation = transform.eulerAngles.y;

            engineSound = CreateSound(pool.GetSource((int)EnumAudioIndex.Engine), "engine", engineVolume, true);
            trackSound = CreateSound(pool.GetSource((int)EnumAudioIndex.Tracks), "tracks", trackVolume, true);
            turretSound = CreateSound(pool.GetSource((int)EnumAudioIndex.Turret), "turret", turretVolume, true);

            engineSound?.Play();
        }

        private void FixedUpdate()
        {
            float speed = movement.GetCurrentSpeedAbsolute();
            float pitch = 1f + (speed / 100f);
            engineSound?.SetPitch(pitch);
            trackSound?.SetPitch(pitch);

            float currentYRotation = transform.eulerAngles.y;
            float rotationSpeed = Mathf.Abs(Mathf.DeltaAngle(currentYRotation, lastYRotation));
            lastYRotation = currentYRotation;

            CheckTracks(speed, rotationSpeed);
            CheckTurret(input?.GetLookInput() ?? Vector2.zero);
        }

        private void CheckTracks(float speed, float rotationSpeed)
        {
            bool isMoving = Mathf.Abs(speed) > 0.1f || rotationSpeed > 0.1f;

            if (isMoving && !wasMoving)
                trackSound?.Play();
            else if (!isMoving && wasMoving)
                trackSound?.Stop();

            wasMoving = isMoving;
        }

        private void CheckTurret(Vector2 lookInput)
        {
            bool isMoving = Mathf.Abs(lookInput.x) > 0.1f || Mathf.Abs(lookInput.y) > 0.1f;

            if (isMoving)
            {
                if (!turretSound.IsPlaying())
                {
                    turretSound.Play();
                }

                float randPitch = Random.Range(0.90f, 1.05f);
                turretSound.SetPitch(randPitch);
            }
            else
            {
                if (turretSound.IsPlaying())
                    turretSound.Stop();
            }
        }

        private TankAudioSource CreateSound(AudioSource source, string clipName, float volume, bool loop)
        {
            var clip = AudioManager.Instance.GetSFXClip(clipName);
            if (clip == null) return null;

            var sound = gameObject.AddComponent<TankAudioSource>();
            sound.Initialize(source, clip, loop, volume);
            return sound;
        }
    }
}