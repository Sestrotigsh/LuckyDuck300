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
    /// Simulates three popular color techniques used in cinema.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage Technicolor")]
    public sealed class VintageTechnicolor : VintageBase
    {
      /// <summary>
      /// Technicolor processes.
      /// </summary>
      public enum Processes
      {
        /// <summary>
        /// 2 Strip additive process.
        /// </summary>
        One,

        /// <summary>
        /// 2 Strip subtractive process.
        /// </summary>
        Two,

        /// <summary>
        /// 3 Strip subtractive  process.
        /// </summary>
        Three
      }

      #region Properties.
      /// <summary>
      /// Technicolor processes.
      /// </summary>
      public Processes Process
      {
        get { return technicolorSystem; }
        set { technicolorSystem = value; }
      }
      #endregion

      #region Private data.
      [SerializeField]
      private Processes technicolorSystem;

      private static readonly string keywordTechnicolorOne = @"TECHNICOLOR_ONE";
      private static readonly string keywordTechnicolorTwo = @"TECHNICOLOR_TWO";
      private static readonly string keywordTechnicolorThree = @"TECHNICOLOR_THREE";
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Simulates three popular color techniques used in cinema.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        technicolorSystem = Processes.One;

        base.ResetDefaultValues();
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        switch (technicolorSystem)
        {
          case Processes.One: material.EnableKeyword(keywordTechnicolorOne); break;
          case Processes.Two: material.EnableKeyword(keywordTechnicolorTwo); break;
          case Processes.Three: material.EnableKeyword(keywordTechnicolorThree); break;
        }
      }
      #endregion
    }
  }
}