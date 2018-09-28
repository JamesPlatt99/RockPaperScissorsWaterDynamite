using System;
using System.Collections.Generic;
using System.Text;
using BotInterface.Game;
using BotInterface.Bot;

namespace SuperCoolBotWhatIDidMake
{
    class MostProbableCounter : BaseStrat, IStrat
    {
        public Move GetMove(Gamestate gamestate)
        {
            return GetMostProbableCounter(gamestate);
        }

        private Move GetMostProbableCounter(Gamestate gamestate)
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
    }
}
