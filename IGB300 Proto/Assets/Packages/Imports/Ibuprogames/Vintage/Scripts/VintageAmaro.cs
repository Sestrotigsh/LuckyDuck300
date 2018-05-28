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
    /// This effect adds more light to the centre of the screen and darkens around the edges.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage Amaro")]
    public sealed class VintageAmaro : VintageBase
    {
      #region Properties.
      /// <summary>
      /// Overlay strength [0.0 - 1.0]. Default 0.5.
      /// </summary>
      public float Overlay
      {
        get { return overlayStrength; }
        set { overlayStrength = Mathf.Clamp01(value); }
      }
      #endregion

      #region Private data.
      private Texture2D blowoutTex;
      private Texture2D overlayTex;
      private Texture2D levelsTex;

      private static readonly string variableBlowoutTex = @"_BlowoutTex";
      private static readonly string variableOverlayTex = @"_OverlayTex";
      private static readonly string variableLevelsTex = @"_LevelsTex";
      private static readonly string variableOverlayStrength = @"_OverlayStrength";

      [SerializeField]
      private float overlayStrength = 0.5f;
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"This effect adds more light to the centre of the screen and darkens around the edges.";
      }

      /// <summary>
      /// Set the default values of the shader.
      /// </summary>
      public override void ResetDefaultValues()
      {
        overlayStrength = 0.5f;

        base.ResetDefaultValues();
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        blowoutTex = LoadTextureFromResources(@"Textures/blackboard1024");
        overlayTex = LoadTextureFromResources(@"Textures/overlayMap");
        levelsTex = LoadTextureFromResources(@"Textures/amaroMap");
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetTexture(variableBlowoutTex, blowoutTex);
        material.SetTexture(variableLevelsTex, levelsTex);
        material.SetTexture(variableOverlayTex, overlayTex);

        material.SetFloat(variableOverlayStrength, overlayStrength);
      }
      #endregion
    }
  }
}