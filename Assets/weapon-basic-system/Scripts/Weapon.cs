using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace IsmaelNascimento
{
    public class Weapon : MonoBehaviour
    {
        #region VARIABLES

        private InputsGame inputsGame;
        public enum Type
        {
            INFINITY,
            RANGED,
        }

        [Header("General")]
        [SerializeField] private string id;
        [SerializeField] private Type WeaponType;

        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private List<Transform> bulletsPlace = new List<Transform>();
        [SerializeField] private int damage;
        [SerializeField] private Sprite Icon;

        [Header("Shooting")]
        [SerializeField] private float delayBetweenShot;
        [SerializeField] private float bulletSpeed = 1f;
        [SerializeField] private float impulseForce;
        [SerializeField] private ParticleSystem shotParticleSystem;

        [Header("Reload")]
        [SerializeField] private float timerForReload;
        private bool isReloading;

        [Header("Audio")]
        [SerializeField] private string audioShotName;

        private bool CanShoot = true;

        public static Action<Weapon> OnWeaponEnable;

        #endregion

        #region MONOBEHAVIOUR_METHODS

        private void Awake()
        {
            inputsGame = new InputsGame();
        }

        private void OnEnable()
        {
            inputsGame.Enable();
            OnWeaponEnable?.Invoke(this);
        }

        private void OnDisable()
        {
            inputsGame.Disable();
        }

        private void Start()
        {
            inputsGame.Player.Shoot.performed += Shoot;
        }

        #endregion

        #region PRIVATE_METHODS

        private void Shoot(CallbackContext callbackContext)
        {
            if (CanShoot)
            {
                CanShoot = false;
                for (int i = 0; i < bulletsPlace.Count; i++)
                {
                    ParticleSystem shotParticleSystemCreated = Instantiate(shotParticleSystem, bulletsPlace[i].transform.position, Quaternion.identity);
                    Destroy(shotParticleSystemCreated.gameObject, 1f);
                    Bullet bullet = Instantiate(bulletPrefab, bulletsPlace[i].transform.position, Quaternion.identity);
                    bullet.Setup(damage, bulletSpeed, bulletsPlace[i].transform.position);
                }
                CanShoot = true;
            }
        }

        #endregion

        #region PUBLIC_METHODS

        public Sprite GetIcon()
        {
            return Icon;
        }

        #endregion
    }
}