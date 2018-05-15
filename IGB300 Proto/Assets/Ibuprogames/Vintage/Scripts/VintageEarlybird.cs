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
    /// Use Earlybird to get a retro 'Polaroid' feel with soft faded colors and a hint of yellow.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage Earlybird")]
    public sealed class VintageEarlybird : VintageBase
    {
      #region Private data.
      private Texture2D curvesTex;
      private Texture2D overlayTex;
      private Texture2D blowoutTex;
      private Texture2D levelsTex;

      private static readonly string variableBlowoutTex = @"_BlowoutTex";
      private static readonly string variableOverlayTex = @"_OverlayTex";
      private static readonly string variableLevelsTex = @"_LevelsTex";
      private static readonly string variableCurvesTex = @"_CurvesTex";
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Use Earlybird to get a retro 'Polaroid' feel with soft faded colors and a hint of yellow.";
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        curvesTex = LoadTextureFromResources(@"Textures/earlyBirdCurves");
        overlayTex = LoadTextureFromResources(@"Textures/earlybirdOverlayMap");
        blowoutTex = LoadTextureFromResources(@"Textures/earlybirdBlowout");
        levelsTex = LoadTextureFromResources(@"Textures/earlybirdMap");
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetTexture(variableCurvesTex, curvesTex);
        material.SetTexture(variableOverlayTex, overlayTex);
        material.SetTexture(variableBlowoutTex, blowoutTex);
        material.SetTexture(variableLevelsTex, levelsTex);
      }
      #endregion
    }
  }
}