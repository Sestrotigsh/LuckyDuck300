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
    /// This low-key effect brings out the grays and greens in your game.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Ibuprogrames/Vintage/Vintage Brannan")]
    public sealed class VintageBrannan : VintageBase
    {
      #region Private data.
      private Texture2D processTex;
      private Texture2D blowoutTex;
      private Texture2D contrastTex;
      private Texture2D lumaTex;
      private Texture2D screenTex;

      private static readonly string variableProcessTex = @"_ProcessTex";
      private static readonly string variableBlowoutTex = @"_BlowoutTex";
      private static readonly string variableContrastTex = @"_ContrastTex";
      private static readonly string variableLumaTex = @"_LumaTex";
      private static readonly string variableScreenTex = @"_ScreenTex";
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"This low-key effect brings out the grays and greens in your game.";
      }
      #endregion

      #region Private functions.
      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected override void LoadCustomResources()
      {
        processTex = LoadTextureFromResources(@"Textures/brannanProcess");
        blowoutTex = LoadTextureFromResources(@"Textures/brannanBlowout");
        contrastTex = LoadTextureFromResources(@"Textures/brannanContrast");
        lumaTex = LoadTextureFromResources(@"Textures/brannanLuma");
        screenTex = LoadTextureFromResources(@"Textures/brannanScreen");        
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected override void SendValuesToShader()
      {
        material.SetTexture(variableProcessTex, processTex);
        material.SetTexture(variableBlowoutTex, blowoutTex);
        material.SetTexture(variableContrastTex, contrastTex);
        material.SetTexture(variableLumaTex, lumaTex);
        material.SetTexture(variableScreenTex, screenTex);        
      }
      #endregion
    }
  }
}