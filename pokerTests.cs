using CardGame;
using Xunit;

namespace CardGame.Tests;

public class PokerHandTests
{
    [Fact]
    public void IfThreeOfAKindWorksWithAthreeOfAKindHand() //Here I basically wanted to try if the threeOfAKind fucntion actually work! (I don't know exactly what to name my functions)
    {
        // Arrange
        var threeOfAKindHand = new Hand(
            new List<Card>
            {
                new Card('♥', '8'),
                new Card('♦', '8'),
                new Card('♣', '8'),
                new Card('♠', 'K'),
                new Card('♥', 'Q'),
            }
        );
        // Act
        var (result, handType) = CompareHands.IsThreeOfAKind(threeOfAKindHand);

        // Assert
        Assert.Equal(threeOfAKindHand, result);
        Assert.Equal("Three of a Kind", handType);
    }

    [Fact]
    public void IsThreeOfAKindReturnsNull() //Here I wanted to test if I give a non-three-of-a-kind hand, does it work then.
    {
        var pairHand = new Hand(
            new List<Card>
            {
                new Card('♥', '7'),
                new Card('♦', '7'),
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsThreeOfAKind(pairHand);

        // Assert
        Assert.Null(result);
        Assert.Null(handType);
    }

    [Fact]
    public void IsPairHandWorkingAsItShould() //Here I wanted to test if I give a pairHand to IsPair method does it work then.
    {
        var pairHand = new Hand(
            new List<Card>
            {
                new Card('♥', '7'),
                new Card('♦', '7'),
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsPair(pairHand);

        // Assert
        Assert.Equal(pairHand, result);
        Assert.Equal("Pair", handType);
    }

    [Fact]
    public void IsPairHandWorkingAsItShouldWithNoPairHand() //Here I wanted to test if I give a non pairHand to IsPair method does it work then???
    {
        var NonPairHand = new Hand(
            new List<Card>
            {
                new Card('♥', '7'),
                new Card('♦', '8'),
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsPair(NonPairHand);

        // Assert
        Assert.Null(result);
        Assert.Null(handType);
    }

    [Fact]
    public void Does_a_compare_hand_method_works_with_invalid_hand()
    {
        var invalidHand = new Hand(
            new List<Card>
            {
                new Card('♥', '7'),
                new Card('C', '8'), //The invaild hand that I want to test
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsPair(invalidHand);

        // Assert
        Assert.Null(result);
        Assert.Null(handType);
    }

    [Fact]
    public void does_IsTwoPair_method_accept_an_invalid_hand()
    {
        var invalidHand = new Hand( //getting this hand is impossible because it has 2 exact same cards, but it has a pair too.
            new List<Card>
            {
                new Card('♥', '7'),
                new Card('♥', '7'),
                new Card('♣', '7'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsPair(invalidHand);

        // Assert
        Assert.Null(result);
        Assert.Null(handType);
    }

    [Fact]
    public void if_the_compare_hand_can_find_the_higer_hand_when_they_are_equal() //BUGGGGG FOUND 🐞!!!
    {
        // Arrange - Here I created two hands that are both pairs, but one of the cards has a higher rank
        var hand1 = new Hand(
            new List<Card>
            {
                new Card('♥', '5'),
                new Card('♦', '5'),
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        var hand2 = new Hand(
            new List<Card>
            {
                new Card('♥', 'A'),
                new Card('♦', 'A'),
                new Card('♣', '2'),
                new Card('♠', '3'),
                new Card('♥', '4'),
            }
        );

        // Act
        var resultwithHigherFirst = CompareHands.CheckHands(hand2, hand1); //if we change the place of the hands here, it will work, but that shouldn't be the winner factor of a hand. So basically if I write (higherPairHand, lowerPairHand) the result will be correct. The result is dependant on which card is being compared first.
        var resultWithLowerFirst = CompareHands.CheckHands(hand1, hand2);

        // Assert
        Assert.Equal(hand2, resultwithHigherFirst.winningHand); //Here both hands are pair, but it returns the lowerPairHand as the winnner
        Assert.Equal(hand2, resultWithLowerFirst.winningHand); //This fails, but actually this should be the winner hand, because the result is a pair hand but the lowercard wins
        Assert.Equal("Pair", resultwithHigherFirst.handType);
        Assert.Equal("Pair", resultWithLowerFirst.handType);
    }

    [Fact]
    public void if_the_compare_higest_method_can_compare_all_the_ranks_and_also_if_it_allows_invalid_ranks()
    {
        var lowerPairHand = new Hand( //I put two set of invalid hands to see if it fails
            new List<Card>
            {
                new Card('♥', '9'),
                new Card('♦', '9'),
                new Card('♣', '9'),
                new Card('♠', '9'),
                new Card('♥', '9'),
            }
        );

        var higherPairHand = new Hand(
            new List<Card>
            {
                new Card('♥', '9'),
                new Card('♦', '9'),
                new Card('♣', '9'),
                new Card('♠', '9'),
                new Card('♥', '9'),
            }
        );

        // Act
        var result = CompareHands.CompareHighestCard(lowerPairHand, higherPairHand);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void if_a_impossible_card_pass_IsStright_method()
    {
        // Arrange
        var impossibleHand = new Hand( //Here is an impossible hand that will not pass, I wanted to test if these CompareHands.cs methods
            new List<Card>
            {
                new Card('♥', '9'),
                new Card('♦', '9'),
                new Card('♣', '9'),
                new Card('♠', '9'),
                new Card('♥', '9'),
            }
        );

        // Act
        var (result, handType) = CompareHands.IsStraight(impossibleHand);

        // Assert
        Assert.Null(result);
        Assert.Null(handType);
    }

    [Fact]
    public void TESTWITHMAX()
    {
        var LowerPairHand = new Hand(
            new List<Card>
            {
                new Card('♥', '5'),
                new Card('♦', '5'),
                new Card('♣', 'K'),
                new Card('♠', 'Q'),
                new Card('♥', 'J'),
            }
        );

        var HigherPairHand = new Hand(
            new List<Card>
            {
                new Card('♥', 'A'),
                new Card('♦', 'A'),
                new Card('♣', '2'),
                new Card('♠', '3'),
                new Card('♥', '4'),
            }
        );

        // Act
        var (winningHand, handType) = CompareHands.CheckHands(HigherPairHand, LowerPairHand);

        // Assert
        Assert.True(winningHand == HigherPairHand);
    }
}
