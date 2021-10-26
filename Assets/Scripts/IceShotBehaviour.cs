using UnityEngine;

public class IceShotBehaviour : BaseShotBehaviour
{
    [SerializeField] private float freezeTime = 2;
    [SerializeField] private AudioClip freezeClip;
    
    private const float MaxLifetime = 10;
    
    private void Start()
    {
        Destroy(gameObject, MaxLifetime);
    }
    
    public override void AdditionalShotBehaviour(GameObject otherObject)
    {
        otherObject.GetComponent<EnemyBehaviour>().Freeze(freezeTime, freezeClip);
    }
}