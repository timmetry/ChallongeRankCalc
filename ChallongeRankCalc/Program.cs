using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChallongeRankCalc
{
    public class Player
    {
        protected string nick;
        protected string sponsor;
        protected string name;
        public string Tag { get { return sponsor + nick; } }
        public string Name { get { return name; } }

        protected int score;
        protected int totalMatches;
        protected int totalVictories;
        public int Score { get { return score; } }
        public int TotalMatches { get { return totalMatches; } }
        public int TotalVictories { get { return totalVictories; } }
        public float WinPercentage { get { return 100.0f * totalVictories / totalMatches; } }

        protected List<string> aliasList;
        public bool HasAlias(string alias)
        { return aliasList.Contains(alias); }
        public void AddAlias(string alias)
        {
            if (HasAlias(alias)) return;    // make sure there is no duplicate alias added
            else aliasList.Add(alias);
        }

        public Player(string tag)
        {
            nick = tag;
            sponsor = "";
            name = "Anonymous";

            score = 1000;
            totalMatches = 0;
            totalVictories = 0;

            aliasList = new List<string>();
            aliasList.Add(tag);
        }

        public void ChangeTag(string gamerTag, string sponsor)
        { this.nick = gamerTag; this.sponsor = sponsor; }
        public void ChangeTag(string gamerTag)
        { ChangeTag(gamerTag, ""); }

        public void Defeated(Player loser)
        {
            // increment the basic match stats for each player
            ++this.totalMatches;
            ++loser.totalMatches;
            ++this.totalVictories;

            // get the score ratio between the winner and loser
            float ratio = (float)this.score / (float)loser.score;

            // set minimum and maximum constants
            float maxRatio = 2.0f;  // the maximum score ratio, which will cause the minimum point gain and loss
            float avgRatio = 1.0f;  // the average score ratio, meaning the two players each have equal points
            float minRatio = 0.5f;  // the minimum score ratio, which will cause the maximum point gain and loss
            int maxGain = (int)(loser.score * 0.2f);    // the maximum amount of points that the winner can gain
            int maxLoss = (int)(this.score * 0.1f);     // the maximum amount of points that the loser can lose
            int minGain = 2;                            // the minimum amount of points that the winner can gain
            int minLoss = 1;                            // the minimum amount of points that the loser can lose

            // make sure that by some fluke the max's aren't less than the min's
            if (maxGain < minGain) maxGain = minGain;
            if (maxLoss < minLoss) maxLoss = minLoss;

            // calculate the score gain and loss for the two players based on their ratio
            float gain, loss;
            if (ratio < avgRatio) // the winner was disadvantaged, so high score gain
            {
                gain = maxGain / 2 + (maxGain / 2) * (avgRatio - ratio) / (avgRatio - minRatio);
                loss = maxLoss / 2 + (maxLoss / 2) * (avgRatio - ratio) / (avgRatio - minRatio);
            }

/*
            // does the ratio provide maximum dominance for the winner over the loser?
            if (ratio >= maxRatio)
            {
                // grant the winner a minimum gain of points
                this.score += minGain;
                // subtract the loser a minimum loss of points
                loser.score -= minLoss;
            }
            // does the ratio provide minimum dominance for the winner over the loser? 
            else if (ratio <= minRatio)
            {
                // grant the winner a maximum gain of points
                this.score += maxGain;
                // subtract the loser a maximum loss of points
                loser.score -= maxLoss;
            }
            // is the ratio at perfect equilibrium between the winner and loser?
            else if (ratio == avgRatio)
            {
                // grant the winner an average gain of points
                this.score += maxGain / 2;
                // subtract the loser an average loss of points
                loser.score -= maxLoss / 2;
            }
            // does the ratio give an advantage to the winner over the loser?
            else if (ratio > avgRatio)
            {
            }
            // does the ratio give a disadvantage to the winner over the loser?
            else if (ratio < avgRatio)
            {
            }
 */
        }
    }

    public class ChallongeRankCalc
    {
        protected List<string> tourneyList;
        protected List<Player> playerList;

        public ChallongeRankCalc()
        {
            TempTourneyListPopulator();
        }

        private void TempTourneyListPopulator()
        {
            tourneyList = new List<string>();
            tourneyList.Add("http://challonge.com/abilenenoobattackpms");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChallongeRankCalc calc = new ChallongeRankCalc();
        }
    }
}
