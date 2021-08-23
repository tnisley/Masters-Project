using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Updates the heads up display using events

public class HUD : MonoBehaviour
{
    [SerializeField]
    Image[] healthMeter;

    [SerializeField]
    Image currentWeapon;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI gemsRemaining;

    // Sprites for the health meter
    [SerializeField]
    Sprite fullHealthContainer;
    [SerializeField]
    Sprite emptyHealthContainer;

    private void Awake()
    {
        GameEvents.OnPlayerHealthChange.AddListener(UpdateHealth);
        GameEvents.OnWeaponChange.AddListener(UpdateWeapon);
        GameEvents.OnScoreUpdate.AddListener(UpdateScore);
        GameEvents.OnGemsUpdate.AddListener(UpdateGems);
    }


    void UpdateHealth(int health)
    {
        for(int i = 0; i < healthMeter.Length; i++)
        {
            if (i < health)
                healthMeter[i].sprite = fullHealthContainer;
            else
                healthMeter[i].sprite = emptyHealthContainer;
        }
    }

    void UpdateWeapon(WeaponData weapon)
    {
        currentWeapon.sprite = weapon.UISprite;
    }

    void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D6");
    }

    void UpdateGems(int gems)
    {
        gemsRemaining.text = gems.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerHealthChange.RemoveListener(UpdateHealth);
        GameEvents.OnWeaponChange.RemoveListener(UpdateWeapon);
        GameEvents.OnScoreUpdate.RemoveListener(UpdateScore);
        GameEvents.OnGemsUpdate.RemoveListener(UpdateGems);
    }

}
