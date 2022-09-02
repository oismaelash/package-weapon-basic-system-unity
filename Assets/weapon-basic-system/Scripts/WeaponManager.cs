using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using System.Collections.Generic;

namespace IsmaelNascimento
{
    public class WeaponManager : MonoBehaviour
    {
        #region VARIABLES

        public static WeaponManager Instance;
        private InputsGame inputsGame;
        private int currentWeapon = 0;

        [Header("General")]
        [SerializeField] private List<Weapon> weapons;
        [Header("Player")]
        [SerializeField] private Transform weaponPivot;
        [Header("Canvas/UI")]
        [SerializeField] private Image weaponImage;
        [SerializeField] private TextMeshProUGUI bulletCountText;

        #endregion

        #region MONOBEHAVIOUR_METHODS

        private void Awake()
        {
            Instance = this;
            inputsGame = new InputsGame();
        }

        private void OnEnable()
        {
            inputsGame.Enable();
            Weapon.OnWeaponEnable += OnWeaponEnable_Action;
        }

        private void Start()
        {
            inputsGame.Player.MouseScrollY.performed += OnChangeWeapon;
        }

        private void OnDisable()
        {
            inputsGame.Disable();
        }

        #endregion

        #region PRIVATE_METHODS

        private void OnChangeWeapon(CallbackContext callbackContext)
        {
            float mouseScrollY = callbackContext.ReadValue<float>();
            Debug.Log(mouseScrollY);

            int weaponCount = DisableAllWeapons();

            for (int index = 0; index < weaponCount; index++)
                weaponPivot.GetChild(index).gameObject.SetActive(false);

            if (mouseScrollY == 120f)
            {
                currentWeapon++;

                if (currentWeapon >= weaponCount)
                {
                    currentWeapon = weaponCount - 1;
                }
            }
            else if (mouseScrollY == -120f)
            {
                currentWeapon--;

                if (currentWeapon < 0)
                {
                    currentWeapon = 0;
                }
            }

            weaponPivot.GetChild(currentWeapon).gameObject.SetActive(true);
        }

        private void OnWeaponEnable_Action(Weapon weapon)
        {
            weaponImage.sprite = weapon.GetIcon();
        }

        private int DisableAllWeapons()
        {
            int weaponCount = weaponPivot.childCount;

            for (int index = 0; index < weaponCount; index++)
            {
                weaponPivot.GetChild(index).gameObject.SetActive(false);
            }

            return weaponCount;
        }

        #endregion

        #region PUBLIC_METHODS

        [ContextMenu("GetNewWeapon")]
        public void GetNewWeapon()
        {
            DisableAllWeapons();
            Weapon weaponCreated = Instantiate(weapons[1], weaponPivot.transform.position, Quaternion.identity);
            weaponCreated.transform.SetParent(weaponPivot);
        }

        public void GetNewWeapon(Weapon weapon)
        {
            DisableAllWeapons();
            Weapon weaponCreated = Instantiate(weapon, weaponPivot.transform.position, Quaternion.identity);
            weaponCreated.transform.SetParent(weaponPivot);
        }

        #endregion
    }
}