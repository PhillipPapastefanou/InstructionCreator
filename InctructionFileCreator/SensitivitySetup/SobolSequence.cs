using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SensitivitySetup
{
    public class SobolSequence
    {
        private string _sobolNumbers;
        private int[] dL;
        private int[] sL;
        private int[] aL;
        private List<int>[] mL;


        private double[,] points;

        public double[,] Points => points;


        public SobolSequence(string sobolNumbers)
        {
            this._sobolNumbers = sobolNumbers;

            using (StreamReader reader = File.OpenText(sobolNumbers))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split('\n');

                dL = new int[lines.Length - 1];
                sL = new int[lines.Length - 1];
                aL = new int[lines.Length - 1];
                mL = new List<int>[lines.Length - 1];

                for (int i = 1; i < lines.Length - 1; i++)
                {
                    string values = lines[i];

                    dL[i - 1] = Convert.ToInt32(values.Substring(0, 6));
                    sL[i - 1] = Convert.ToInt32(values.Substring(6, 6));
                    aL[i - 1] = Convert.ToInt32(values.Substring(12, 6));


                    string[] mStrings = values.Substring(18, values.Length - 18)
                        .Split(new string[] {" "}, StringSplitOptions.None);

                    mL[i - 1] = new List<int>();

                    for (int j = 0; j < mStrings.Length; j++)
                    {
                        if (mStrings[j] != String.Empty)
                        {
                            mL[i - 1].Add(Convert.ToInt32(mStrings[j]));
                        }

                    }

                }

            }

        }


        public void GetSequence(int N, int D)
        {
            // L = max number of bits needed 
            uint L = (uint) Math.Ceiling(Math.Log((double) N) / Math.Log(2.0));

            // C[i] = index from the right of the first zero bit of i
            uint[] C = new uint[N];
            C[0] = 1;
            for (uint i = 1; i < N; i++)
            {
                C[i] = 1;
                uint value = i;

                while ((value & 1) != 0)
                {
                    value >>= 1;
                    C[i]++;
                }
            }


            // POINTS[i][j] = the jth component of the ith point
            //                with i indexed from 0 to N-1 and j indexed from 0 to D-1

            points = new double[N, D];

            // ----- Compute the first dimension -----
            // Compute direction numbers V[1] to V[L], scaled by pow(2,32)
            uint[] V = new uint[L + 1];

            for (int i = 1; i < L + 1; i++)
            {
                V[i] = (uint) (1 << 32 - i);
            }

            // Evalulate X[0] to X[N-1], scaled by pow(2,32)
            uint[] X = new uint[N];
            X[0] = 0;

            for (uint i = 1; i < N; i++)
            {
                X[i] = X[i - 1] ^ V[C[i - 1]];
                points[i, 0] = (double) X[i] / Math.Pow(2.0, 32.0);
            }



            for (int j = 1; j < D; j++)
            {
                V = new uint[L + 1];


                List<int> m = mL[j - 1];
                int s = sL[j - 1];
                int d = dL[j - 1];
                int a = aL[j - 1];

                if (L < s + 1)
                {
                    for (int i = 1; i < L + 1; i++)
                        V[i] = (uint) (m[i - 1] << (32 - i));
                }

                else
                {
                    for (int i = 1; i < s + 1; i++)
                    {
                        V[i] = (uint) (m[i - 1] << (32 - i));
                    }

                    for (int i = s + 1; i < L + 1; i++)
                    {
                        V[i] = V[i - s] ^ (V[i - s] >> s);

                        for (int k = 1; k < s; k++)
                        {
                            V[i] ^= (uint) (((a >> (s - 1 - k)) & 1) * V[i - k]);
                        }
                    }

                }


                X = new uint[N];
                X[0] = 0;

                for (int i = 1; i < N; i++)
                {

                    X[i] = X[i - 1] ^ V[C[i - 1]];

                    points[i, j] = (double) X[i] / Math.Pow(2.0, 32.0);
                }



            }


        }
    }
}
