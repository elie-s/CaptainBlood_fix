using UnityEngine;

// To visualize your curve and test the values, use this : https://www.desmos.com/calculator/0cc2eebpug 
// a = height
// b = highestPointCoord
// c = thickness

public class NormalDistribution
{
    private float height;
    private float highestPointCoord;
    private float thickness;

    /// <summary>
    /// Create an instance of Bell Curve parameter set.
    /// </summary>
    /// <param name="_height">Highest value of your curve.</param>
    /// <param name="_highestPointCoord">Input value that will return the highest value of the curve.</param>
    /// <param name="_thickness">Thickness of your curve. CANNOT EQUAL 0 !</param>
    public NormalDistribution(float _height, float _highestPointCoord, float _thickness)
    {
        if (_thickness == 0) throw new NormalDistributionException("Thickness must not equals 0.");

        height = _height;
        highestPointCoord = _highestPointCoord;
        thickness = _thickness;
    }

    public float GetValueFrom(float _value)
    {
        float power = Mathf.Pow(highestPointCoord - _value, 2) / (2 * Mathf.Pow(thickness, 2));
        float result = height * Mathf.Exp(-power);

        return result;
    }

    /// <summary>
    /// Calculate isolated value from parameters.
    /// </summary>
    /// <param name="_height">Highest value of your curve.</param>
    /// <param name="_highestPointCoord">Input value that will return the highest value of the curve.</param>
    /// <param name="_thickness">Thickness of your curve. CANNOT EQUAL 0 !</param>
    /// <param name="_value">Input value</param>
    /// <returns></returns>
    public static float Calculate(float _height, float _highestPointCoord, float _thickness, float _value)
    {
        if (_thickness == 0) throw new NormalDistributionException("Thickness must not equals 0.");

        float power = Mathf.Pow(_highestPointCoord - _value, 2) / (2 * Mathf.Pow(_thickness, 2));
        float result = _height * Mathf.Exp(-power);

        return result;
    }

    public class NormalDistributionException : System.Exception
    {
        public NormalDistributionException(string _message) : base(_message) { }
    }
}


