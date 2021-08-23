using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles switching of weapons.

public class WeaponManager : MonoBehaviour
{
    // weapon accessor for external classes
    public IWeapon CurrentWeapon { get; private set; }

    [SerializeField]
    private GameObject[] weapons; // set size in inspector
    [SerializeField]
    private bool[] isEnabled; // set in inspector
    private int arrayPos;

    Animator entityAnimator; // entity weapon is attached to

    private void Awake()
    {
        entityAnimator = GetComponent<Animator>();
        InputHandler.OnWeaponChangeButton.AddListener(Next);
    }

    private void Start()
    {
        // create the weapons
        foreach (GameObject weapon in weapons)
        {
            //Instantiate(weapon);
            weapon.SetActive(false);
        }

        // Set current starting weapon
        weapons[0].SetActive(true);
        CurrentWeapon = weapons[0].GetComponent<IWeapon>();
    }

    public void Next()
    {
        Debug.Log("Next called");
        CurrentWeapon.SetActive(false);
        arrayPos++;
        if (arrayPos == weapons.Length)
            arrayPos = 0;

        while (!isEnabled[arrayPos])
        {
            arrayPos++;
            if (arrayPos == weapons.Length)
                arrayPos = 0;
        }

        CurrentWeapon = weapons[arrayPos].GetComponent<IWeapon>();
        CurrentWeapon.SetActive(true);
        WeaponData data = weapons[arrayPos].GetComponent<Weapon>().data;
        GameEvents.OnWeaponChange.Invoke(data);
    }

    private void OnDestroy()
    {
        InputHandler.OnWeaponChangeButton.RemoveListener(Next);

    }
}
