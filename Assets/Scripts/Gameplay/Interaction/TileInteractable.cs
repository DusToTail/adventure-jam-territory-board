using UnityEngine;
using TerritoryBoard;

namespace Gameplay.Interaction
{
    [RequireComponent(typeof(HexagonTile))]
    public class TileInteractable : BaseInteractable
    {
        public override void OnHoverEnter()
        {
            // TODO: turn on highlight and transit to hovered animation clip

            // Required for proper enter/exit of coroutine for OnHoverStay()
            base.OnHoverEnter();
        }
        public override void OnHoverExit()
        {
            // TODO: turn off highlight and transit back to idle animation clip

            // Required for proper enter/exit of coroutine for OnHoverStay()
            base.OnHoverExit();
        }
        public override void OnSelectEnter()
        {
            // TODO: turn on highlight and transit to selected animation clip

            // Required for proper enter/exit of coroutine for OnSelectStay()
            base.OnSelectEnter();
        }
        public override void OnSelectExit()
        {
            // TODO: turn on highlight and transit to idle animation clip

            // Required for proper enter/exit of coroutine for OnSelectStay()
            base.OnSelectExit();
        }
    }
}
