using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class CardBase : MonoBehaviour
{
    [Header("Base References")]
    [SerializeField] protected Image cardImage;
    [SerializeField] protected Image cardBG;
    [SerializeField] private Canvas canvas;

    [SerializeField] protected TextMeshProUGUI nameTextField;
    [SerializeField] protected TextMeshProUGUI descTextField;
    [SerializeField] protected TextMeshProUGUI manaTextField;
    [SerializeField] protected List<GameObject> rarityRootList;
    private Vector3 defaultScale = new Vector3(1.3f, 1.5f, 1);


    private bool isDragging = false;
    private Vector3 offset;
    public CardData CardData { get; protected set; }
    public bool IsInactive { get; protected set; }
    public bool IsExhausted { get; private set; }
    public bool IsPlayable { get; protected set; } = true;


    protected Transform CachedTransform { get; set; }
    protected WaitForEndOfFrame CachedWaitFrame { get; set; }

    protected Camera mainCam;

    public List<GameObject> RarityRootList => rarityRootList;

    protected GameManager GameManager => GameManager.Ins;
    protected DeckManager DeckManager => DeckManager.Ins;

    protected CombatManager CombatManager => CombatManager.Ins;

    private AudioManager AudioManager => AudioManager.Ins;

    protected virtual void Awake()
    {
        CachedTransform = transform;
        CachedWaitFrame = new WaitForEndOfFrame();
        mainCam = Camera.main;
    }

    public virtual void SetCard(CardData targetProfile, bool isPlayable = true)
    {
        CardData = targetProfile;
        IsPlayable = isPlayable;
        nameTextField.text = CardData.CardName;
        descTextField.text = CardData.CardDescription;
        manaTextField.text = CardData.ManaCost.ToString();
        cardImage.sprite = CardData.CardSprite;
        cardBG.sprite = CardData.CardBGSprite;
       // foreach (var rarityRoot in RarityRootList)
        //    rarityRoot.gameObject.SetActive(rarityRoot.Rarity == CardData.Rarity);
    }

    private void Use()
    {
        if (!IsPlayable) return;
        SpendMana(CardData.ManaCost);
        DeckManager.MoveCard(this);
  

    }

    public void DoAction(CharacterBase selfTarget)
    {
        foreach (var playerAction in CardData.CardActionDataList)
        {
            CardActionBase attackAction = ActionFactory.GetAction(playerAction.CardActionType);
            attackAction.DoAction(DetermineTargets(playerAction), playerAction.ActionValue, selfTarget);
        }
    }

    private CharacterBase DetermineTargets(CardActionData playerAction)
    {
        CharacterBase target;
        switch (playerAction.ActionTargetType)
        {
            case ActionTargetType.Enemy:
                target = CombatManager.Enemy;
                break;
            case ActionTargetType.Player:
                target = CombatManager.Player;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return target;
    }

    private void SpendMana(int value)
    {
        if (!IsPlayable) return;
        GameManager.InitGameplayData.CurrentMana -= value;
    }


    public void Discard()
    {
        if (IsExhausted) return;
        if (!IsPlayable) return;
        DeckManager.OnCardDiscarded(this);
        StartCoroutine(DiscardRoutine());
    }

    public void Exhaust(bool destroy = true)
    {
        if (IsExhausted) return;
        if (!IsPlayable) return;
        IsExhausted = true;
        DeckManager.OnCardExhausted(this);
        StartCoroutine(ExhaustRoutine(destroy));
    }

    public virtual void UpdateCardText()
    {
 
        nameTextField.text = CardData.CardName;
        descTextField.text = CardData.MyDescription;
        manaTextField.text = CardData.ManaCost.ToString();
    }


    public CardActionType GetCardActionFirst()
    {
        return CardData.CardActionDataList[0].CardActionType;
    }

    private IEnumerator DiscardRoutine(bool destroy = true)
    {
        var timer = 0f;
        transform.SetParent(DeckManager.HandController.discardTransform);

        var startPos = CachedTransform.localPosition;
        var endPos = Vector3.zero;

        var startScale = CachedTransform.localScale;
        var endScale = Vector3.zero;

        var startRot = CachedTransform.localRotation;
        var endRot = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);

        while (true)
        {
            timer += Time.deltaTime * 5;

            CachedTransform.localPosition = Vector3.Lerp(startPos, endPos, timer);
            CachedTransform.localRotation = Quaternion.Lerp(startRot, endRot, timer);
            CachedTransform.localScale = Vector3.Lerp(startScale, endScale, timer);

            if (timer >= 1f) break;

            yield return CachedWaitFrame;
        }

        if (destroy)
            Destroy(gameObject);

    }

    private IEnumerator ExhaustRoutine(bool destroy = true)
    {
        var timer = 0f;
        transform.SetParent(DeckManager.HandController.exhaustTransform);

        var startPos = CachedTransform.localPosition;
        var endPos = Vector3.zero;

        var startScale = CachedTransform.localScale;
        var endScale = Vector3.zero;

        var startRot = CachedTransform.localRotation;
        var endRot = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);

        while (true)
        {
            timer += Time.deltaTime * 5;

            CachedTransform.localPosition = Vector3.Lerp(startPos, endPos, timer);
            CachedTransform.localRotation = Quaternion.Lerp(startRot, endRot, timer);
            CachedTransform.localScale = Vector3.Lerp(startScale, endScale, timer);

            if (timer >= 1f) break;

            yield return CachedWaitFrame;
        }

        if (destroy)
            Destroy(gameObject);

    }


 

    private void OnMouseDown()
    {
        if (!GameManager.AllowClick) return;
        if (!isDragging)
        {
            isDragging = true;
            CachedTransform.localScale = new Vector3(1.6f, 1.8f, 2);
            canvas.sortingOrder = 1;
            offset = CachedTransform.position - mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseDrag()
    {
        if (!GameManager.AllowClick) return;

        if (isDragging)
        {
            Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition) + offset;
            CachedTransform.position = new Vector3(newPosition.x, newPosition.y, CachedTransform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (!GameManager.AllowClick) return;
        if (CachedTransform.localPosition.y < 2.5f)
        {
            CachedTransform.localPosition = Vector3.zero;
            CachedTransform.localScale = defaultScale ;
            canvas.sortingOrder = 0;
        }
        else
        {
            GameManager.AlowClick(false);
            CachedTransform.localScale = defaultScale;
            AudioManager.PlayCardSound();
            Use();
        }
        isDragging = false;
    }
}
