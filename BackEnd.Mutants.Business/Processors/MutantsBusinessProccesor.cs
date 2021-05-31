using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Mutants.Business.Processors
{
    public static class MutantsBusinessProccesor
    {
        public static List<int> ExtractDimensionMatriz(List<string> dna)
        {
            int rows = dna.Count;
            int columns = 0;
            foreach (var item in dna)
            {
                columns = item.ToCharArray().Length;
                break;
            }
            return new List<int> { rows, columns };
        }

        public static char[,] ConvertDataBidimentional(List<string> dna, int typeConvert)
        {
            char[,] matriz = new char[dna.Count, dna.FirstOrDefault().Length];
            switch (typeConvert)
            {
                case 1:

                    for (int a = 0; a < dna.Count; a++)
                    {
                        var arrDna = dna[a].ToCharArray();
                        for (int b = 0; b <= dna.FirstOrDefault().Length - 1; b++)
                        {
                            matriz[a, b] = arrDna[b];
                        }
                    }
                    break;
                case 2:

                    for (int a = 0; a < dna.Count; a++)
                    {
                        var arrDna = dna[a].ToCharArray();
                        for (int b = 0; b <= dna.FirstOrDefault().Length - 1; b++)
                        {
                            matriz[b, a] = arrDna[b];
                        }
                    }
                    break;
                case 3:
                    for (int a = 0; a < dna.Count; a++)
                    {
                        var arrDna = dna[a].ToCharArray();
                        for (int b = 0; b <= dna.FirstOrDefault().Length - 1; b++)
                        {
                            if (a == b)
                                matriz[a, b] = arrDna[a];                            
                        }
                    }
                    break;
            }

            return matriz;
        }

        public static int ValidateMutant(char[,] dna, List<int> dimensions, int i)
        {
            int count = 0;
            //validate Matriz.
            if (i != 3)
            {
                for (int a = 0; a <= dimensions[0] - 1; a++)
                {
                    char[] arrDna = new char[dimensions[0]];
                    int resultParent = 0;
                    for (int b = 0; b <= dimensions[1] - 1; b++)
                    {
                        arrDna[b] = dna[a, b];
                    }

                    foreach (var item in arrDna)
                    {
                        arrDna = arrDna.Where((source, index) => index != 0).ToArray();
                        if (arrDna.Length > 0)
                        {
                            if (item == arrDna[0])
                                resultParent++;
                            else
                                resultParent = 0;
                        }
                        if (resultParent == 3)
                            count++;
                    }
                }
            }
            else if (i == 3)
            {
                char[] arrDna = new char[dimensions[0]];
                char[] arrDnaInvert = new char[dimensions[0]];
                int resultParent = 0;
                for (int a = 0; a <= dimensions[0] - 1; a++)
                {
                    for (int b = 0; b <= dimensions[1] - 1; b++)
                    {
                        if( a == b)
                            arrDna[b] = dna[a, b];
                    }
                }
                int position = 0;
                for (int a = dimensions[0] - 1; a >= 0; a--)
                {
                    for (int b = dimensions[1] - 1; b >= 0; b--)
                    {
                        if (a == b)
                        {
                            arrDnaInvert[position] = dna[a, b];
                            position++;
                        }
                            
                    }
                }

                foreach (var item in arrDna)
                {
                    arrDna = arrDna.Where((source, index) => index != 0).ToArray();
                    if (arrDna.Length > 0)
                    {
                        if (item == arrDna[0])
                            resultParent++;
                        else
                            resultParent = 0;
                    }
                    if (resultParent == 3)
                        count++;
                }

                foreach (var item in arrDnaInvert)
                {
                    arrDnaInvert = arrDnaInvert.Where((source, index) => index != 0).ToArray();
                    if (arrDnaInvert.Length > 0)
                    {
                        if (item == arrDnaInvert[0])
                            resultParent++;
                        else
                            resultParent = 0;
                    }
                    else
                        resultParent = 0;

                    if (resultParent == 3)
                        count++;
                }
            }
            
            return count;
        }
    }
}
