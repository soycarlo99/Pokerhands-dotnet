using System;
using System.Linq;

namespace CardGame
{
    public class CompareHands
    {

        public static Hand CheckHands(Hand hand1, Hand hand2)
        {
            var hands = new[] { hand1, hand2 };
            var methods = new Func<Hand, Hand>[] { IsRoyalFlush, IsStraightFlush, IsFourOfAKind, IsFullHouse, IsFlush, IsStraight, IsThreeOfAKind, IsTwoPair, IsPair };
            foreach (var method in methods)
            {
                foreach (var hand in hands)
                {
                    if (method(hand) != null)
                    {
                        return hand;
                    }
                    else
                    {
                        return CompareHighestCard(hand1, hand2);
                    }
                }
            }
            return null;
        }

        public static Hand CompareHighestCard(Hand hand1, Hand hand2)
        {
            var ranks = "23456789TJQKA";
            var sorted1 = hand1.Cards.OrderBy(c => ranks.IndexOf(c.Rank)).ToList();
            var sorted2 = hand2.Cards.OrderBy(c => ranks.IndexOf(c.Rank)).ToList();
            for (var i = 4; i >= 0; i--)
            {
                if (ranks.IndexOf(sorted1[i].Rank) > ranks.IndexOf(sorted2[i].Rank))
                {
                    return hand1;
                }
                if (ranks.IndexOf(sorted1[i].Rank) < ranks.IndexOf(sorted2[i].Rank))
                {
                    return hand2;
                }
            }
            return null;
        }

        public static Hand IsPair(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (hand.Cards.Count(c => c.Rank == card.Rank) == 2)
                {
                    return hand;
                }
            }
            return null;
        }
        
        public static Hand IsTwoPair(Hand hand)
        {
            var pairs = 0;
            foreach (var card in hand.Cards)
            {
                if (hand.Cards.Count(c => c.Rank == card.Rank) == 2)
                {
                    pairs++;
                }
            }
            return pairs == 2 ? hand : null;
        }

        public static Hand IsThreeOfAKind(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (hand.Cards.Count(c => c.Rank == card.Rank) == 3)
                {
                    return hand;
                }
            }
            return null;
        }

        public static Hand IsStraight(Hand hand)
        {
            var ranks = "23456789TJQKA";
            var sorted = hand.Cards.OrderBy(c => ranks.IndexOf(c.Rank)).ToList();
            for (var i = 0; i < sorted.Count - 1; i++)
            {
                if (ranks.IndexOf(sorted[i + 1].Rank) - ranks.IndexOf(sorted[i].Rank) != 1)
                {
                    return null;
                }
            }
            return hand;
        }

        public static Hand IsFlush(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (hand.Cards.Count(c => c.Suit == card.Suit) == 5)
                {
                    return hand;
                }
            }
            return null;
        }

        public static Hand IsFullHouse(Hand hand)
        {
            return IsPair(hand) != null && IsThreeOfAKind(hand) != null ? hand : null;
        }

        public static Hand IsFourOfAKind(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (hand.Cards.Count(c => c.Rank == card.Rank) == 4)
                {
                    return hand;
                }
            }
            return null;
        }

        public static Hand IsStraightFlush(Hand hand)
        {
            return IsStraight(hand) != null && IsFlush(hand) != null ? hand : null;
        }

        public static Hand IsRoyalFlush(Hand hand)
        {
            var ranks = "TJQKA";
            var sorted = hand.Cards.OrderBy(c => ranks.IndexOf(c.Rank)).ToList();
            for (var i = 0; i < sorted.Count; i++)
            {
                if (sorted[i].Rank != ranks[i])
                {
                    return null;
                }
            }
            return hand;
        }
    }
}
