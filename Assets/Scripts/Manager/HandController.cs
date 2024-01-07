using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public Transform discardTransform;
    public Transform exhaustTransform;
    public Transform drawTransform;
    public Transform enemyCardMoveToPos;
    public Transform playerCardMoveToPos;

    public List<CardBase> cardHandList;
    public List<EnemyCardBase> cardEnemyHandList;

    public List<Transform> cardHandPosList;
    public List<Transform> cardEnemyHandPosList;

    public CardBase cardPrefab;

    private GameManager GameManager => GameManager.Ins;
    private DeckManager DeckManager => DeckManager.Ins;
    private FxManager FxManager => FxManager.Ins;
    private AudioManager AudioManager => AudioManager.Ins;


    protected CombatManager CombatManager => CombatManager.Ins;

    

    private EnemyCardBase currentEnemyCardPlay;
    private CardBase currentPlayerCardPlay;


    int enemycardIndex = 0;


    public void AddCardToHand(CardData cardData)
    {
        CardBase newCard = Instantiate(GameManager.GameplayData.CardPrefab, drawTransform);
        newCard.SetCard(cardData);
        cardHandList.Add(newCard);
       // newCard.transform.position = cardHandPosList[cardHandList.Count-1].transform.position;
        StartCoroutine(MoveCardToHandRoutine(cardHandPosList[cardHandList.Count - 1].transform, newCard));

        //anim
    }

    public void AddCardToEnemyHand(CardData cardData)
    {
        EnemyCardBase newCard = Instantiate(GameManager.GameplayData.EnemyCardPrefab);
        newCard.SetCard(cardData);
        cardEnemyHandList.Add(newCard);
        newCard.transform.position = cardEnemyHandPosList[cardEnemyHandList.Count - 1].transform.position;
        newCard.transform.parent = cardEnemyHandPosList[cardEnemyHandList.Count - 1].transform;
    }

    public void RemoveCardFromEnemyHand()
    {
        foreach (EnemyCardBase card in cardEnemyHandList)
        {

            card.RemoveCard();
        }
        enemycardIndex = 0;
        cardEnemyHandList.Clear();
    }

    public void RemoveCardFromHand(CardBase card)
    {
        cardHandList.Remove(card);
    }

    public void MoveCardToPos(CardBase playercard)
    {
        currentPlayerCardPlay = playercard;
       
        currentEnemyCardPlay = cardEnemyHandList[enemycardIndex];
        playercard.transform.position = playerCardMoveToPos.position;
        StartCoroutine(MoveCardRoutine(enemycardIndex, playercard));
        enemycardIndex++;
    }

    public void MoveCardBack(CardBase playercard)
    {
        DeckManager.OnCardPlayed(playercard);
   

    }
    protected virtual IEnumerator MoveCardToHandRoutine(Transform parentPos, CardBase playercard)
    {
        var timer = 0f;
        Transform CachedTransform = playercard.transform;
        CachedTransform.SetParent(parentPos);


        var startPos = CachedTransform.transform.position;
        var endPos = Vector3.zero;


        float duration = 0.3f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            CachedTransform.localPosition = Vector3.Lerp(startPos, endPos, timer / duration);
  
            yield return null;
        }
 
    }

    protected virtual IEnumerator MoveCardRoutine(int cardIndex, CardBase playercard)
    {
        var timer = 0f;
        Transform CachedTransform = currentEnemyCardPlay.transform;
        Transform defaultTransform = CachedTransform.parent;
        CachedTransform.SetParent(enemyCardMoveToPos);

        bool isRotatedToHalf = false;
        var startPos = CachedTransform.transform.position;
        var endPos = Vector3.zero;

        var startScale = CachedTransform.transform.localScale;
        var endScale = new Vector3(1.3f, 1.5f, 1f);

        var startRot = CachedTransform.transform.localRotation;
        var endRot = Quaternion.Euler(0f, 180f, 0f);

        float duration = 0.3f; 

        while (timer < duration)
        {
            timer += Time.deltaTime;

            CachedTransform.localPosition = Vector3.Lerp(startPos, endPos, timer / duration);
            CachedTransform.localRotation = Quaternion.Lerp(startRot, endRot, timer / duration);
            CachedTransform.localScale = Vector3.Lerp(startScale, endScale, timer / duration);

            if (!isRotatedToHalf && Mathf.Abs(CachedTransform.localRotation.eulerAngles.y - 180f) < 0.1f)
            {
                isRotatedToHalf = true;
                cardEnemyHandList[cardIndex].FlipCard();
            }

            yield return null;
        }

        yield return new WaitForSeconds(2);
        MoveCardBack(playercard);
        StartCoroutine(MoveCardBackRoutine(currentEnemyCardPlay.transform, defaultTransform));
    }

    protected virtual IEnumerator MoveCardBackRoutine(Transform currentEnemyCard, Transform defaultParent)
    {
        var timer = 0f;
        currentEnemyCard.SetParent(defaultParent);

        var startPos = currentEnemyCard.transform.position;
        var endPos = Vector3.zero;

        var startScale = currentEnemyCard.transform.localScale;
        var endScale = new Vector3(0.7f, 0.8f, 1f);

        float duration = 0.5f; 

        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (currentEnemyCard == null)
            {
                break;
            }
            currentEnemyCard.localPosition = Vector3.Lerp(startPos, endPos, timer / duration);
            currentEnemyCard.localScale = Vector3.Lerp(startScale, endScale, timer / duration);

            yield return null;
        }
        GameManager.AlowClick(true);
        DoCardActions();
    }

    protected virtual IEnumerator MoveAllCardsToNextPosInHand(Transform parentPos, CardBase playercard)
    {
        var timer = 0f;
        Transform CachedTransform = playercard.transform;
        CachedTransform.SetParent(parentPos);


        var startPos = CachedTransform.transform.position;
        var endPos = Vector3.zero;


        float duration = 0.3f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            CachedTransform.localPosition = Vector3.Lerp(startPos, endPos, timer / duration);

            yield return null;
        }

    }

    private void DoCardActions()
    {
        var enemyActionFirst = currentEnemyCardPlay.GetCardActionFirst();
        var playerActionFirst = currentPlayerCardPlay.GetCardActionFirst();

        if (CheckBlockAttack(enemyActionFirst, playerActionFirst))
            return;
        if(CheckNegateCard(enemyActionFirst, playerActionFirst))
            return;
     
        currentPlayerCardPlay.DoAction(CombatManager.Player);
        currentEnemyCardPlay.DoAction(CombatManager.Enemy);
        CombatManager.DoEffectCardEachWhenCharacterDrawACardIfHave();
    }

   bool CheckBlockAttack(CardActionType enemyActionFirst, CardActionType playerActionFirst)
    {
        if (enemyActionFirst == CardActionType.Attack && playerActionFirst == CardActionType.Block)
        {
            FxManager.DisplayBuffEffect(CombatManager.Player.PositiveBuffPos, "Blocked!", false);
            CombatManager.DoEffectCardEachWhenCharacterDrawACardIfHave();
            AudioManager.PlayBlockSound();
            return true ;
        }
        if (enemyActionFirst == CardActionType.Block && playerActionFirst == CardActionType.Attack)
        {
            FxManager.DisplayBuffEffect(CombatManager.Enemy.PositiveBuffPos, "Blocked!", false);
            CombatManager.DoEffectCardEachWhenCharacterDrawACardIfHave();
            AudioManager.PlayBlockSound();

            return true;
        }
        return false ;
    }

    bool CheckNegateCard(CardActionType enemyActionFirst, CardActionType playerActionFirst)
    {
        if ((enemyActionFirst == CardActionType.NegateSpell && playerActionFirst != CardActionType.Attack)
        || (enemyActionFirst != CardActionType.Attack && playerActionFirst == CardActionType.NegateSpell))
        {
            return true;
        }
        return false;
    }
}
