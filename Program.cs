// Pokerhands Program for unit testing purposes

using System;
using CardGame;

    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                // Create a new deck of cards
                DeckOfCards deck = new DeckOfCards();

                // Deal two hands
                Hand hand1 = deck.DealHand();
                Hand hand2 = deck.DealHand();

                // Compare hands to determine the winner
                Hand winner = CompareHands.CheckHands(hand1, hand2);

                // Print the result
                if (winner == null)
                {
                    Console.WriteLine("It's a tie!");
                    Console.WriteLine(hand1); Console.WriteLine(hand2);
                }
                else
                {
                    Console.WriteLine($"Hand {(winner == hand1 ? "1" : "2")} wins!");
                    
                    Console.WriteLine(winner);
                }
                Console.WriteLine("Press Enter to play again, or any other key to exit.");
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
    }




