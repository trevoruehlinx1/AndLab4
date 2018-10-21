using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigGame
{
    class PigGameLogic
    {
        public bool player1Turn = true;
        public string player1Name { get; set; }
        public string player2Name { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int PointsForTurn { get; set; }
        public int RollValue { get; set; }


        public string RollDie()
        {
            Random random = new Random();
            RollValue = random.Next(1, 6);
            if (RollValue == 1)
                PointsForTurn = 0;
            else
                PointsForTurn += RollValue;
            return PointsForTurn.ToString();
        }

        public void EndTurn()
        {
            if (player1Turn == true)
            {
                if (Player1Score + PointsForTurn <= 10)
                {
                    Player1Score += PointsForTurn;
                    player1Turn = false;
                }
                else
                    player1Turn = false;
                PointsForTurn = 0;
            }
            else
            {
                if(Player2Score + PointsForTurn <= 10)
                {
                    Player2Score += PointsForTurn;
                    player1Turn = true;
                }
                else
                    player1Turn = true;
                PointsForTurn = 0;
            }
            CheckForWinner();
        }
        public void GetPlayerNames(string name1, string name2)
        {
            player1Name = name1;
            player2Name = name2;
        }
        public string CheckForWinner()
        {
            if (Player1Score == 10)
                return player1Name + " wins the game";
            if (Player2Score == 10)
                return player2Name + " wins the game!";
            else
                return "";
        }
        public int SetDiceImage()
        {
            int[] diceImages = new int[6]
            {
                Resource.Drawable.Die1,
                Resource.Drawable.Die2,
                Resource.Drawable.Die3,
                Resource.Drawable.Die4,
                Resource.Drawable.Die5,
                Resource.Drawable.Die6
            };
            return diceImages[RollValue - 1];
        }
    }
}