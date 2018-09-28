using BotInterface.Bot;
using BotInterface.Game;
using System;

namespace SuperCoolBotWhatIDidMake
{
    public class Bot : IBot
    {
        const int PlayDynamiteAtRoundValue = 2;

        private readonly GameStateAnalyser GameStateAnalyser = new GameStateAnalyser();

        public Move MakeMove(Gamestate gamestate)
        {
            GameStateAnalyser.RoundIndex++;
            if (IsItWorthPlayingDynamite(gamestate))
            {
                if(GameStateAnalyser.DynamiteAvailable())
                {
                    return PlayDynamite();
                }
                if(GameStateAnalyser.OpponentHasDynamiteAvailable(gamestate))
                {
                    return PlayWater();
                }
            }
            return GameStateAnalyser.GetMostProbableCounter(gamestate);
        }

        private Move PlayDynamite()
        {
            GameStateAnalyser.RemainingDynamite--;
            return Move.D;
        }

        private Move PlayWater()
        {
            return Move.W;
        }


        private bool IsItWorthPlayingDynamite(Gamestate gamestate)
        {
            return GameStateAnalyser.GetRoundValue(gamestate) >= PlayDynamiteAtRoundValue;
        }

        private bool TossACoin()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 100) % 2 == 0);
        }

    }
}
