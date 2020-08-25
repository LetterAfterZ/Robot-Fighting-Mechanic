using UnityEngine;

public class RobotLaser : MonoBehaviour
{
    [SerializeField] private int _maxPower = 100;
    [SerializeField] private int _currPower;

    [SerializeField] HealthBar _powerbar = null;
    [SerializeField] GameEvent _laserCharged = null;

    private void Start() {
        //set initial charge
        _currPower = 0;
        //update power bar
        _powerbar.SetMaxHealth(_maxPower);
        _powerbar.SetHealth(_currPower);
    }

    public void ChargeLaser(int charge){
        _currPower += charge;
        _powerbar.SetHealth(_currPower);
        
        if (_currPower >= _maxPower)
            _laserCharged.Raise();
    }
}
