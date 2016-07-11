using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1Polynomial;
using NUnit.Framework;
using System.Globalization;

namespace Task1Polynomial.Tests
{
  public class Task1PolynomialTests
  {
    [Test]
    [TestCase(new double[] { -1, 1.4, 0, 7.8, -2.2 }, ExpectedResult = "- x^4 + 1.4x^3 + 7.8x - 2.2")]
    [TestCase(new double[] { 1, 0, -3.3 }, ExpectedResult = "x^2 - 3.3")]
    [TestCase(new double[] { -1.2, 7.4, -1.9, -18.2 }, ExpectedResult = "- 1.2x^3 + 7.4x^2 - 1.9x - 18.2")]
    [TestCase(new double[] { -1, -1.4, -1.1, +6, -7.8, 0 }, ExpectedResult = "- x^5 - 1.4x^4 - 1.1x^3 + 6x^2 - 7.8x")]
    public string Polynomial_ToString(double[] coeffs)
    {
      Polynomial polynomial = new Polynomial(coeffs);
      return polynomial.ToString();
    }


    [Test, TestCaseSource(nameof(FormatAndProviderData))]
    public void Polynomial_ToStringFormattable(string format, IFormatProvider formatProvider, string expectedResult)
    {
      Polynomial polynomial = new Polynomial(new double[] { -1, -1.41, -1.1, +6, -7.87, 0 });
      Assert.AreEqual(polynomial.ToString(format, formatProvider), expectedResult);
    }

    private static readonly object[] FormatAndProviderData =
    {
            new object[] {"G", CultureInfo.CurrentCulture, "- x^5 - 1.41x^4 - 1.1x^3 + 6x^2 - 7.87x"},
            new object[] {"G", CultureInfo.GetCultureInfo("EN-us"), "- x^5 - 1.41x^4 - 1.1x^3 + 6x^2 - 7.87x" },
            new object[] {"G", CultureInfo.GetCultureInfo("Ru-ru"), "- x^5 - 1,41x^4 - 1,1x^3 + 6x^2 - 7,87x" },
            new object[] {"G", CultureInfo.GetCultureInfo("fr-FR"), "- x^5 - 1,41x^4 - 1,1x^3 + 6x^2 - 7,87x" },
            new object[] {"F3", CultureInfo.GetCultureInfo("en-US"), "- x^5 - 1.410x^4 - 1.100x^3 + 6.000x^2 - 7.870x"},
            new object[] {"F1", CultureInfo.InvariantCulture, "- x^5 - 1.4x^4 - 1.1x^3 + 6.0x^2 - 7.9x"},
        };

    [TestCase(new double[] { 8.0, -2.5, 4.5 }, 1.0, ExpectedResult = 10)]
    [TestCase(new double[] { 1.7, 0, 1.8 }, 0, ExpectedResult = 1.8)]
    [TestCase(new double[] { -1.2, 7.4, -5.9 }, 3.3, ExpectedResult = 5.452)]
    public double Polynomial_GetValue(double[] coeffs, double x)
    {
      Polynomial polynomial = new Polynomial(coeffs);
      return polynomial.CalculateValueOfPolynomial(x);
    }


    [TestCase(new double[] { 5.4, 0 }, new double[] { 7.2, -1.2, -8, 0 }, ExpectedResult = "7.2x^3 - 1.2x^2 - 2.6x")]
    [TestCase(new double[] { -1, 1.1, 4.4}, new double[] { 1.2, -2, 1.1 }, ExpectedResult = "0.2x^2 - 0.9x + 5.5")]
    [TestCase(new double[] { 7.2, -1.2, -8, 0 }, new double[] { -7.2, 1.2, 8, 0 }, ExpectedResult = "")]
    public string Polynomial_Addition(double[] pol1, double[] pol2)
    {
      Polynomial firstPolynomial = new Polynomial(pol1);
      Polynomial secondPolynomial = new Polynomial(pol2);

      Polynomial resultPolynomial = firstPolynomial + secondPolynomial;
      return resultPolynomial.ToString();
    }

    [TestCase(new double[] { -1, 1.1, 4.4 }, new double[] { -1, 1.1, 4.4 }, ExpectedResult = "")]
    [TestCase(new double[] { 5.4, 0 }, new double[] { 7.2, -1.2, -8, 0.8 }, ExpectedResult = "- 7.2x^3 + 1.2x^2 + 13.4x - 0.8")]
    public string Polynomial_Deduction(double[] firstCoeffs, double[] secondCoeffs)
    {
      Polynomial firstPolynomial = new Polynomial(firstCoeffs);
      Polynomial secondPolynomial = new Polynomial(secondCoeffs);

      Polynomial resultPolynomial = firstPolynomial - secondPolynomial;
      return resultPolynomial.ToString();
    }

  }
}
