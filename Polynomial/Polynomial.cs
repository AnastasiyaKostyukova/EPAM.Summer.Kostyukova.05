using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Task1Polynomial
{
  public class Polynomial : IFormattable
  {
    private double[] _coeffs;

    public Polynomial(double[] coeffs)
    {
      _coeffs = coeffs;
    }
    
    public double[] Coeffs
    {
      get
      {
        return _coeffs;
      }
    }

    #region overrides of operators +, -

    public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
    {
      double[] resultCoeffs;
      double[] minArray;

      if (pol1.Coeffs.Length > pol2.Coeffs.Length)
      {
        resultCoeffs = new double[pol1.Coeffs.Length];
        Array.Copy(pol1.Coeffs, resultCoeffs, pol1.Coeffs.Length);
        minArray = pol2.Coeffs;
      }
      else
      {
        resultCoeffs = new double[pol2.Coeffs.Length];
        Array.Copy(pol2.Coeffs, resultCoeffs, pol2.Coeffs.Length);
        minArray = pol1.Coeffs;
      }

      var j = resultCoeffs.Length - 1;
      for(var i = minArray.Length - 1; i >= 0; i--)
      {
        resultCoeffs[j] += minArray[i];
        j--;
      }

      return new Polynomial(resultCoeffs);
    }
    
    public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
    {
      var tempPol2 = new double[pol2.Coeffs.Length];
      Array.Copy(pol2.Coeffs, tempPol2, pol2.Coeffs.Length);
      
      for(var i = 0; i < tempPol2.Length; i++)
      {
        tempPol2[i] *= -1;
      }

      var tempPolinomial = new Polynomial(tempPol2);

      return pol1 + tempPolinomial;
    }

    #endregion

    #region all overrides of methods ToString()
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    public string ToString(IFormatProvider formatProvider)
    {
      return ToString("G", formatProvider);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      if(_coeffs == null)
      {
        throw new ArgumentException("Coefficients should be not null.");
      }

      if (String.IsNullOrEmpty(format))
      {
        format = "G";
      }

      if (formatProvider == null)
      {
        formatProvider = CultureInfo.CurrentCulture;
      }

      StringBuilder createdString = new StringBuilder();
      int d = GetDegree();

      for (int i = 0; i < d; i++)
      {
        var numberWithSign = WriteNumberWithSign(_coeffs[i], format, formatProvider, i == 0);

        if (numberWithSign != null)
        {
          createdString.Append(numberWithSign);
          createdString.Append("x");

          if (d - i != 1)
          {
            createdString.Append("^");
            createdString.Append(d - i);
          }
        }
      }

      createdString.Append(WriteNumberWithSign(_coeffs[_coeffs.Length - 1], format, formatProvider));

      return createdString.ToString();
    }
    #endregion

    public double CalculateValueOfPolynomial(double x)
    {
      double result = 0;
      int d = GetDegree();

      for (int i = 0; i <= GetDegree(); i++)
      {
        result += _coeffs[i] * Math.Pow(x, d);
        d--;
      }

      return result;
    }

    #region private methods-helpers
    private string WriteNumberWithSign(double a, string format, IFormatProvider formatProvider, bool isFirstElement = false)
    {
      StringBuilder temp = new StringBuilder();

      if (a == 0)
      {
        return null;
      }

      if (isFirstElement == false)
      {
        if (a > 0)
        {
          temp.Append(" + ");
        }
        else
        {
          temp.Append(" - ");
        }
      }
      else
      {
        if (a < 0)
        {
          temp.Append("- ");
        }
      }

      if (Math.Abs(a) != 1)
      {
        temp.Append(Math.Abs(a).ToString(format, formatProvider));
      }

      return temp.ToString();
    }

    private int GetDegree()
    {
      if (_coeffs.Length == 0)
      {
        return 0;
      }
      return _coeffs.Length - 1;
    }
    #endregion
  }
}
