﻿namespace RurouniJones.DCS.OverlordBot.GameState
{
    public class Player : GameObject
    {
        public string Group { get; set; }
        public int Flight { get; set; }
        public int Plane { get; set; }

        public string Callsign => $"{Group} {Flight} {Plane}";
    }
}
