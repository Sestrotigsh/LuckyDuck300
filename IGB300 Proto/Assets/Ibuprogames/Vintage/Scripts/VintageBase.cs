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
    /// Base effect.
    /// </summary>
    [HelpURL("http://www.ibuprogames.com/2015/05/04/vintage-image-efffects/")]
    public abstract class VintageBase : MonoBehaviour
    {
      #region Properties.
      /// <summary>
      /// Strength of the effect [0, 1]. Default 1.
      /// </summary>
      public float Strength
      {
        get { return strength; }
        set { strength = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Screen or Layer mode. Default Screen.
      /// </summary>
      public EffectModes EffectMode
      {
        get { return effectMode; }
        set
        {
          if (value != effectMode)
          {
            effectMode = value;

            if (effectMode == EffectModes.Screen)
            {
              this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;

              DestroyDepthCamera();
            }
            else
            {
              this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;

              CreateDepthCamera();
            }
          }
        }
      }

      /// <summary>
      /// The layer to which the effect affects. Used in EffectMode.Layer. Default 'Everything'.
      /// </summary>
      public LayerMask Layer
      {
        get { return layer; }
        set
        {
          layer = value;

          if (renderDepth != null)
            renderDepth.layer = layer;
        }
      }

      /// <summary>
      /// Accuracy of depth texture [0.0, 0.01]. Used in EffectMode.Layer. Default 0.004.
      /// </summary>
      public float DepthThreshold
      {
        get { return depthThreshold; }
        set { depthThreshold = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Effect strength curve. Used in EffectMode.Distance.
      /// </summary>
      public AnimationCurve DistanceCurve
      {
        get { return distanceCurve; }
        set
        {
          distanceCurve = value;

          UpdateDistanceCurveTexture();
        }
      }

      /// <summary>
      /// Enable color controls (Brightness, Contrast, Gamma, Hue and Saturation).
      /// </summary>
      public bool EnableColorControls
      {
        get { return enableColorControls; }
        set { enableColorControls = value; }
      }

      /// <summary>
      /// Brightness [-1.0, 1.0]. Default 0.
      /// </summary>
      public float Brightness
      {
        get { return brightness; }
        set { brightness = Mathf.Clamp(value, -1.0f, 1.0f); }
      }

      /// <summary>
      /// Contrast [-1.0, 1.0]. Default 0.
      /// </summary>
      public float Contrast
      {
        get { return contrast; }
        set { contrast = Mathf.Clamp(value, -1.0f, 1.0f); }
      }

      /// <summary>
      /// Gamma [0.1, 10.0]. Default 1.
      /// </summary>
      public float Gamma
      {
        get { return gamma; }
        set { gamma = Mathf.Clamp(value, 0.1f, 10.0f); }
      }

      /// <summary>
      /// The color wheel [0.0, 1.0]. Default 0.
      /// </summary>
      public float Hue
      {
        get { return hue; }
        set { hue = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Intensity of a colors [0.0, 2.0]. Default 1.
      /// </summary>
      public float Saturation
      {
        get { return saturation; }
        set { saturation = Mathf.Clamp(value, 0.0f, 2.0f); }
      }

      /// <summary>
      /// Enable old film effect.
      /// </summary>
      public bool EnableFilm
      {
        get { return enableFilm; }
        set { enableFilm = value; }
      }

      /// <summary>
      /// Natural vignette [0, 2]. Default 0.1.
      /// </summary>
      public float FilmVignette
      {
        get { return filmVignette; }
        set { filmVignette = Mathf.Clamp(value, 0.0f, 2.0f); }
      }

      /// <summary>
      /// Grain [0.0 - 1.0]. Default 0.2.
      /// </summary>
      public float FilmGrainStrength
      {
        get { return filmGrainStrength; }
        set { filmGrainStrength = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Blink strength [0.0 - 1.0]. Default 0.
      /// </summary>
      public float FilmBlinkStrenght
      {
        get { return filmBlinkStrenght; }
        set { filmBlinkStrenght = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Blink speed. Default 50.
      /// </summary>
      public float FilmBlinkSpeed
      {
        get { return filmBlinkSpeed; }
        set { filmBlinkSpeed = value; }
      }

      /// <summary>
      /// Film scratches. [0.0 - 1.0]. Default 0.5.
      /// </summary>
      public float FilmScratches
      {
        get { return filmScratches; }
        set { filmScratches = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Blotches [0 - 6]. Default 3.
      /// </summary>
      public int FilmBlotches
      {
        get { return filmBlotches; }
        set { filmBlotches = value; }
      }

      /// <summary>
      /// Blotch size [0.0 - 10.0]. Default 1.0.
      /// </summary>
      public float FilmBlotchSize
      {
        get { return filmBlotchSize; }
        set { filmBlotchSize = Mathf.Clamp(value, 0.0f, 10.0f); }
      }

      /// <summary>
      /// Lines [0 - 8]. Default 4.
      /// </summary>
      public int FilmLines
      {
        get { return filmLines; }
        set { filmLines = value; }
      }

      /// <summary>
      /// Lines strength [0.0 - 1.0]. Default 0.25.
      /// </summary>
      public float FilmLinesStrength
      {
        get { return filmLinesStrength; }
        set { filmLinesStrength = value; }
      }

      /// <summary>
      /// Enable CRT effect.
      /// </summary>
      public bool EnableCRT
      {
        get { return enableCRT; }
        set { enableCRT = value; }
      }

      /// <summary>
      /// Tiny scanlines [0.0 - 2.0]. Default 1.0.
      /// </summary>
      public float CRTScanLine
      {
        get { return crtScanLine; }
        set { crtScanLine = Mathf.Clamp(value, 0.0f, 2.0f); }
      }

      /// <summary>
      /// Slow moving scanlines [0.0 - 1.0]. Default 0.01.
      /// </summary>
      public float CRTSlowScan
      {
        get { return crtSlowScan; }
        set { crtSlowScan = Mathf.Clamp01(value); }
      }

      /// <summary>
      /// Scanline distortion [0.0 - 1.0]. Default 0.3.
      /// </summary>
      public float CRTScanDistort
      {
        get { return crtScanDistort; }
        set { crtScanDistort = Mathf.Clamp(value, 0.0f, 1.0f); }
      }

      /// <summary>
      /// CRT vignette [0.0 - 1.0]. Default 1.0.
      /// </summary>
      public float CRTVignette
      {
        get { return crtVignette; }
        set { crtVignette = Mathf.Clamp(value, 0.0f, 1.0f); }
      }

      /// <summary>
      /// CRT vignette aperture [0.0 - 1.0]. Default 1.0.
      /// </summary>
      public float CRTVignetteAperture
      {
        get { return crtVignetteAperture; }
        set { crtVignetteAperture = Mathf.Clamp(value, 0.0f, 1.0f); }
      }

      /// <summary>
      /// CRT color shift [0.0 - 1.0]. Default 0.25.
      /// </summary>
      public float CRTColorShift
      {
        get { return crtColorShift; }
        set { crtColorShift = Mathf.Clamp(value, 0.0f, 1.0f); }
      }

      /// <summary>
      /// CRT top reflexion of the screen.
      /// </summary>
      public float CRTReflexionShine
      {
        get { return crtReflexionShine; }
        set { crtReflexionShine = Mathf.Clamp(value, 0.0f, 1.0f); }
      }

      /// <summary>
      /// CRT ambient reflexion of the screen.
      /// </summary>
      public float CRTReflexionAmbient
      {
        get { return crtReflexionAmbient; }
        set { crtReflexionAmbient = Mathf.Clamp(value, 0.0f, 1.0f); }
      }
      #endregion

      #region Private data.
      [SerializeField]
      private float strength = 1.0f;

      [SerializeField]
      private EffectModes effectMode = EffectModes.Screen;

      [SerializeField]
      private bool enableColorControls = false;

      [SerializeField]
      private float brightness = 0.0f;

      [SerializeField]
      private float contrast = 0.0f;

      [SerializeField]
      private float gamma = 1.0f;

      [SerializeField]
      private float hue = 0.0f;

      [SerializeField]
      private float saturation = 1.0f;

      [SerializeField]
      private bool enableFilm = false;

      [SerializeField]
      private float filmVignette = 0.1f;

      [SerializeField]
      private float filmGrainStrength = 0.2f;

      [SerializeField]
      private float filmBlinkStrenght = 0.0f;

      [SerializeField]
      private float filmBlinkSpeed = 50.0f;

      [SerializeField]
      private float filmScratches = 0.5f;

      [SerializeField]
      private int filmBlotches = 3;

      [SerializeField]
      private float filmBlotchSize = 1.0f;

      [SerializeField]
      private int filmLines = 4;

      [SerializeField]
      private float filmLinesStrength = 0.25f;

      [SerializeField]
      private bool enableCRT = false;

      [SerializeField]
      private float crtScanLine = 1.0f;

      [SerializeField]
      private float crtSlowScan = 0.01f;

      [SerializeField]
      private float crtScanDistort = 0.3f;
      
      [SerializeField]
      private float crtVignette = 1.0f;

      [SerializeField]
      private float crtVignetteAperture = 1.0f;

      [SerializeField]
      private float crtColorShift = 0.25f;

      [SerializeField]
      private float crtReflexionShine = 0.5f;

      [SerializeField]
      private float crtReflexionAmbient = 0.25f;

      [SerializeField]
      private LayerMask layer = -1;

      [SerializeField]
      private AnimationCurve distanceCurve = new AnimationCurve(new Keyframe(1.0f, 0.0f, 0.0f, 0.0f), new Keyframe(0.0f, 1.0f, 0.0f, 0.0f));

      [SerializeField]
      private RenderDepth renderDepth;

      [SerializeField]
      private float depthThreshold = 0.004f;

      protected Material material;

      private Shader shader;

      private Texture2D noiseTex;

      private Texture2D distanceTexture;

      private static readonly string variableStrength = @"_Strength";
      private static readonly string variableRandom = @"_RandomValue";
      private static readonly string variableNoiseTex = @"_NoiseTex";
      private static readonly string variableVignette = @"_Vignette";
      private static readonly string variableBrightness = @"_Brightness";
      private static readonly string variableContrast = @"_Contrast";
      private static readonly string variableGamma = @"_Gamma";
      private static readonly string variableHue = @"_Hue";
      private static readonly string variableSaturation = @"_Saturation";
      private static readonly string variableFilmGrainStrength = @"_FilmGrainStrength";
      private static readonly string variableFilmBlinkStrenght = @"_FilmBlinkStrenght";
      private static readonly string variableFilmBlinkSpeed = @"_FilmBlinkSpeed";
      private static readonly string variableFilmBlotches = @"_FilmBlotches";
      private static readonly string variableFilmBlotchSize = @"_FilmBlotchSize";
      private static readonly string variableFilmScratches = @"_FilmScratches";
      private static readonly string variableFilmLines = @"_FilmLines";
      private static readonly string variableFilmLinesStrength = @"_FilmLinesStrength";
      private static readonly string variableRenderToTexture = @"_RTT";
      private static readonly string variableDepthThreshold = @"_DepthThreshold";
      private static readonly string variableScanLine = @"_ScanLine";
      private static readonly string variableSlowScan = @"_SlowScan";
      private static readonly string variableScanDistort = @"_ScanDistort";
      private static readonly string variableCRTVignette = @"_CRTVignette";
      private static readonly string variableCRTVignetteAperture = @"_CRTVignetteAperture";
      private static readonly string variableCRTColorShift = @"_ColorShift";
      private static readonly string variableCRTReflexionShine = @"_ReflexionShine";
      private static readonly string variableCRTReflexionAmbient = @"_ReflexionAmbient";
      private static readonly string variableDistanceTexture = @"_DistanceTex";

      private static readonly string keywordLinear = @"LINEAR_SPACE";
      private static readonly string keywordModeScreen = @"MODE_SCREEN";
      private static readonly string keywordModeLayer = @"MODE_LAYER";
      private static readonly string keywordModeDistance = @"MODE_DISTANCE";

      private static readonly string keywordColorControls = @"COLOR_CONTROLS";
      private static readonly string keywordFilm = @"FILM_ENABLED";
      private static readonly string keywordCRT = @"CRT_ENABLED";
      #endregion

      #region Public functions.
      /// <summary>
      /// Effect supported by the current hardware?
      /// </summary>
      public bool IsSupported()
      {
        bool supported = false;
        
        if (SystemInfo.supportsImageEffects == true)
        {
          string shaderPath = ShaderPath();

          Shader test = Resources.Load<Shader>(shaderPath);
          if (test != null)
          {
            supported = test.isSupported == true && CheckHardwareRequirements() == true;

            Resources.UnloadAsset(test);
          }
        }

        if (supported == true && (effectMode == EffectModes.Layer || effectMode == EffectModes.Distance))
          supported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth);

        return supported;
      }

      /// <summary>
      /// Reset to default values.
      /// </summary>
      public virtual void ResetDefaultValues()
      {
        strength = 1.0f;

        brightness = 0.0f;
        contrast = 0.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        filmVignette = 0.1f;
        filmGrainStrength = 0.2f;
        filmBlinkStrenght = 0.0f;
        filmBlinkSpeed = 50.0f;
        filmScratches = 0.5f;
        filmBlotches = 3;
        filmBlotchSize = 1.0f;
        filmLines = 4;
        filmLinesStrength = 0.25f;

        crtScanLine = 1.0f;
        crtSlowScan = 0.01f;
        crtScanDistort = 0.3f;
        crtVignette = 1.0f;
        crtVignetteAperture = 1.0f;
        crtColorShift = 0.25f;
        crtReflexionShine = 0.5f;
        crtReflexionAmbient = 0.25f;

        depthThreshold = 0.004f;

        distanceCurve = new AnimationCurve(new Keyframe(1.0f, 0.0f, 0.0f, 0.0f), new Keyframe(0.0f, 1.0f, 0.0f, 0.0f));

        layer = LayerMask.NameToLayer(@"Everything");
      }

      /// <summary>
      /// Effect description.
      /// </summary>
      public override string ToString()
      {
        return @"No description.";
      }
      #endregion

      #region Private functions.
      protected virtual string ShaderPath()
      {
        return string.Format("Shaders/{0}", this.GetType().Name);
      }

      private Material Material
      {
        get
        {
          if (material == null && shader != null)
          {
            string materialName = this.GetType().Name;

            material = new Material(shader);
            if (material != null)
            {
              material.name = materialName;
              material.hideFlags = HideFlags.HideAndDontSave;
            }
            else
            {
              Debug.LogErrorFormat("[Ibuprogames.Vintage] '{0}' material null. Please contact with 'hello@ibuprogames.com' and send the log file.", materialName);

              this.enabled = false;
            }
          }

          return material;
        }
      }

      private void CreateDepthCamera()
      {
        if (renderDepth == null)
        {
          GameObject go = new GameObject(@"VintageDepthCamera", typeof(Camera));
          go.hideFlags = HideFlags.HideAndDontSave;
          go.transform.parent = this.transform;
          go.transform.localPosition = Vector3.zero;
          go.transform.localRotation = Quaternion.identity;
          go.transform.localScale = Vector3.one;

          renderDepth = go.AddComponent<RenderDepth>();
          renderDepth.layer = layer;
        }
      }

      private void DestroyDepthCamera()
      {
        if (renderDepth != null)
        {
          GameObject obj = renderDepth.gameObject;
          renderDepth = null;

          DestroyImmediate(obj);
        }
      }

      /// <summary>
      /// Set the values to shader.
      /// </summary>
      protected abstract void SendValuesToShader();

      /// <summary>
      /// Custom hardware requirements.
      /// </summary>
      protected virtual bool CheckHardwareRequirements()
      {
        if ((effectMode == EffectModes.Layer || effectMode == EffectModes.Distance) && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth) == false)
        {
          Debug.LogWarning(@"[Ibuprogames.Vintage] Depth textures aren't supported on this device.");

          return false;
        }

        return true;
      }

      /// <summary>
      /// Load custom resources.
      /// </summary>
      protected virtual void LoadCustomResources() { }

      /// <summary>
      /// Load texture from "Resources/Textures". Internal use.
      /// </summary>
      protected Texture2D LoadTextureFromResources(string texturePathFromResources)
      {
        Texture2D texture = Resources.Load<Texture2D>(texturePathFromResources);
        if (texture != null)
          texture.wrapMode = TextureWrapMode.Clamp;
        else
        {
          Debug.LogWarningFormat("[Ibuprogames.Vintage] Texture '{0}' not found in 'Ibuprogames/Vintage/Resources/Textures' folder. Please contact with 'hello@ibuprogames.com' and send the log file.", texturePathFromResources);

          this.enabled = false;
        }

        return texture;
      }

      private void UpdateDistanceCurveTexture()
      {
        if (distanceTexture == null)
        {
          distanceTexture = new Texture2D(1024, 2);
          distanceTexture.filterMode = FilterMode.Bilinear;
          distanceTexture.wrapMode = TextureWrapMode.Clamp;
          distanceTexture.anisoLevel = 1;
        }

        float step = 1.0f / (float)distanceTexture.width;
        for (int i = 0; i < distanceTexture.width; ++i)
        {
          Color color = Color.white * Mathf.Clamp01(distanceCurve.Evaluate((float)i * step));
          
          distanceTexture.SetPixel(i, 0, color);
          distanceTexture.SetPixel(i, 1, color);
        }

        distanceTexture.Apply();
      }

      /// <summary>
      /// Called on the frame when a script is enabled just before any of the Update methods is called the first time.
      /// </summary>
      private void Start()
      {
        if (SystemInfo.supportsImageEffects == true)
        {
          string shaderPath = ShaderPath();

          shader = Resources.Load<Shader>(shaderPath);
          if (shader != null)
          {
            if (shader.isSupported == true && CheckHardwareRequirements() == true)
            {
              noiseTex = LoadTextureFromResources(@"Textures/Noise256");

              LoadCustomResources();
            }
            else
            {
              Debug.LogWarningFormat("[Ibuprogames.Vintage] '{0}' shader not supported. Please contact with 'hello@ibuprogames.com' and send the log file.", shaderPath);

              this.enabled = false;
            }
          }
          else
          {
            Debug.LogWarningFormat("[Ibuprogames.Vintage] Shader 'Ibuprogames/Vintage/Resources/{0}.shader' not found. '{1}' disabled.", shaderPath, this.GetType().Name);

            this.enabled = false;
          }
        }
        else
        {
          Debug.LogWarningFormat("[Ibuprogames.Vintage] Hardware not support Image Effects. '{0}' disabled.", this.GetType().Name);

          this.enabled = false;
        }
      }

      /// <summary>
      /// Called when the object becomes enabled and active.
      /// </summary>
      private void OnEnable()
      {
        if (effectMode != EffectModes.Screen && renderDepth == null)
          CreateDepthCamera();

        Camera effectCamera = this.GetComponent<Camera>();
        effectCamera.depthTextureMode = (effectMode == EffectModes.Screen ? DepthTextureMode.None : DepthTextureMode.Depth);
      }

      /// <summary>
      /// When the MonoBehaviour will be destroyed.
      /// </summary>
      protected virtual void OnDestroy()
      {
        if (material != null)
#if UNITY_EDITOR
          DestroyImmediate(material);
#else
				  Destroy(material);
#endif
      }

      /// <summary>
      /// Called after all rendering is complete to render image.
      /// </summary>
      private void OnRenderImage(RenderTexture source, RenderTexture destination)
      {
        if (Material != null)
        {
          material.shaderKeywords = null;

          material.SetFloat(variableStrength, strength);
          material.SetTexture(variableNoiseTex, noiseTex);
          material.SetVector(variableRandom, new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));

          if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            material.EnableKeyword(keywordLinear);

          switch (effectMode)
          {
            case EffectModes.Screen:
              material.EnableKeyword(keywordModeScreen);
              break;
            case EffectModes.Layer:
              material.EnableKeyword(keywordModeLayer);

              if (renderDepth != null)
                material.SetTexture(variableRenderToTexture, renderDepth.renderTexture);

              material.SetFloat(variableDepthThreshold, depthThreshold);
              break;
            case EffectModes.Distance:
              material.EnableKeyword(keywordModeDistance);

              if (distanceTexture == null)
                UpdateDistanceCurveTexture();

              material.SetTexture(variableDistanceTexture, distanceTexture);
              break;
          }

          if (enableColorControls == true && strength > 0.0f)
          {
            material.EnableKeyword(keywordColorControls);

            material.SetFloat(variableBrightness, brightness);
            material.SetFloat(variableContrast, contrast);
            material.SetFloat(variableGamma, 1.0f / gamma);
            material.SetFloat(variableHue, hue);
            material.SetFloat(variableSaturation, saturation);
          }

          if (enableFilm == true && strength > 0.0f)
          {
            material.EnableKeyword(keywordFilm);

            material.SetFloat(variableVignette, filmVignette);
            material.SetFloat(variableFilmGrainStrength, filmGrainStrength);
            material.SetFloat(variableFilmBlinkStrenght, filmBlinkStrenght * 0.1f);
            material.SetFloat(variableFilmBlinkSpeed, filmBlinkSpeed);
            material.SetFloat(variableFilmScratches, filmScratches);
            material.SetInt(variableFilmBlotches, filmBlotches);
            material.SetFloat(variableFilmBlotchSize, filmBlotchSize);
            material.SetInt(variableFilmLines, filmLines);
            material.SetFloat(variableFilmLinesStrength, filmLinesStrength / 8.0f);
          }

          if (enableCRT == true && strength > 0.0f)
          {
            material.EnableKeyword(keywordCRT);

            material.SetFloat(variableScanLine, crtScanLine);
            material.SetFloat(variableSlowScan, crtSlowScan);
            material.SetFloat(variableScanDistort, crtScanDistort * 0.1f);
            material.SetFloat(variableCRTVignette, crtVignette);
            material.SetFloat(variableCRTVignetteAperture, crtVignetteAperture);
            material.SetFloat(variableCRTColorShift, crtColorShift);
            material.SetFloat(variableCRTReflexionShine, crtReflexionShine);
            material.SetFloat(variableCRTReflexionAmbient, crtReflexionAmbient);
          }

          SendValuesToShader();

          Graphics.Blit(source, destination, material, 0);
        }
        else
          Graphics.Blit(source, destination);
      }    
      #endregion
    }
  }
}