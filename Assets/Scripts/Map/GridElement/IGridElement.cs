namespace HoloRPG.Map.GridElement
{
    public interface IGridElement
    {
        /// <summary>
        /// Can a shadow move on this tile
        /// </summary>
        public bool CanShadowMoveHere();
    }
}
