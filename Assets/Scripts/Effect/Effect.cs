using UnityEngine;

public class Effect : MonoBehaviour
{
    float _lifeTime = 1f;
    float _currTime = 0;

    void Update()
    {
        if (_currTime >= _lifeTime){
            Destroy(gameObject);
        }
        _currTime += Time.deltaTime;
    }

}
