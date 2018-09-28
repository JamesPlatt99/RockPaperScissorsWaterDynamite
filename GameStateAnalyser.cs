using BotInterface.Bot;
using BotInterface.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperCoolBotWhatIDidMake
{
    class GameStateAnalyser
    {
        public int RemainingDynamite = 100;
        public int RoundIndex { get; set; }
        private int _roundValue = 1;

        public bool DynamiteAvailable()
        {
            return RemainingDynamite > 0;
        }

        public int GetRoundValue(Gamestate gamestate)
        {
            if(RoundIndex > 1)
            {
                CalculateRoundValue(gamestate);
            }
            return _roundValue;
        }

        public bool OpponentHasDynamiteAvailable(Gamestate gamestate)
        {
            int dynamiteUsed = 0;
            foreach(Round round in gamestate.GetRounds())
            {
                if (round.GetP2() == Move.D)
                {
                    dynamiteUsed++;
                }
            }
            return dynamiteUsed > 100;
        }

        private void CalculateRoundValue(Gamestate gamestate)
        {
            var round = gamestate.GetRounds()[RoundIndex - 2];
            if (round.GetP1() == round.GetP2())
            {
                _roundValue++;
            }
            else
            {
                _roundValue = 1;
            }
        }

        public Move GetMostProbableCounter(Gamestate gamestate)
        {
            Move mostUsedMove = GetMostUsedMoveFromOpp(gamestate);
            switch (mostUsedMove)
            {
                case Move.R:
                    return Move.P;
                case Move.P:
                    return Move.S;
                case Move.S:
                    return Move.R;
                default:
                    return PlayRandomStandard();
            }
        }

        private Move GetMostUsedMoveFromOpp(Gamestate gamestate)
        {
            int rockPlays = 0;
            int paperPlays = 0;
            int scissorPlays = 0;
            foreach (Round round in gamestate.GetRounds())
            {
                switch (round.GetP2())
                {
                    case Move.R:
                        rockPlays++;
                        break;
                    case Move.P:
                        paperPlays++;
                        break;
                    case Move.S:
                        scissorPlays++;
                        break;
                }
            }
            if (rockPlays > paperPlays && rockPlays > scissorPlays) return Move.R;
            if (paperPlays > rockPlays && paperPlays > scissorPlays) return Move.P;
            if (scissorPlays > rockPlays && scissorPlays > paperPlays) return Move.S;
            //Default to water to say there is not a most common play
            return Move.W;
        }

        private Move PlayRandomStandard()
        {
            var rand = new Random();
            switch (rand.Next(0, 3))
            {
                case 1:
                    return Move.R;
                case 2:
                    return Move.P;
                default:
                    return Move.S;
            }
        }
    }
}
