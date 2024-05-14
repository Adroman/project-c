using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace Cards
{
    public class CardManager : MonoBehaviour
    {
        public List<CardBehavior> Deck;
        public List<CardBehavior> DiscardPile;
        public List<CardBehavior> Hand;
        public List<CardBehavior> PlayArea;

        public IntVariable Skills;
        public IntVariable Mana;

        public Button EndTurnButton;
        public GameObject TowerUi;
        public GameObject CardShop;
        public GameObject NextWaveButton;

        public Transform CardsParent;
        public CardBehavior BasicSkillPrefab;
        public int BasicSkillAmount;
        public CardBehavior CursePrefab;
        public int CurseAmount;
        public CardBehavior GoldCardPrefab;
        public int GoldCardAmount;
        public CardBehavior GoldAndSkillPrefab;
        public int GoldAndSkillAmount;

        private void Awake()
        {
            //Random.InitState(Environment.TickCount);
            Initialize();
            DrawCards(5);
        }

        private void Initialize()
        {
            for (int i = 0; i < BasicSkillAmount; i++)
                InstantiateCardToDiscardPile(BasicSkillPrefab);
            
            for (int i = 0; i < CurseAmount; i++)
                InstantiateCardToDiscardPile(CursePrefab);
            
            for (int i = 0; i < GoldCardAmount; i++)
                InstantiateCardToDiscardPile(GoldCardPrefab);
            
            for (int i = 0; i < GoldAndSkillAmount; i++)
                InstantiateCardToDiscardPile(GoldAndSkillPrefab);
        }

        public CardBehavior InstantiateCardToDiscardPile(CardBehavior cardPrefab)
        {
            var card = Instantiate(cardPrefab, CardsParent);
            card.CardManager = this;
            card.gameObject.SetActive(false);
            DiscardPile.Add(card);
            return card;
        }

        public void DrawCards(int amount)
        {
            while (amount > 0 && Deck.Count + DiscardPile.Count > 0)
            {
                if (Deck.Count == 0)
                {
                    ShuffleDiscardIntoDeck();
                }

                var card = TakeCard(Deck, Deck.Count - 1);
                Hand.Add(card);
                card.gameObject.SetActive(true);
                
                amount--;
            }
        }

        private void ShuffleDiscardIntoDeck()
        {
            while (DiscardPile.Count > 0)
            {
                int i = Random.Range(0, DiscardPile.Count);
                var card = TakeCard(DiscardPile, i);
                Deck.Add(card);
            }
        }

        private static CardBehavior TakeCard(IList<CardBehavior> fromDeck, int index)
        {
            var card = fromDeck[index];
            fromDeck.RemoveAt(index);
            return card;
        }

        private static void MoveDeckToAnotherDeck(ICollection<CardBehavior> fromDeck, ICollection<CardBehavior> toDeck)
        {
            foreach (var card in fromDeck)
            {
                toDeck.Add(card);
            }
            
            fromDeck.Clear();
        }

        public void EndTurn()
        {
            EndTurnButton.gameObject.SetActive(false);
            Skills.Value = 0;
            Mana.Value = 0;
            MoveDeckToAnotherDeck(Hand, DiscardPile);
            MoveDeckToAnotherDeck(PlayArea, DiscardPile);
            DrawCards(5);
            TowerUi.SetActive(true);
            NextWaveButton.SetActive(true);
            CardShop.SetActive(false);
            gameObject.SetActive(false);
        }

        public void PlayCard(CardBehavior card)
        {
            int index = Hand.IndexOf(card);
            if (index == -1)
            {
                Debug.LogError("Invalid Card");
                return;
            }
            
            Hand.RemoveAt(index);
            PlayArea.Add(card);
            card.gameObject.SetActive(false);

            if (Hand.Count == 0)
            {
                EndTurnButton.gameObject.SetActive(true);
            }
        }
    }
}