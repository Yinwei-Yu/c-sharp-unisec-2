namespace rectangle
{
    public interface IShape
    {
        double Area { get; }
        bool isValid { get; }
    }

    public class Rectangle : IShape
    {
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; private set; }
        public double Height { get; private set; }

        public double Area => Width * Height;
        public bool isValid => Width > 0 && Height > 0;
    }

    public class Square : IShape
    {
        public double Side
        {
            get; private set;
        }

        public Square(double side)
        {
            Side = side;
        }

        public double Area => Side * Side;
        public bool isValid => (Side > 0) && (Side == Side);
    }

    public class Triangle : IShape
    {
        public Triangle(double side1, double side2, double side3)
        {
            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
        }

        public double Side1 { get; private set; }
        public double Side2 { get; private set; }
        public double Side3 { get; private set; }

        public double Area
        {
            get
            {
                double s = (Side1 + Side2 + Side3) / 2;
                return System.Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
            }
        }

        public bool isValid
        {
            get
            {
                return (Side1 + Side2 > Side3) && (Side1 + Side3 > Side2) && (Side2 + Side3 > Side1);
            }
        }
    }

    public class ShapeFactory
    {
        private static readonly Random _random = new Random();

        public static IShape CreateRandomShape()
        {
            int type = _random.Next(3); // 0:矩形 1:正方形 2:三角形

            switch (type)
            {
                case 0:
                    return new Rectangle(_random.NextDouble() * 10 + 1,
                                         _random.NextDouble() * 10 + 1);

                case 1:
                    return new Square(_random.NextDouble() * 10 + 1);

                case 2:
                    double a, b, c;
                    do
                    {
                        a = _random.NextDouble() * 10 + 1;
                        b = _random.NextDouble() * 10 + 1;
                        c = _random.NextDouble() * 10 + 1;
                    } while (a + b <= c || a + c <= b || b + c <= a);
                    return new Triangle(a, b, c);

                default:
                    throw new InvalidOperationException("Unexpected shape type");
            }
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            for (int j = 0; j < 10; j++)
            {
                double totalArea = 0;
                List<IShape> shapes = new List<IShape>();

                // 创建10个随机形状
                for (int i = 0; i < 10; i++)
                {
                    shapes.Add(ShapeFactory.CreateRandomShape());
                }

                // 计算面积之和
                foreach (var shape in shapes)
                {
                    totalArea += shape.Area;
                }

                Console.WriteLine($"The {j}th total area of 10 shapes: {totalArea:F2}");
            }
        }
    }
}