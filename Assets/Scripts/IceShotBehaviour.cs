using UnityEngine;

public class IceShotBehaviour : BaseShotBehaviour
{
    [SerializeField] private float freezeTime = 2;
    [SerializeField] private AudioClip freezeClip;
    [SerializeField] private Color32 frozenColor = new Color32(0, 166, 255, 255);
    
    private const float MaxLifetime = 10;
    
    private void Start()
    {
        Destroy(gameObject, MaxLifetime);
    }
    
    public override void AdditionalShotBehaviour(GameObject otherObject)
    {
        otherObject.GetComponent<EnemyBehaviour>().Freeze(freezeTime, freezeClip, frozenColor);
    }
}