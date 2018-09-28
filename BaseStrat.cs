using BotInterface.Bot;
using BotInterface.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperCoolBotWhatIDidMake
{
    class BaseStrat
    {
        private int _roundValue = 1;

        public bool DynamiteAvailable(Gamestate gamestate)
        {
            return RemainingDynamite(gamestate) > 0;
        }

        public int GetRoundValue(Gamestate gamestate)
        {
            if(RoundIndex(gamestate) > 1)
            {
                CalculateRoundValue(gamestate);
            }
            return _roundValue;
        }

        public int RoundIndex(Gamestate gamestate)
        {
            return gamestate.GetRounds().Length;
        }

        private int RemainingDynamite(Gamestate gamestate)
        {
            int dynamiteAvailable = 100;
            foreach (Round round in gamestate.GetRounds())
            {
                if (round.GetP1() == Move.D)
                {
                    dynamiteAvailable--;
                }
            }
            return dynamiteAvailable;
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
            var round = gamestate.GetRounds()[RoundIndex(gamestate) - 2];
            if (round.GetP1() == round.GetP2())
            {
                _roundValue++;
            }
            else
            {
                _roundValue = 1;
            }
        }        

        protected Move PlayRandomStandard()
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
