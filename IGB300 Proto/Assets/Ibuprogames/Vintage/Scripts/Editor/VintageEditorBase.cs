///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace Ibuprogames
{
  namespace VintageAsset
  {
    /// <summary>
    /// Vintage base editor.
    /// </summary>
    [CustomEditor(typeof(VintageBase))]
    public abstract class VintageEditorBase : Editor
    {
      #region Properties.
      #endregion

      #region Private data.
      private VintageBase baseTarget;

      private bool displayColorControls = false;
      private bool displayFilm = false;
      private bool displayCRT = false;

      private string displayColorControlsKey;
      private string displayFilmKey;
      private string displayCRTKey;
      #endregion

      #region Private functions.
      /// <summary>
      /// Custom inspector.
      /// </summary>
      protected virtual void Inspector()
      {
      }

      private void OnEnable()
      {
        string productID = this.GetType().ToString().Replace(@"Editor", string.Empty);

        displayColorControlsKey = string.Format("{0}.displayColorControls", productID);
        displayFilmKey = string.Format("{0}.displayFilm", productID);
        displayCRTKey = string.Format("{0}.displayCRT", productID);

        displayColorControls = EditorPrefs.GetInt(displayColorControlsKey, 0) == 1;
        displayFilm = EditorPrefs.GetInt(displayFilmKey, 0) == 1;
        displayCRT = EditorPrefs.GetInt(displayCRTKey, 0) == 1;

        baseTarget = this.target as VintageBase;
      }

      /// <summary>
      /// OnInspectorGUI.
      /// </summary>
      public override void OnInspectorGUI()
      {
        EditorHelper.Reset(0, 0.0f, 125.0f);

        Undo.RecordObject(baseTarget, baseTarget.GetType().Name);

        EditorHelper.BeginVertical();
        {
          /////////////////////////////////////////////////
          // Common.
          /////////////////////////////////////////////////
          
          EditorHelper.Separator();

          baseTarget.Strength = EditorHelper.Slider(@"Strength", "The strength of the effect.\nFrom 0.0 (no effect) to 1.0 (full effect).", baseTarget.Strength, 0.0f, 1.0f, 1.0f);

          baseTarget.EffectMode = (EffectModes)EditorHelper.EnumPopup(@"Mode", @"Screen, Layer or Depth mode. Default Screen.", baseTarget.EffectMode, EffectModes.Screen);

          if (baseTarget.EffectMode == EffectModes.Layer)
          {
            EditorHelper.IndentLevel++;

            baseTarget.Layer = EditorHelper.LayerMask(@"Layer mask", baseTarget.Layer, LayerMask.NameToLayer(@"Everything"));

            baseTarget.DepthThreshold = EditorHelper.Slider(@"Depth threshold", "Accuracy of depth texture.", baseTarget.DepthThreshold, 0.0f, 0.01f, 0.004f);

            EditorHelper.IndentLevel--;            
          }
          else if (baseTarget.EffectMode == EffectModes.Distance)
            baseTarget.DistanceCurve = EditorHelper.Curve(@"    Curve", baseTarget.DistanceCurve);

          Inspector();

          /////////////////////////////////////////////////
          // Color controls.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          baseTarget.EnableColorControls = EditorHelper.Header(ref displayColorControls, baseTarget.EnableColorControls, @"Color");
          if (displayColorControls == true)
          {
            EditorHelper.Enabled = baseTarget.EnableColorControls;

            EditorGUI.indentLevel++;

            baseTarget.Brightness = EditorHelper.Slider(@"Brightness", "Brightness [-1.0, 1.0]. Default 0.", baseTarget.Brightness, -1.0f, 1.0f, 0.0f);

            baseTarget.Contrast = EditorHelper.Slider(@"Contrast", "Contrast [-1.0, 1.0]. Default 0.", baseTarget.Contrast, -1.0f, 1.0f, 0.0f);

            baseTarget.Gamma = EditorHelper.Slider(@"Gamma", "Gamma [0.1, 10.0]. Default 1.", baseTarget.Gamma, 0.01f, 10.0f, 1.0f);

            if (baseTarget.GetType() != typeof(VintageInkwell))
            {
              baseTarget.Hue = EditorHelper.Slider(@"Hue", "The color wheel [0.0, 1.0]. Default 0.", baseTarget.Hue, 0.0f, 1.0f, 0.0f);

              baseTarget.Saturation = EditorHelper.Slider(@"Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", baseTarget.Saturation, 0.0f, 2.0f, 1.0f);
            }

            EditorGUI.indentLevel--;

            EditorHelper.Enabled = true;
          }

          /////////////////////////////////////////////////
          // Film.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          baseTarget.EnableFilm = EditorHelper.Header(ref displayFilm, baseTarget.EnableFilm, @"Old film");
          if (displayFilm == true)
          {
            EditorHelper.Enabled = baseTarget.EnableFilm;

            EditorHelper.IndentLevel++;

            baseTarget.FilmVignette = EditorHelper.Slider(@"Vignette", "Natural vignette.", baseTarget.FilmVignette, 0.0f, 2.0f, 0.1f);

            baseTarget.FilmGrainStrength = EditorHelper.Slider(@"Grain", "Film grain or granularity is noise texture due to the presence of small particles.\nFrom 0.0 (no grain) to 1.0 (full grain).", baseTarget.FilmGrainStrength, 0.0f, 1.0f, 0.2f);

            baseTarget.FilmScratches = EditorHelper.Slider(@"Scratches", baseTarget.FilmScratches, 0.0f, 1.0f, 0.5f);

            // Blink.
            EditorHelper.Label(@"Blink");

            EditorHelper.IndentLevel++;

            baseTarget.FilmBlinkStrenght = EditorHelper.Slider(@"Strenght", "Brightness variation.\nFrom 0.0 (no fluctuation) to 1.0 (full epilepsy).", baseTarget.FilmBlinkStrenght, 0.0f, 1.0f, 0.0f);

            baseTarget.FilmBlinkSpeed = EditorHelper.Slider(@"Speed", baseTarget.FilmBlinkSpeed, 0.0f, 250.0f, 50.0f);

            EditorHelper.IndentLevel--;

            // Blotches.
            EditorHelper.Label(@"Blotches");

            EditorHelper.IndentLevel++;

            baseTarget.FilmBlotches = EditorHelper.IntSlider(@"Count", baseTarget.FilmBlotches, 0, 6, 3);

            baseTarget.FilmBlotchSize = EditorHelper.Slider(@"Size", baseTarget.FilmBlotchSize, 0.0f, 10.0f, 1.0f);

            EditorHelper.IndentLevel--;

            // Lines.
            EditorHelper.Label(@"Lines");

            EditorHelper.IndentLevel++;

            baseTarget.FilmLines = EditorHelper.IntSlider(@"Count", baseTarget.FilmLines, 0, 8, 4);

            baseTarget.FilmLinesStrength = EditorHelper.Slider(@"Strength", baseTarget.FilmLinesStrength, 0.0f, 1.0f, 0.25f);

            EditorHelper.IndentLevel--;

            EditorHelper.IndentLevel--;

            EditorHelper.Enabled = true;
          }

          /////////////////////////////////////////////////
          // CRT.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          baseTarget.EnableCRT = EditorHelper.Header(ref displayCRT, baseTarget.EnableCRT, @"CRT");
          if (displayCRT == true)
          {
            EditorHelper.Enabled = baseTarget.EnableCRT;

            EditorHelper.IndentLevel++;

            EditorHelper.Label(@"Vignette");

            EditorHelper.IndentLevel++;

            baseTarget.CRTVignette = EditorHelper.Slider(@"Strength", baseTarget.CRTVignette, 0.0f, 1.0f, 1.0f);

            baseTarget.CRTVignetteAperture = EditorHelper.Slider(@"Aperture", baseTarget.CRTVignetteAperture, 0.0f, 1.0f, 1.0f);

            EditorHelper.IndentLevel--;

            EditorHelper.Label(@"Scanlines");

            EditorHelper.IndentLevel++;

            baseTarget.CRTScanLine = EditorHelper.Slider(@"Lines", @"Tiny scanlines.", baseTarget.CRTScanLine, 0.0f, 2.0f, 1.0f);

            baseTarget.CRTSlowScan = EditorHelper.Slider(@"Slow moving", baseTarget.CRTSlowScan, 0.0f, 1.0f, 0.01f);

            baseTarget.CRTScanDistort = EditorHelper.Slider(@"Distort bar", @"Scanline distortion.", baseTarget.CRTScanDistort, 0.0f, 1.0f, 0.3f);

            EditorHelper.IndentLevel--;

            baseTarget.CRTColorShift = EditorHelper.Slider(@"Color shift", baseTarget.CRTColorShift, 0.0f, 1.0f, 0.25f);

            EditorHelper.Label(@"Reflexion");

            EditorHelper.IndentLevel++;

            baseTarget.CRTReflexionShine = EditorHelper.Slider(@"Shine", baseTarget.CRTReflexionShine, 0.0f, 1.0f, 0.5f);

            baseTarget.CRTReflexionAmbient = EditorHelper.Slider(@"Ambient", baseTarget.CRTReflexionAmbient, 0.0f, 1.0f, 0.25f);

            EditorHelper.IndentLevel--;

            EditorHelper.IndentLevel--;

            EditorHelper.Enabled = true;
          }

          /////////////////////////////////////////////////
          // Description.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          EditorGUILayout.HelpBox(baseTarget.ToString(), MessageType.Info);

          /////////////////////////////////////////////////
          // Misc.
          /////////////////////////////////////////////////

          EditorHelper.Separator();

          EditorHelper.BeginHorizontal();
          {
            if (GUILayout.Button(new GUIContent(@"[doc]", @"Online documentation"), GUI.skin.label) == true)
              Application.OpenURL(@"http://www.ibuprogames.com/2015/05/04/vintage-image-efffects/");

            EditorHelper.FlexibleSpace();

            if (EditorHelper.Button(@"Reset") == true)
              baseTarget.ResetDefaultValues();
          }
          EditorHelper.EndHorizontal();
        }
        EditorHelper.EndVertical();

        EditorHelper.Separator();

        if (EditorHelper.Changed == true)
        {
          EditorPrefs.SetInt(displayColorControlsKey, displayColorControls == true ? 1 : 0);
          EditorPrefs.SetInt(displayFilmKey, displayFilm == true ? 1 : 0);
          EditorPrefs.SetInt(displayCRTKey, displayCRT == true ? 1 : 0);

          EditorHelper.SetDirty(target);
        }
      }      
      #endregion
    }
  }
}