using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class Calculator
    {
        public String Name;
        public float sum;
        public float count;
        public char size;
        public float value;

        public float Add(int a, int b)
        {
            value = a + b;
            return (float)value;
        }

        public float Mul(int a, int b)
        {
            value = a * b;
            return (float)value;
        }

        public float Sub(int a, int b)
        {
            value = a - b;
            return (float)value;
        }
    }
}
