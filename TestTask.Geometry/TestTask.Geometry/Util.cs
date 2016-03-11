namespace TestTask.Geometry.RightAngledTriangle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Содержит функции связанные с прямоугольным треугольником.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Максимальная точность дробных значений в количестве знаков после запятой.
        /// </summary>
        public static readonly uint Preсision = 14;
        /// <summary>
        /// Максимально допустимая разница в значениях с плавающей точкой, при которой они будут рассматриваться как одинаковые.
        /// </summary>
        public static readonly double Tolerance = Math.Pow(1, -Preсision);

        /// <summary>
        /// Расчет площади прямоугольного треугольника, для случая когда заранее неизвестно какая из сторон является гипотенузой.
        /// </summary>
        /// <param name="a">
        /// Первая сторона.
        /// </param>
        /// <param name="b">
        /// Вторая сторона.
        /// </param>
        /// <param name="c">
        /// Третья сторона.
        /// </param>
        /// <returns>
        /// Возвращает площадь/>.
        /// </returns>
        public static double Area(double a, double b, double c)
        {
            double hypotenuse;
            double cathetus1;
            double cathetus2;
            Define(a, b, c, out cathetus1, out cathetus2, out hypotenuse);

            return cathetus1 * cathetus2 / 2;
        }

        /// <summary>
        /// Определяет тип каждой стороны (катет или гипотенуза) треугольника.
        /// </summary>
        /// <param name="a">Длина первой стороны треугольника</param>
        /// <param name="b">Длина второй стороны треугольника</param>
        /// <param name="c">Длина третьей стороны треугольника</param>
        /// <param name="cathetus1">Длина первого катета</param>
        /// <param name="cathetus2">Длина второго катета</param>
        /// <param name="hypotenuse">Длина гипотенузы</param>
        private static void Define(
            double a, 
            double b, 
            double c, 
            out double cathetus1, 
            out double cathetus2, 
            out double hypotenuse)
        {
            const string RightAngledValidationFailed =
                "Неверные значения аргументов. Сумма квадратов катетов должна быть равна квадрату гипотенузы. Значения аргументов: a = {0}, b = {1}, c = {2}";

            Validate(nameof(a), a);
            Validate(nameof(b), b);
            Validate(nameof(c), c);

            var parameters = new List<double> { a, b, c };
            hypotenuse = parameters.Max();
            parameters.Remove(hypotenuse);
            cathetus1 = parameters.First();
            cathetus2 = parameters.Last();

            if (hypotenuse * hypotenuse - (cathetus1 * cathetus1 + cathetus2 * cathetus2) > Tolerance)
            {
                throw new ArgumentException(string.Format(RightAngledValidationFailed, a, b, c));
            }
        }

        /// <summary>
        /// Проверка корректности значения длины строны треугольника.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="triangleSideValue"></param>
        private static void Validate(string parameterName, double triangleSideValue)
        {
            const string PositiveValueOnly =
                "Для сторон треугольника допустимы только положительные значения. Фактическое значение - {0}";

            if (triangleSideValue < double.Epsilon || double.IsNegativeInfinity(triangleSideValue)
                || double.IsNaN(triangleSideValue))
            {
                throw new ArgumentOutOfRangeException(
                    parameterName, 
                    string.Format(PositiveValueOnly, triangleSideValue));
            }
        }
    }
}