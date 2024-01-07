using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : UICanvas
{
    [SerializeField] private UICardBase choiceCardUIPrefab;
    [SerializeField] private CardBase cardGoldPrefab;
    [SerializeField] private RewardContainerData rewardContainerData;
    [SerializeField] private CardData cardGoldData;        
    [SerializeField] Transform cardParent;
    private List<CardData> cardRewardList = new List<CardData>();

    bool isClaimReward = false;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.AlowClick(false);
        BuildReward();
    }

    private void BuildReward()
    {
        cardRewardList = rewardContainerData.GetRandomCardRewardList();
        UICardBase rewardCard = Instantiate(choiceCardUIPrefab, cardParent);
        rewardCard.SetCard(cardGoldData);
        rewardCard.RewardButton.onClick.AddListener(() => GetGoldReward(Random.Range(50, 100)));
        StartCoroutine(DisplayCardRewardRoutine());
    }


    public void SkipButtton()
    {
        SceneManager.LoadScene(1);  
    }


    private void GetCardReward(CardData cardData)
    {
        if (isClaimReward)
            return;
        isClaimReward = true;
        GameManager.Ins.InitGameplayData.CurrentCardsList.Add(cardData);
        GameManager.Ins.InitGameplayData.SaveData();
        SceneManager.LoadScene(1);
      
    }

    private void GetGoldReward(int amount)
    {
        if (isClaimReward)
            return;
        isClaimReward = true;
        GameManager.Ins.InitGameplayData.CurrentGold += amount;
        GameManager.Ins.InitGameplayData.SaveData();
        SceneManager.LoadScene(1);
   
    }

    private IEnumerator DisplayCardRewardRoutine()
    {
        Vector3 defaultScale = Vector3.zero;
        Vector3 finalScale = Vector3.one;
        float duration = 0.5f;


        foreach (CardData item in cardRewardList)
        {
            float timer = 0f;
            UICardBase rewardCard = Instantiate(choiceCardUIPrefab, cardParent);
            rewardCard.SetCard(item);
      

            while (timer < duration)
            {
                rewardCard.transform.localScale = Vector3.Lerp(defaultScale, finalScale, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            rewardCard.transform.localScale = finalScale;
            rewardCard.RewardButton.onClick.AddListener(() => GetCardReward(item));

            yield return null; 
        }
    }

    private IEnumerator CardMoveToDeckRoutine()
    {
        Vector3 defaultScale = Vector3.zero;
        Vector3 finalScale = Vector3.one;
        float duration = 0.5f;

        foreach (CardData item in cardRewardList)
        {
            float timer = 0f;
            UICardBase rewardCard = Instantiate(choiceCardUIPrefab, cardParent);
            rewardCard.SetCard(item);


            while (timer < duration)
            {
                rewardCard.transform.localScale = Vector3.Lerp(defaultScale, finalScale, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            rewardCard.transform.localScale = finalScale;
            rewardCard.RewardButton.onClick.AddListener(() => GetCardReward(item));

            yield return null;
        }
    }
}
