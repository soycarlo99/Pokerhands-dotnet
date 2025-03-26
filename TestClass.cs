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

}