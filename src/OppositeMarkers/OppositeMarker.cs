namespace src.OppositeMarkers
{
    public class OppositeMarker
    {
        
        
        public static string Marker(string marker)
        {
            return marker == GlobalConstants.GlobalConstants.XMarker ? 
                GlobalConstants.GlobalConstants.OMarker : GlobalConstants.GlobalConstants.XMarker;
        }
    }
}