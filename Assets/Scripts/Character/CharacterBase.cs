using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterBase : MonoBehaviour
{
    public CharacterStats CharacterStats { get; protected set; }
    [SerializeField] protected CharacterCanvas characterCanvas;
    [SerializeField] protected Transform positiveBuffPos;
    [SerializeField] protected Transform negativeBuffPos;

    protected CombatManager CombatManager => CombatManager.Ins;

    protected GameManager GameManager => GameManager.Ins;

    protected AudioManager AudioManager => AudioManager.Ins;


    public CharacterCanvas CharacterCanvas => characterCanvas;

    public Transform PositiveBuffPos => positiveBuffPos;
    public Transform NegativeBuffPos => negativeBuffPos;    

    float duration = 0.2f;
    float magnitude = 0.1f;
    public virtual void Awake()
    {
    }

    public virtual void BuildCharacter()
    {

    }

    protected virtual void OnDeath()
    {
        GameManager.AlowClick(false);
    }

    public CharacterBase GetCharacterBase()
    {
        return this;
    }

    public void VibrateTarget()
    {
        StartCoroutine(Vibrate(transform));
    }


    IEnumerator Vibrate(Transform target)
    {
        Vector3 originalPosition = target.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;
            float z = originalPosition.z;

            target.position = new Vector3(x, y, z);

            elapsed += Time.deltaTime;
            yield return null;
        }


        target.position = originalPosition;
    }
}
