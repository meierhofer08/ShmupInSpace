using System;
using UnityEngine;

public class LaserShotBehaviour : BaseShotBehaviour
{
    [SerializeField] private float timeToLive; 
    
    private bool _shotStarted;
    private float _remainingTTL;
    public override void AdditionalShotBehaviour(GameObject otherObject)
    {
        // nothing
    }

    private void Update()
    {
        if (!triggered)
        {
            return;
        }

        if (!_shotStarted)
        {
            _shotStarted = true;
            _remainingTTL = timeToLive;
        }
        else
        {
            _remainingTTL -= Time.deltaTime;
            if (_remainingTTL <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}