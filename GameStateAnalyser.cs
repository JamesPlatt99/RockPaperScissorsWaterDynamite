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
    }
}
