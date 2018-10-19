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
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int PointsForTurn { get; set; }
        public int RollValue { get; set; }


        public string RollDie()
        {
            Random random = new Random();
            RollValue = random.Next(1, 6);

            PointsForTurn += RollValue;
            return PointsForTurn.ToString();
        }

        public void EndTurn()
        {
            if (player1Turn == true)
            {
                Player1Score += PointsForTurn;
                player1Turn = false;
                PointsForTurn = 0;
            }
            else
            {
                Player2Score += PointsForTurn;
                player1Turn = true;
                PointsForTurn = 0;
            }
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