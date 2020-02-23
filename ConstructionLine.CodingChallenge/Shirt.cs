using System;

namespace ConstructionLine.CodingChallenge
{
    public class Shirt
    {
        public Guid Id { get; }

        public string Name { get; }

        public Size Size { get; }   // I have removed the public setter to ensure it is only initialised from constructor 

        public Color Color { get; } // I have removed the public setter to ensure it is only initialised from constructor

        public Shirt(Guid id, string name, Size size, Color color)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
        }
    }
}