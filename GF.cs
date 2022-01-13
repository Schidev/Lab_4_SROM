using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    internal class GF
    {
        // poly(x) = x^509 + x^23 + x^3 + x^2 + 1;

        string _basis;
        string _power = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111110";
        string _module = "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000001101";
        int[] _indexes = new int[] { 0, 2, 3, 23, 509 };

        string[,] _lambda = new string[510, 510];

        /*string _power = "10";
        string _module = "111";
        int[] _indexes = new int[] { 0, 1, 2 };
        string[,] _lambda = new string [3,3];*/

        public GF(string basis)
        {
            _basis = basis;

            BigInteger bigInteger_1 = new BigInteger();
            BigInteger bigInteger_2 = new BigInteger();

            BigInteger temp_1 = new BigInteger();
            BigInteger temp_2 = new BigInteger();
            BigInteger temp_3 = new BigInteger();
            BigInteger temp_4 = new BigInteger();
            for (int i = 0; i < 510; i++)
            {
                for (int j = 0; j < 510; j++)
                {
                    bigInteger_1 = BigInteger.ModPow(2, i, 1019);
                    bigInteger_2 = BigInteger.ModPow(2, j, 1019);

                    temp_1 = BigInteger.ModPow((bigInteger_1 + bigInteger_2), 1, 1019);
                    temp_2 = BigInteger.ModPow((bigInteger_1 - bigInteger_2), 1, 1019);
                    temp_3 = BigInteger.ModPow((-bigInteger_1 + bigInteger_2), 1, 1019);
                    temp_4 = BigInteger.ModPow((-bigInteger_1 - bigInteger_2), 1, 1019);

                    if ((temp_1 == 1) || (temp_2 == 1) || (temp_3 == 1) || (temp_4 == 1))
                    {
                        _lambda[i, j] = "1";
                    }
                    else
                    {
                        _lambda[i, j] = "0";
                    }
                }
            }
        }

        public string Module
        {
            get { return _module; }
        }
        public string Element
        {
            get { return _basis; }
            set { _basis = value; }
        }
        public int FindOrd()
        {
            int ord = 0;
            BigInteger bigInteger = new BigInteger();
            for (int k = 1; k < 1019; k++)
            {
                bigInteger = BigInteger.ModPow(2, k, 1019);
                if (bigInteger == 1)
                {
                    ord = k;

                }
            }
            Console.WriteLine("ORD = {0}", ord);
            return ord;
        }
        public bool Optim()
        {
            //Простое
            int p = 2 * 509 + 1;

            bool c = false;

            if ((this.FindOrd() == 1018) || ((p % 4) == 3) && (this.FindOrd() == 509))
            {
                c = true;
            }

            return c;
        }
        public void Mod()
        {
            while (this._basis.Length >= this._module.Length)
            {
                for (int q = 0; q < this.Element.Length; q++)
                {
                    if (this.Element[q] != '0')
                    {
                        this.Element = this.Element.Remove(0, q);
                        q = this.Element.Length;
                    }
                    if (q == this.Element.Length - 1)
                    {
                        this.Element = "0";
                        return;
                    }
                }

                if (this._basis.Length >= this._module.Length)
                {
                    GF t = new GF(this._module + String.Concat(Enumerable.Repeat("0", this.Element.Length - this._module.Length)));
                    this.Element = (t + this).Element;
                }
            }
        }
        public string Y()
        {
            GF answer = new GF(this._power);

            return this.Pow(answer);
        }
        public void _Trace()
        {
            int trace = 0;
            for (int q = 0; q < this.Element.Length; q++)
            {
                if (this.Element[q] == '1')
                {
                    trace += 1;
                    trace = trace % 2;
                }
            }
            Console.WriteLine("Tr({0}) = {1}", this.Element, trace);
        }
        public static BigInteger B(string num)
        {
            BigInteger sum = 0;

            if (num.Length == 1)
            {
                return sum = BigInteger.Parse(num[0].ToString());
            }

            for (int q = 0; q < num.Length; q++)
            {
                var n = BigInteger.Pow(2, num.Length - q - 1);
                sum = sum + BigInteger.Parse(num[q].ToString()) * n;
            }

            return sum;
        }
        public GF Square()
        {
            GF u = new GF((String.Concat(Enumerable.Repeat("0", 510 - this._basis.Length)).ToString() + this._basis));
            u._basis = u._basis[u._basis.Length - 1].ToString() + u._basis.Remove(u._basis.Length - 1);
            return u;
        }
        public GF YY()
        {
            GF temp = new GF(this._basis);
            BigInteger counter = new BigInteger(0);
            counter = BigInteger.Parse("837987995621412318723376562387865382967460363787024586107722590232610251879596686050117143635431464230626991136655378178359617675746660621652103062880255");
            //837987995621412318723376562387865382967460363787024586107722590232610251879596686050117143635431464230626991136655378178359617675746660621652103062880255

            while (counter != 0)
            {
                temp.Square();
                counter--;
            }

            return temp;
        }
        public static GF operator ^(GF _elem_1, GF _elem_2)
        {
            GF _mult = new GF("");
            string _el_1 = _elem_1.Element;
            string _el_2 = _elem_2.Element;

            int[] ind = new int[510];
            string[] h = new string[510];

            string y = "";


            for (int j = 0; j < 510; j++)
            {
                for (int i = 0; i < 510; i++)
                {
                    if (i == 1)
                    {
                        ind[j] += 1;
                    }
                }
            }

            for (int j = 0; j < 510; j++)
            {
                GF temp = new GF("0");
                for (int t = 0; t < ind[j]; t++)
                {
                    temp = temp + _elem_1;
                }
                h[j] = temp._basis;
            }

            for (int j = 0; j < 510; j++)
            {
                GF temp = new GF(h[j]);
                temp = temp + _elem_2;
                temp.Element = temp.Element.TrimStart(new Char[] { '0' });
                if (temp.Element == "")
                {
                    temp.Element = "0";
                }
                y = y + temp.Element;
            }

            return new GF(y);
        }
        public string Pow(GF exponent)
        {
            for (int q = 0; q < exponent.Element.Length; q++)
            {
                if (exponent.Element[q] != '0')
                {
                    exponent.Element = exponent.Element.Remove(0, q);
                    q = exponent.Element.Length;
                }
                if (q == exponent.Element.Length - 1)
                {
                    exponent.Element = "0";
                    q = exponent.Element.Length;
                }
            }

            for (int q = 0; q < this.Element.Length; q++)
            {
                if (this.Element[q] != '0')
                {
                    this.Element = this.Element.Remove(0, q);
                    q = this.Element.Length;
                }
                if (q == this.Element.Length - 1)
                {
                    this.Element = "0";
                    q = this.Element.Length;
                }
            }

            if (exponent._basis == "10")
            {
                return (this.Square())._basis;
            }
            else if (exponent._basis == "1")
            {
                return this._basis;
            }
            else
            {
                return ModPow(this, exponent)._basis;
            }
        }

        public GF ModPow(GF baseNum, GF exponent)
        {
            GF temp = new GF(baseNum._basis);
            bool c = false;
            BigInteger b = B(exponent._basis);
            if (BigInteger.ModPow(b, 1, 2) == 1)
            {
                c = true;
            }

            b = (b - 1) / 2;

            for (int i = 0; i < b; i++)
            {
                temp.Square();
            }

            if (c)
            {
                temp = temp * baseNum;
            }
            return temp;
        }

        public GF Gorner(GF x, string a, int i = 0)
        {
            if (i >= a.Length) return new GF("1");
            return new GF(a[i] + (x * Gorner(x, a, i + 1)).Element);
        }

        public static GF operator +(GF _elem_1, GF _elem_2)
        {
            GF _sum = new GF("");

            if (_elem_1._basis.Length < _elem_2._basis.Length)
            {
                _sum._basis = (_elem_2 + _elem_1)._basis;
            }
            else
            {
                string num_1 = _elem_1._basis;
                string num_2 = _elem_2._basis;

                for (int q = 0; q < num_1.Length - num_2.Length + q; q++)
                {
                    num_2 = "0" + num_2;
                }

                for (int q = 0; q < num_1.Length; q++)
                {
                    if ((num_1[q] == num_2[q]))
                    {
                        _sum.Element = _sum.Element + "0";
                    }
                    else
                    {
                        _sum.Element = _sum.Element + "1";
                    }
                }

                _sum.Mod();
            }

            return _sum;
        }
        public static GF operator *(GF _elem_1, GF _elem_2)
        {
            GF _mult = new GF("");
            string _el_1 = _elem_1.Element;
            string _el_2 = _elem_2.Element;

            for (int q = 0; q < _elem_1.Element.Length; q++)
            {
                if (_elem_1.Element[q] != '0')
                {
                    _elem_1.Element = _elem_1.Element.Remove(0, q);
                    q = _elem_1.Element.Length;
                }
            }

            for (int q = 0; q < _elem_2.Element.Length; q++)
            {
                if (_elem_2.Element[q] != '0')
                {
                    _elem_2.Element = _elem_2.Element.Remove(0, q);
                    q = _elem_2.Element.Length;
                }
            }

            if (B(_elem_1._basis) < B(_elem_2._basis))
            {
                _mult._basis = (_elem_2 * _elem_1)._basis;
            }
            else
            {
                string num_1 = _elem_1._basis;
                string num_2 = _elem_2._basis;

                for (int q = 0; q < _elem_2.Element.Length; q++)
                {
                    if (_elem_2.Element[_elem_2.Element.Length - q - 1] == '1')
                    {
                        _elem_1.Element = _elem_1.Element + String.Concat(Enumerable.Repeat("0", q));
                        _mult.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length - _mult.Element.Length)) + _mult.Element;
                        _mult.Element = (_mult + _elem_1).Element;
                    }
                    else
                    {
                        _elem_1.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length + q));
                        _mult.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length - _mult.Element.Length)) + _mult.Element;
                        _mult.Element = (_mult + _elem_1).Element;
                    }

                    _elem_1.Element = _el_1;
                }


            }

            _mult.Mod();

            return _mult;
        }

    }
}
