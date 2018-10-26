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
        public PigGameLogic()
        {

        }
        public PigGameLogic(string name1, string name2, int score1, int score2, Boolean turn, int turnPoints, int lastRoll)
        {
            Player1Name = name1;
            Player2Name = name2;
            Player1Score = score1;
            Player2Score = score2;
            Player1Turn = turn;
            PointsForTurn = turnPoints;
            RollValue = lastRoll;
        }
        public Boolean Player1Turn = true;
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
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
            if (Player1Turn == true)
            {
                if (Player1Score + PointsForTurn <= 100)
                {
                    Player1Score += PointsForTurn;
                    Player1Turn = false;
                }
                else
                    Player1Turn = false;
                PointsForTurn = 0;
            }
            else
            {
                if(Player2Score + PointsForTurn <= 100)
                {
                    Player2Score += PointsForTurn;
                    Player1Turn = true;
                }
                else
                    Player1Turn = true;
                PointsForTurn = 0;
            }
            CheckForWinner();
        }
        public void GetPlayerNames(string name1, string name2)
        {
            Player1Name = name1;
            Player2Name = name2;
        }
        public string CheckForWinner()
        {
            if(Player1Turn == true)
            {
                if (Player1Score == 100)
                    return Player2Name + " wins the game";
                if (Player2Score == 100)
                    return Player2Name + " wins the game!";
                else
                    return "";
            }
            else
                return "";
        }
        public int SetDiceImage()
        {
            if (RollValue == 0)
                return Resource.Drawable.pig;
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