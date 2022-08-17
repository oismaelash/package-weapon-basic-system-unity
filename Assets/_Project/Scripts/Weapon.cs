using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Weapon : MonoBehaviour
{
    private InputsGame inputsGame;
    public enum Type
    {
        INFINITY,
        RANGED,
    }

    [Header("General")]
    public string id;
    public Type WeaponType;
    //public float KnockbackForce;

    public Bullet bulletPrefab;
    public List<Transform> BulletsPlace = new List<Transform>();
    public int Damage;
    [SerializeField] private Sprite Icon;


    [Header("Shooting")]
    public float DelayBetweenShot;
    private float delayBetweenShot;

    public float BulletSpeed;
    public float Impulse;
    public ParticleSystem shotParticleSystem;

    [Header("Reloading")]
    public float TimeToReload;
    private float TimerReaload;
   // [HideInInspector]
    public bool Reloading;

    public bool IsActive = true;
    //private Bullet BulletCode;
    private Rigidbody2D rbPlayer;
    private Vector2 dir;
    private GameObject Player;

    private bool CanShoot = true;
    private int ShootNumber = 0;


    public static Action<Weapon> OnWeaponEnable;

    
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
    void Start()
    {
        inputsGame.Player.Shoot.performed += Shoot;
        //playerInput.Player.Reload.performed += _ => Reload();
        //BulletCode = bulletPrefab.GetComponent<Bullet>();
        delayBetweenShot = DelayBetweenShot;
      
        TimerReaload = TimeToReload;
        delayBetweenShot = DelayBetweenShot;
    }


    void Update()
    {
        if (IsActive)
        {
            if(!CanShoot)
            {
                delayBetweenShot -= Time.deltaTime;
                if(delayBetweenShot <= 0)
                {
                    CanShoot = true;
                    delayBetweenShot = DelayBetweenShot;
                }
            }

        }


    }

    public void Shoot(CallbackContext callbackContext)
    {
        Debug.Log("Shoot perfomed");
        
        if (CanShoot)
        {
            CanShoot = false;
            for (int i = 0; i < BulletsPlace.Count; i++)
            {
                if (ShootNumber == 0)
                {
                    //FindObjectOfType<AudioManager>().Play("PlayerShoot1");
                    ShootNumber++;
                }
                else
                {
                    //FindObjectOfType<AudioManager>().Play("PlayerShoot2");
                    ShootNumber = 0;
                }

                //BulletCode.speed = BulletSpeed;
                //BulletCode.Damage = Damage;
                //BulletCode.Position = BulletsPlace[i].transform.right;
                Instantiate(shotParticleSystem, BulletsPlace[i].transform.position, Quaternion.identity);
                Bullet bullet = Instantiate(bulletPrefab, BulletsPlace[i].transform.position, Quaternion.identity);
                bullet.speed = BulletSpeed;
                bullet.Damage = Damage;
                bullet.Position = BulletsPlace[i].transform.position;


                //dir = Player.transform.position - Bullets.transform.position;
                // dir.Normalize();

                //rbPlayer.velocity = Impulse * dir;
                //rbPlayer.AddForce(dir * Impulse, ForceMode2D.Impulse);                  
                // TimerToShoot = TimeToShoot;
            }
            CanShoot = true;
        }
    }

    /*
    public void Reload()
    {
        if(!Reloading && CurrentAmmo > 0)
        {
            StartCoroutine("ReloadTime");
        }
    }
    IEnumerator ReloadTime()
    {
        FindObjectOfType<AudioManager>().Play("NoAmmo");
        Reloading = true;
        yield return new WaitForSeconds(TimeToReload);
        FindObjectOfType<AudioManager>().Play("Reload");
        CurrentAmmo -= MaxBullet;
        CurrentBullets = MaxBullet;
        Reloading = false;


    }*/

    public Sprite GetIcon()
    {
        return Icon;
    }

}
