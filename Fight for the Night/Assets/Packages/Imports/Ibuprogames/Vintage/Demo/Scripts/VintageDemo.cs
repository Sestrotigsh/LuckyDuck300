///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames <hello@ibuprogames.com>. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using Ibuprogames.VintageAsset;

/// <summary>
/// Vintage demo.
/// </summary>
public sealed class VintageDemo : MonoBehaviour
{
  [SerializeField]
  private bool guiShow = true;

  [SerializeField]
  private float slideEffectTime = 3.0f;

  private bool menuOpen = false;

  private const float guiMargen = 10.0f;
  private const float guiWidth = 250.0f;

  private float updateInterval = 0.5f;
  private float accum = 0.0f;
  private int frames = 0;
  private float timeleft;
  private float fps = 0.0f;

  private GUIStyle menuStyle;
  private GUIStyle boxStyle;
  private GUIStyle descriptionStyle;

  private Vector2 scrollPosition = Vector2.zero;

  private float effectTime = 0.0f;

  private List<VintageBase> vintageEffects = new List<VintageBase>();

  private int effectSelected = 0;
  private float effectSelectedStrength = 1.0f;
  private bool effectSelectedOldFilm = false;
  private bool effectSelectedCRT = false;
  private bool enableCompare = false;

  private void OnEnable()
  {
    Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
    Camera selectedCamera = null;

    for (int i = 0; i < cameras.Length && selectedCamera == null; ++i)
    {
      if (cameras[i].enabled == true)
        selectedCamera = cameras[i];
    }

    if (selectedCamera != null)
    {
      VintageBase[] effects = selectedCamera.gameObject.GetComponents<VintageBase>();
      if (effects.Length > 0)
      {
        for (int i = 0; i < effects.Length; ++i)
        {
          if (effects[i].IsSupported() == true)
            vintageEffects.Add(effects[i]);
          else
            effects[i].enabled = false;
        }
      }
      else
      {
        System.Type[] types = Assembly.GetAssembly(typeof(VintageBase)).GetTypes();
        for (int i = 0; i < types.Length; ++i)
        {
          if (types[i].IsClass == true && types[i].IsAbstract == false && (types[i].IsSubclassOf(typeof(VintageBase)) == true || types[i].IsSubclassOf(typeof(VintageLutBase)) == true))
          {
            VintageBase vintageEffect = selectedCamera.gameObject.AddComponent(types[i]) as VintageBase;
            if (vintageEffect.IsSupported() == true)
              vintageEffects.Add(vintageEffect);
            else
              Destroy(vintageEffect);
          }
        }
      }

      if (effectSelected > vintageEffects.Count - 1)
        effectSelected = 0;        

      for (int i = 0; i < vintageEffects.Count; ++i)
        vintageEffects[i].enabled = (i == effectSelected);
    }
    else
      Debug.LogWarning(@"No camera found.");

    this.enabled = vintageEffects.Count > 0;
  }

  private void Update()
  {
    timeleft -= Time.deltaTime;
    accum += Time.timeScale / Time.deltaTime;
    frames++;

    if (timeleft <= 0.0f)
    {
      fps = accum / frames;
      timeleft = updateInterval;
      accum = 0.0f;
      frames = 0;
    }

    if (slideEffectTime > 0.0f && vintageEffects.Count > 0)
    {
      effectTime += Time.deltaTime;
      if (effectTime >= slideEffectTime)
      {
        vintageEffects[effectSelected].enabled = false;

        effectSelected = (effectSelected < (vintageEffects.Count - 1) ? effectSelected + 1 : 0);

        EnableEffect(effectSelected);

        effectTime = 0.0f;
      }
    }

    if (Input.GetKeyUp(KeyCode.Tab) == true)
      guiShow = !guiShow;

    if (Input.GetKeyUp(KeyCode.KeypadPlus) == true || Input.GetKeyUp(KeyCode.KeypadMinus) == true ||
        Input.GetKeyUp(KeyCode.PageUp) == true || Input.GetKeyUp(KeyCode.PageDown) == true ||
        Input.GetKeyUp(KeyCode.LeftArrow) == true || Input.GetKeyUp(KeyCode.RightArrow) == true ||
        Input.GetKeyUp(KeyCode.UpArrow) == true || Input.GetKeyUp(KeyCode.DownArrow) == true)
    {
      int effectSelected = 0;

      slideEffectTime = 0.0f;

      for (int i = 0; i < vintageEffects.Count; ++i)
      {
        if (vintageEffects[i].enabled == true)
        {
          vintageEffects[i].enabled = false;

          effectSelected = i;

          break;
        }
      }

      if (Input.GetKeyUp(KeyCode.KeypadPlus) == true || Input.GetKeyUp(KeyCode.PageUp) == true ||
          Input.GetKeyUp(KeyCode.RightArrow) == true || Input.GetKeyUp(KeyCode.UpArrow) == true)
      {
        slideEffectTime = 0.0f;

        if (effectSelected < vintageEffects.Count - 1)
          effectSelected++;
        else
          effectSelected = 0;

        EnableEffect(effectSelected);
      }

      if (Input.GetKeyUp(KeyCode.KeypadMinus) == true || Input.GetKeyUp(KeyCode.PageDown) == true ||
          Input.GetKeyUp(KeyCode.LeftArrow) == true || Input.GetKeyUp(KeyCode.DownArrow) == true)
      {
        slideEffectTime = 0.0f;

        if (effectSelected > 0)
          effectSelected--;
        else
          effectSelected = vintageEffects.Count - 1;

        EnableEffect(effectSelected);
      }
    }

#if !UNITY_WEBGL
    if (Input.GetKeyDown(KeyCode.Escape))
      Application.Quit();
#endif
  }

  private void OnGUI()
  {
    if (vintageEffects.Count == 0 || guiShow == false)
      return;

    if (menuStyle == null)
    {
      menuStyle = new GUIStyle(GUI.skin.textArea);
      menuStyle.richText = true;
      menuStyle.alignment = TextAnchor.MiddleCenter;
      menuStyle.fontSize = 24;
    }

    if (boxStyle == null)
    {
      boxStyle = new GUIStyle(GUI.skin.box);
      boxStyle.normal.background = MakeTex(2, 2, new Color(0.5f, 0.5f, 0.5f, 0.5f));
      boxStyle.focused.textColor = Color.red;
    }

    if (descriptionStyle == null)
    {
      descriptionStyle = new GUIStyle(GUI.skin.box);
      descriptionStyle.alignment = TextAnchor.MiddleCenter;
      descriptionStyle.wordWrap = true;
      descriptionStyle.fontSize = 32;
    }

    GUILayout.BeginVertical(GUILayout.Height(Screen.height));
    {
      GUILayout.BeginHorizontal(boxStyle, GUILayout.Width(Screen.width));
      {
        GUILayout.Space(guiMargen);

        if (GUILayout.Button("MENU", menuStyle, GUILayout.Width(80.0f)) == true)
          menuOpen = !menuOpen;

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("<<<", menuStyle) == true)
        {
          slideEffectTime = 0.0f;

          if (effectSelected > 0)
            effectSelected--;
          else
            effectSelected = vintageEffects.Count - 1;
        }

        string vintageEffectName = @"NONE";
        for (int i = 0; i < vintageEffects.Count && string.Compare(vintageEffectName, @"NONE") == 0; ++i)
          vintageEffectName = vintageEffects[i].enabled == true ? EffectName(vintageEffects[i].GetType().ToString()) : @"NONE";

        if (string.IsNullOrEmpty(vintageEffectName) == false)
          GUILayout.Label(vintageEffectName, menuStyle, GUILayout.Width(200.0f));

        if (GUILayout.Button(">>>", menuStyle) == true)
        {
          slideEffectTime = 0.0f;

          if (effectSelected < vintageEffects.Count - 1)
            effectSelected++;
          else
            effectSelected = 0;
        }

        GUILayout.FlexibleSpace();
#if false
        if (GUILayout.Button(@"MUTE", menuStyle) == true)
          AudioListener.volume = 1.0f - AudioListener.volume;
#endif
        GUILayout.Space(guiMargen);

        if (fps < 24.0f)
          GUI.contentColor = Color.yellow;
        else if (fps < 15.0f)
          GUI.contentColor = Color.red;
        else
          GUI.contentColor = Color.green;

        GUILayout.Label(fps.ToString(@"000"), menuStyle, GUILayout.Width(60.0f));

        GUI.contentColor = Color.white;

        GUILayout.Space(guiMargen);
      }
      GUILayout.EndHorizontal();

      for (int i = 0; i < vintageEffects.Count; ++i)
      {
        VintageBase vintageEffect = vintageEffects[i];

        if (effectSelected == i && vintageEffect.enabled == false)
          EnableEffect(effectSelected);

        if (vintageEffect.enabled == true && effectSelected != i)
          vintageEffect.enabled = false;
      }

      if (menuOpen == true)
        MenuGUI();

      GUILayout.FlexibleSpace();

      GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
      {
        GUILayout.Space(guiMargen);

        if (effectSelected >= 0 && effectSelected < vintageEffects.Count)
          GUILayout.Label(vintageEffects[effectSelected].ToString(), descriptionStyle, GUILayout.ExpandWidth(true));

        GUILayout.Space(guiMargen);
      }
      GUILayout.EndHorizontal();

      GUILayout.Space(guiMargen);
    }
    GUILayout.EndVertical();
  }

  private void MenuGUI()
  {
    GUILayout.BeginVertical(boxStyle, GUILayout.Width(guiWidth));
    {
      GUILayout.Space(guiMargen);

      // Common controls.
      if (effectSelected >= 0 && effectSelected < vintageEffects.Count)
      {
        VintageBase currentEffect = vintageEffects[effectSelected];

        GUILayout.BeginVertical(boxStyle);
        {
          GUILayout.BeginHorizontal();
          {
            GUILayout.Label(@"Strength", GUILayout.Width(70));
            effectSelectedStrength = GUILayout.HorizontalSlider(currentEffect.Strength, 0.0f, 1.0f);
          }
          GUILayout.EndHorizontal();

          GUILayout.BeginHorizontal();
          {
            effectSelectedOldFilm = GUILayout.Toggle(effectSelectedOldFilm, @"  Old Film");

            GUILayout.FlexibleSpace();

            effectSelectedCRT = GUILayout.Toggle(effectSelectedCRT, @"  CRT");

            GUILayout.Space(10.0f);
          }
          GUILayout.EndHorizontal();

          enableCompare = GUILayout.Toggle(enableCompare, @"  Compare");
          if (enableCompare == true)
            Shader.EnableKeyword(@"ENABLE_DEMO");
          else
            Shader.DisableKeyword(@"ENABLE_DEMO");
        }
        GUILayout.EndVertical();

        currentEffect.Strength = effectSelectedStrength;
        currentEffect.EnableFilm = effectSelectedOldFilm;
        currentEffect.EnableCRT = effectSelectedCRT;
      }

      // Effects.
      GUILayout.BeginVertical(boxStyle);
      {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, @"box");
        {
          int effectChanged = -1;

          for (int i = 0; i < vintageEffects.Count; ++i)
          {
            VintageBase vintageEffect = vintageEffects[i];

            GUILayout.BeginHorizontal();
            {
              GUILayout.BeginVertical(vintageEffect.enabled == true ? @"box" : string.Empty);
              {
                bool enableChanged = GUILayout.Toggle(vintageEffect.enabled, string.Format("  {0}", EffectName(vintageEffect.GetType().ToString())));
                if (enableChanged != vintageEffect.enabled)
                {
                  slideEffectTime = 0.0f;

                  effectChanged = i;
                }
              }
              GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(guiMargen * 0.5f);
          }

          for (int i = 0; i < vintageEffects.Count; ++i)
          {
            VintageBase vintageEffect = vintageEffects[i];

            if (effectChanged == i)
            {
              vintageEffect.enabled = !vintageEffect.enabled;

              if (vintageEffect.enabled == true)
                effectSelected = i;
            }

            if (vintageEffect.enabled == true && effectSelected != i)
              vintageEffect.enabled = false;
          }
        }
        GUILayout.EndScrollView();
      }
      GUILayout.EndVertical();

      GUILayout.FlexibleSpace();

      // Options.
      GUILayout.BeginVertical(boxStyle);
      {
        GUILayout.Label(@"TAB - Hide/Show gui.");

        GUILayout.BeginHorizontal(boxStyle);
        {
          if (GUILayout.Button(@"Open Web") == true)
            Application.OpenURL(@"http://www.ibuprogames.com/2015/05/04/vintage-image-efffects/");

#if !UNITY_WEBGL
          if (GUILayout.Button(@"Quit") == true)
            Application.Quit();
#endif
        }
        GUILayout.EndHorizontal();
      }
      GUILayout.EndVertical();
    }
    GUILayout.EndVertical();
  }

  private string EffectName(string name)
  {
    return name.Replace(@"Ibuprogames.VintageAsset.Vintage", string.Empty).ToUpper();
  }

  private void EnableEffect(int index)
  {
    if (index >= 0 && index < vintageEffects.Count)
    {
      vintageEffects[index].enabled = true;
      vintageEffects[index].Strength = effectSelectedStrength;
      vintageEffects[index].EnableFilm = effectSelectedOldFilm;
      vintageEffects[index].EnableCRT = effectSelectedCRT;
    }
  }

  private Texture2D MakeTex(int width, int height, Color col)
  {
    Color[] pix = new Color[width * height];
    for (int i = 0; i < pix.Length; ++i)
      pix[i] = col;

    Texture2D result = new Texture2D(width, height);
    result.SetPixels(pix);
    result.Apply();

    return result;
  }
}
