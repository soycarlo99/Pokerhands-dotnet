using Xunit;
using CardGame;

namespace Pokerhands;

public class TestClass
{

    [Fact]
    public void TestMethodSameHandLowerFirst()
    {
        // Arrange
        var lowerHand = new Hand(
        [
            new Card('♣', '5'),
            new Card('♠', '5'),
            new Card('♠', '5'),
            new Card('♣', 'K'),
            new Card('♠', 'K')
        ]);

        var higherHand = new Hand(
        [
            new Card('♠', 'A'),
            new Card('♣', 'A'),
            new Card('♠', 'A'),
            new Card('♠', 'K'),
            new Card('♣', 'K')
        ]);

        // Act

        var (winningHand, handType) = CompareHands.CheckHands(lowerHand, higherHand);

        // Assert

        Assert.True(winningHand == higherHand);
        Assert.Equal("Full House", handType);
    }

    [Fact]
    public void TestMethodSameHandHigherFirst()
    {
        // Arrange
        var lowerHand = new Hand(
        [
            new Card('♣', '5'),
            new Card('♠', '5'),
            new Card('♠', '5'),
            new Card('♣', 'K'),
            new Card('♠', 'K')
        ]);

        var higherHand = new Hand(
        [
            new Card('♠', 'A'),
            new Card('♣', 'A'),
            new Card('♠', 'A'),
            new Card('♠', 'K'),
            new Card('♣', 'K')
        ]);

        // Act

        var (winningHand, handType) = CompareHands.CheckHands(higherHand, lowerHand);

        // Assert

        Assert.True(winningHand == higherHand);
        Assert.Equal("Full House", handType);
    }

    [Fact]
    public void TestMethodIsPair()
    {
        // Arrange
        var pairHand = new Hand(
        [
            new Card('♣', '5'),
            new Card('♠', '5'),
            new Card('♠', '4'),
            new Card('♣', '7'),
            new Card('♠', '9')
        ]);


        // Act

        var (hand1Result, handType) = CompareHands.IsPair(pairHand);

        // Assert
        // Example of types of asserts
        Assert.NotNull(hand1Result);
        Assert.NotNull(handType);
        Assert.True(hand1Result == pairHand);
        Assert.Equal("Pair", handType);
    }

}