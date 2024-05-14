using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    public class CardPurchaseOption : MonoBehaviour
    {
        public CardBehavior CardToInstantiate;
        public CardManager CardManager;
        public UnityEvent AfterPurchase;

        public void Awake()
        {
            CardManager = FindObjectOfType<CardManager>();
            if (CardManager == null)
            {
                Debug.LogError("No card manager found");
            }
        }

        public void Purchase()
        {
            if (!CardToInstantiate.CanPurchase())
            {
                Debug.Log("Not enough skills to purchase the card");
                return;
            }
            CardToInstantiate.Purchase(CardManager);
            AfterPurchase.Invoke();
        }
    }
}