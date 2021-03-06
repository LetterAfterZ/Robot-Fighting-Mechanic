﻿using UnityEngine;

public class RobotHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currHealth;

    [SerializeField] HealthBar _healthbar = null;
    [SerializeField] GameEvent _playerRobotDamaged = null;
    [SerializeField] GameEvent _playerRobotDestroyed = null;

    private void Start() {
        //set initial health
        _currHealth = _maxHealth;
        //update health bar
        _healthbar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage){
        _currHealth -= damage;
        _healthbar.SetHealth(_currHealth);
        _playerRobotDamaged.Raise();

        if (_currHealth <= 0)
            _playerRobotDestroyed.Raise();
    }
}
