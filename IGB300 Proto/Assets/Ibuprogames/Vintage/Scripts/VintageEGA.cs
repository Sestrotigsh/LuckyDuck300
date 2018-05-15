///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Ibuprogames
{
  namespace VintageAsset
  {
    /// <summary>
    /// Simulates EGA video output plus dithering.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage EGA")]
    public sealed class VintageEGA : VintageBase
    {
      #region Properties.
      /// <summary>
      /// Luminosity [0.0 - 1.0]. Default 1.0.
      /// </summary>
      public float Luminosity
      {
        get { return luminosity; }
        set { luminosity = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Threshold of the palette [0.0 - 2.0]. Default 1.0.
      /// </summary>
      public float Threshold
      {
        get { return threshold; }
        set { threshold = Mathf.Clamp(value, 0.0f, 2.0f); }
      }
      #endregion

      #region Private data.
      private static readonly string variableLuminosity = @"_Luminosity";
      private static readonly string variableThreshold = @"_Threshold";

      [SerializeField]
      private float luminosity = 1.0f;

      [SerializeField]
      private float threshold = 1.0f;
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Simulates EGA video output plus dithering.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        luminosity = 1.0f;
        threshold = 1.0f;

        base.ResetDefaultValues();
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetFloat(variableLuminosity, luminosity);
        material.SetFloat(variableThreshold, threshold);
      }
      #endregion
    }
  }
}