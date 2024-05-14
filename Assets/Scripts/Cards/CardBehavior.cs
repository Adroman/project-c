using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Cards
{
    public class CardBehavior : MonoBehaviour
    {
        public int Price;
        public IntVariable Currency;
        public CardManager CardManager;

        public void Play()
        {
            OnPlay.Invoke();
            CardManager.PlayCard(this);
        }
        
        public UnityEvent OnPlay;
        public UnityEvent<CardManager> OnPurchase;
        
        public bool CanPurchase()
        {
            return Currency.IsAtLeast(Price);
        }

        public void Purchase(CardManager cardManager)
        {
            Currency.SubtractValue(Price);
            OnPurchase.Invoke(cardManager);
        }

        public void InstantiateToDiscardPile(CardManager cardManager)
        {
            cardManager.InstantiateCardToDiscardPile(this);
        }
    }
}