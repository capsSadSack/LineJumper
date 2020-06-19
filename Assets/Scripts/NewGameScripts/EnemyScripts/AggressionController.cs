using UnityEngine;

// NOTE: Изменение состояния агрессивный/не агресивный, по аналогии с вероятностью промаха по призракам из HoMM5.
public class AggressionController : IAggressionController
{
    private int aggressiveStatesInARow = 1;
    private int notAggressiveInARow = 0;


    public Aggression GetAggression()
    {
        if (aggressiveStatesInARow == 2)
        {
            aggressiveStatesInARow = 0;
            notAggressiveInARow = 1;
            return Aggression.NotAggressive;
        }

        if (notAggressiveInARow == 2)
        {
            aggressiveStatesInARow = 1;
            notAggressiveInARow = 0;
            return Aggression.Aggressive;
        }

        Aggression aggression = GetRandowmAggression();
        if (aggression == Aggression.Aggressive)
        {
            aggressiveStatesInARow++;
            notAggressiveInARow = 0;
        }
        else
        {
            aggressiveStatesInARow = 0;
            notAggressiveInARow++;
        }

        return aggression;
    }

    private Aggression GetRandowmAggression()
    {
        var value = Random.value;
        if (value > 0.5f)
        {
            return Aggression.Aggressive;
        }
        else
        {
            return Aggression.NotAggressive;
        }
    }
}
