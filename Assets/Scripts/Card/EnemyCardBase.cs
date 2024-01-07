using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCardBase : CardBase
{
    [SerializeField] Image cardback;
    public override void SetCard(CardData targetProfile, bool isPlayable = true)
    {
        CardData = targetProfile;
        IsPlayable = isPlayable;
        nameTextField.text = CardData.CardName;
        descTextField.text = CardData.CardDescription;
        cardImage.sprite = CardData.CardSprite;
        // foreach (var rarityRoot in RarityRootList)
        //    rarityRoot.gameObject.SetActive(rarityRoot.Rarity == CardData.Rarity);
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }

    public void FlipCard()
    {
        cardback.gameObject.SetActive(false);
    }
}
