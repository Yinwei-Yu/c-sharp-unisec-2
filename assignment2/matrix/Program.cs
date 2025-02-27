namespace matrix
{
    internal class Program
    {
        public static bool isToeplitz(int[,] matrix)
        {
            int row = matrix.GetLength(0);//行数
            int col = matrix.GetLength(1);//列数
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i + 1 < row && j + 1 < col && matrix[i, j] != matrix[i + 1, j + 1])//不是边界值且满足要求
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("判断下面的矩阵是否是托普利茨矩阵:\n");
            int[,] matrix =
            {
                {1,2,3,4 },
                {5,1,2,3 },
                {9,5,1,2 }
            };
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            bool yes = isToeplitz(matrix);
            if (yes)
            {
                Console.WriteLine("是托普利茨矩阵");
            }
            else
            {
                Console.WriteLine("不是托普利茨矩阵");
            }
            Console.ReadKey();
        }
    }
}