using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Variables;

namespace Cards
{
    public class CardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int Price;
        public IntVariable Currency;
        public CardManager CardManager;
        
        public UnityEvent OnPlay;
        public UnityEvent<CardManager> OnPurchase;
        
        private Animator _animator;
        private bool _hasAnimator;
        private static readonly int MouseOver = Animator.StringToHash("MouseOver");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _hasAnimator = _animator != null;
        }

        public void Play()
        {
            OnPlay.Invoke();
            CardManager.PlayCard(this);
        }
        
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(MouseOver, true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(MouseOver, false);
            }
        }
    }
}