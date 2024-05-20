using System;
using Ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Cards
{
    public class CardPurchaseOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CardBehavior CardToInstantiate;
        public CardManager CardManager;
        public UnityEvent AfterPurchase;

        private Animator _animator;
        private bool _hasAnimator;
        private static readonly int ShopMouseOver = Animator.StringToHash("ShopMouseOver");
        private static readonly int Shake = Animator.StringToHash("Shake");

        public void Awake()
        {
            CardManager = FindObjectOfType<CardManager>();
            if (CardManager == null)
            {
                Debug.LogError("No card manager found");
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _hasAnimator = _animator != null;
        }

        public void Purchase()
        {
            if (!CardToInstantiate.CanPurchase())
            {
                Debug.Log("Not enough skills to purchase the card");
                UiErrorMessage.Instance.ShowMessage($"Not enough {CardToInstantiate.Currency.Name.ToLower()} to purchase the card.");
                _animator.SetTrigger(Shake);
                return;
            }
            CardToInstantiate.Purchase(CardManager);
            AfterPurchase.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(ShopMouseOver, true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hasAnimator)
            {
                _animator.SetBool(ShopMouseOver, false);
            }
        }
    }
}