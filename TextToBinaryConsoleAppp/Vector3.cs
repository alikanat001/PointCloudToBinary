using System;

namespace TextToBinaryConsoleAppp
{
    internal class Vector3
    {
        public float x;
        public float y;
        public float z;
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vector3(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public float magnitude()
        {
            return (float) Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public Vector3 normalize()
        {
            return new Vector3(x / magnitude(), y / magnitude(), z / magnitude());
        }
        public void Display()
        {
            Console.WriteLine("(" + x + "," + y + ","+ z + ")");
        }
    }
}