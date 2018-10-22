using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        PigGameLogic gameLogic;
        string player1Name;
        string player2Name;
        int player1Score;
        int player2Score;
        int pointsForTurn;
        int lastRollValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            var rollButton = FindViewById<Button>(Resource.Id.rollButton);
            var newPlayersButton = FindViewById<Button>(Resource.Id.newPlayersPuttonButton);
            var endTurnButton = FindViewById<Button>(Resource.Id.endTurnButton);
            var nameEditText1 = FindViewById<EditText>(Resource.Id.textInputEditText1);
            var nameEditText2 = FindViewById<EditText>(Resource.Id.textInputEditText2);
            var score1Label = FindViewById<TextView>(Resource.Id.scoreLabel1);
            var score2Label = FindViewById<TextView>(Resource.Id.scoreLabel2);
            var scoreTextView1 = FindViewById<TextView>(Resource.Id.scoreTextView1);
            var scoreTextView2 = FindViewById<TextView>(Resource.Id.scoreTextView2);
            var whosTurnLabel = FindViewById<TextView>(Resource.Id.whosTurnLabel);
            var pointsForTurnTextView = FindViewById<TextView>(Resource.Id.pointsForTurnTextView);
            var diceImageView = FindViewById<ImageView>(Resource.Id.diceImageView);

            if (savedInstanceState != null)
            {
                player1Name = savedInstanceState.GetString("player1Name");
                player2Name = savedInstanceState.GetString("player2Name");
                player1Score = savedInstanceState.GetInt("player1Score");
                player2Score = savedInstanceState.GetInt("player2Score");
                Boolean player1Turn = savedInstanceState.GetBoolean("player1Turn");
                pointsForTurn = savedInstanceState.GetInt("pointsForTurn");
                lastRollValue = savedInstanceState.GetInt("lastRollValue");
                gameLogic = new PigGameLogic(player1Name, player2Name, player1Score, player2Score, player1Turn, pointsForTurn, lastRollValue);


                score1Label.Text = gameLogic.Player1Name + "'s Score";
                score2Label.Text = gameLogic.Player2Name + "'s Score";
                scoreTextView1.Text = gameLogic.Player1Score.ToString();
                scoreTextView2.Text = gameLogic.Player2Score.ToString();
                pointsForTurnTextView.Text = gameLogic.PointsForTurn.ToString();
                diceImageView.SetImageResource(gameLogic.SetDiceImage());

                if (gameLogic.Player1Turn == true)
                    whosTurnLabel.Text = gameLogic.Player1Name + "'s Turn";
                else
                    whosTurnLabel.Text = gameLogic.Player2Name + "'s Turn";

                nameEditText1.Enabled = false;
                nameEditText2.Enabled = false;

            }
            else
            {
                gameLogic = new PigGameLogic();
                player1Name = "";
                player2Name = "";
                player1Score = 0;
                player2Score = 0;
                rollButton.Enabled = false;
                endTurnButton.Enabled = false;
                diceImageView.SetImageResource(Resource.Drawable.pig);
            }
            newGameButton.Click += (sender, e) =>
            {
                diceImageView.SetImageResource(Resource.Drawable.pig);
                gameLogic = new PigGameLogic();
                if (nameEditText1.Enabled == true && nameEditText1.Text == ""
                    || nameEditText2.Text == "" && nameEditText2.Enabled == true)
                {
                    whosTurnLabel.Text = "Enter a name for each of the players";
                }

                else
                {
                    rollButton.Enabled = true;
                    endTurnButton.Enabled = true;
                    player1Name = nameEditText1.Text;
                    player2Name = nameEditText2.Text;
                    gameLogic.GetPlayerNames(player1Name, player2Name);
                    score1Label.Text = player1Name + "'s Score";
                    score2Label.Text = player2Name + "'s Score";
                    scoreTextView1.Text = gameLogic.Player1Score.ToString();
                    scoreTextView2.Text = gameLogic.Player2Score.ToString();
                    whosTurnLabel.Text = player1Name + "'s Turn";
                    pointsForTurnTextView.Text = "0";

                    nameEditText1.Enabled = false;
                    nameEditText2.Enabled = false;
                }
            };
            rollButton.Click += (sender, e) =>
            {
                string roll = gameLogic.RollDie();
                if (roll != "0")
                    pointsForTurnTextView.Text = roll;
                else
                {
                    rollButton.Enabled = false;
                    whosTurnLabel.Text = "You rolled a 1";
                    pointsForTurnTextView.Text = roll;
                }
                diceImageView.SetImageResource(gameLogic.SetDiceImage());
            };
            endTurnButton.Click += (sender, e) =>
            {
                gameLogic.EndTurn();
                rollButton.Enabled = true;
                if (gameLogic.CheckForWinner() != "")
                {
                    whosTurnLabel.Text = gameLogic.CheckForWinner();
                    rollButton.Enabled = false;
                    endTurnButton.Enabled = false;
                }
                else
                {
                    scoreTextView1.Text = gameLogic.Player1Score.ToString();
                    scoreTextView2.Text = gameLogic.Player2Score.ToString();
                    pointsForTurnTextView.Text = gameLogic.PointsForTurn.ToString();
                    if (gameLogic.Player1Turn == false)
                        whosTurnLabel.Text = gameLogic.Player2Name + "'s Turn";
                    else
                        whosTurnLabel.Text = gameLogic.Player1Name + "'s Turn";
                }
            };
            newPlayersButton.Click += (sender, e) =>
            {
                nameEditText1.Enabled = true;
                nameEditText2.Enabled = true;
                nameEditText1.Text = "";
                nameEditText2.Text = "";
                score1Label.Text = "";
                score2Label.Text = "";
                scoreTextView1.Text = "";
                scoreTextView2.Text = "";
                rollButton.Enabled = false;
                endTurnButton.Enabled = false;
                whosTurnLabel.Text = "Enter a name for each of the players";
            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            //StringWriter writer = new StringWriter();
            //XmlSerializer pigLogicSerializer = new XmlSerializer(typeof(PigGameLogic));
            //pigLogicSerializer.Serialize(writer, gameLogic );
            //string xmlPigGameLogic = writer.ToString();
            //outState.PutString("savedPageValues", xmlPigGameLogic);

            outState.PutString("player1Name", gameLogic.Player1Name);
            outState.PutString("player2Name", gameLogic.Player2Name);
            outState.PutInt("player1Score", gameLogic.Player1Score);
            outState.PutInt("player2Score", gameLogic.Player2Score);
            outState.PutBoolean("player1Turn", gameLogic.Player1Turn);
            outState.PutInt("pointsForTurn", gameLogic.PointsForTurn);
            outState.PutInt("diceImage", gameLogic.SetDiceImage());
            outState.PutInt("lastRollValue", gameLogic.RollValue);

            base.OnSaveInstanceState(outState);
        }
    }
}

