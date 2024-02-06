using System;

namespace GestionBancariaTest
{
    internal class DataRowAttribute : Attribute
    {
        public DataRowAttribute(int v1, int v2, int v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public int V1 { get; }
        public int V2 { get; }
        public int V3 { get; }
    }
}