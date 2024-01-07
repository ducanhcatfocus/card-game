using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : Singleton<FxManager>
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] DmgFx dmgFxPrefab;
    [SerializeField] PopUpFX popupFxPrefab;

    [SerializeField] Transform enemyHitTransform;

    private ParticleSystem particleSystemInstance;
    private DmgFx dmgFxInstance;
    private PopUpFX popupFxInstance;
    public void  PlayHitFx(Transform playPos)
    {     
         particleSystemInstance = Instantiate(hitParticlePrefab, playPos.position, Quaternion.identity, transform);
         particleSystemInstance.Play();
    }

    public void DisplayFloatingDamage(Transform displayPos, int dmg)
    {
        float randX = Random.Range(displayPos.position.x - 1, displayPos.position.x + 1);
        float randY = Random.Range(displayPos.position.y , displayPos.position.y +2);
        dmgFxInstance = Instantiate(dmgFxPrefab, new Vector3(randX, randY), Quaternion.identity, transform);
        dmgFxInstance.Init(dmg);
    }


    public void DisplayBuffEffect(Transform displayPos, string text, bool isNegative)
    {
        popupFxInstance = Instantiate(popupFxPrefab, displayPos.position, Quaternion.identity, transform);
        popupFxInstance.Init(text, isNegative);
    }


    public void DisplayApplyEffect()
    {

    }



    public void StopParticleSystem()
    {
        if (particleSystemInstance != null)
        {
            particleSystemInstance.Stop();
        }
    }
}
