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
    /// Hefe slightly increases saturation and gives a warm fuzzy tone to your game.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage Hefe")]
    public sealed class VintageHefe : VintageBase
    {
      #region Private data.
      private Texture2D edgeBurnTex;
      private Texture2D levelsTex;
      private Texture2D gradientTex;
      private Texture2D softLightTex;

      private static readonly string variableEdgeBurnTex = @"_EdgeBurnTex";
      private static readonly string variableLevelsTex = @"_LevelsTex";
      private static readonly string variableGradientTex = @"_GradientTex";
      private static readonly string variableSoftLightTex = @"_SoftLightTex";
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"Hefe slightly increases saturation and gives a warm fuzzy tone to your game.";
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        edgeBurnTex = LoadTextureFromResources(@"Textures/edgeBurn");
        levelsTex = LoadTextureFromResources(@"Textures/hefeMap");
        gradientTex = LoadTextureFromResources(@"Textures/hefeGradientMap");
        softLightTex = LoadTextureFromResources(@"Textures/hefeSoftLight");
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
          material.SetTexture(variableEdgeBurnTex, edgeBurnTex);
          material.SetTexture(variableLevelsTex, levelsTex);
          material.SetTexture(variableGradientTex, gradientTex);
          material.SetTexture(variableSoftLightTex, softLightTex);
      }
      #endregion
    }
  }
}