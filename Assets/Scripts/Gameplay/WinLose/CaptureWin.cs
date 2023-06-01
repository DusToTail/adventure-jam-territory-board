using TerritoryBoard;

namespace Gameplay
{
    public class CaptureWin : BooleanTriggerAction
    {
        public CaptureWin(IBooleanCondition condition, ITrigger trigger) : base(condition, trigger)
        {
            //TODO ICapturable and ICapture interfaces?
        }
    }
}
