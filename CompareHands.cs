using System;
using System.Linq;

namespace CardGame
{
    public class CompareHands
    {
        public static Hand CompareHighestCard(Hand hand1, Hand hand2)
        {
            // Get the highest cards from each hand
            Card highestCardHand1 = hand1.Cards.OrderByDescending(card => card.Rank).First();
            Card highestCardHand2 = hand2.Cards.OrderByDescending(card => card.Rank).First();

            // Compare the ranks of the highest cards
            int comparison = highestCardHand1.Rank.CompareTo(highestCardHand2.Rank);

            if (comparison == 0)
            {  
                // If ranks are equal, return null to indicate a tie
                return null;
            }
            else if (comparison > 0)
            {
                // If the rank of the highest card in hand1 is greater, hand1 wins
                return hand1;
            }
            else
            {
                // If the rank of the highest card in hand2 is greater, hand2 wins
                return hand2;
            }
        }
        public static Hand CompareOnePair(Hand hand1, Hand hand2)
        {
            bool hasOnePair1 = HasOnePair(hand1);
            bool hasOnePair2 = HasOnePair(hand2);

            if (hasOnePair1 && !hasOnePair2)
            {
                return hand1; // Hand 1 has one pair but Hand 2 does not
            }
            else if (!hasOnePair1 && hasOnePair2)
            {
                return hand2; // Hand 2 has one pair but Hand 1 does not
            }
            else if (hasOnePair1 && hasOnePair2)
            {
                // Both hands have one pair, compare the ranks of the pairs
                Card pair1 = GetPair(hand1);
                Card pair2 = GetPair(hand2);

                int comparison = pair1.Rank.CompareTo(pair2.Rank);

                if (comparison == 0)
                {
                    // If ranks of pairs are equal, compare highest non-pair card
                    Hand remainingHand1 = RemovePair(hand1);
                    Hand remainingHand2 = RemovePair(hand2);
                    return CompareHighestCard(remainingHand1, remainingHand2);
                }
                else if (comparison > 0)
                {
                    return hand1; // Pair in Hand 1 has higher rank
                }
                else
                {
                    return hand2; // Pair in Hand 2 has higher rank
                }
            }
            else
            {
                return null; // Neither hand has one pair
            }
        }

        private static bool HasOnePair(Hand hand)
        {
            var groups = hand.Cards.GroupBy(card => card.Rank);
            return groups.Any(group => group.Count() == 2);
        }

        private static Card GetPair(Hand hand)
        {
            var groups = hand.Cards.GroupBy(card => card.Rank);
            var pairGroup = groups.FirstOrDefault(group => group.Count() == 2);

            if (pairGroup != null)
            {
                // Select the first card from the group representing the pair
                return pairGroup.First();
            }

            return null; // No pair found
        }

        private static Hand RemovePair(Hand hand)
        {
            var groups = hand.Cards.GroupBy(card => card.Rank);
            var nonPairCards = groups.Where(group => group.Count() != 2).SelectMany(group => group).ToArray();
            return new Hand(nonPairCards.ToList());
        }

    }
}
