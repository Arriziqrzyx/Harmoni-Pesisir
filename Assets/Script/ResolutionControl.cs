using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionControl : MonoBehaviour
{
    public TMP_Dropdown aspectRatioDropdown;
    public TMP_Dropdown refreshRateDropdown;

    private List<string> aspectRatioOptions = new List<string>();
    private List<string> refreshRateOptions = new List<string>();

    private void Start()
    {
        // Mengisi opsi aspect ratio pada dropdown
        aspectRatioOptions.Add("16:9");
        aspectRatioOptions.Add("18:9");
        aspectRatioOptions.Add("19:9");
        aspectRatioOptions.Add("20:9");
        aspectRatioDropdown.AddOptions(aspectRatioOptions);

        // Mengisi opsi refresh rate pada dropdown
        refreshRateOptions.Add("60 FPS");
        refreshRateOptions.Add("30 FPS");
        refreshRateDropdown.AddOptions(refreshRateOptions);

        // Mengatur nilai default pada dropdown berdasarkan PlayerPrefs
        aspectRatioDropdown.value = PlayerPrefs.GetInt("AspectRatioIndex", 0);
        refreshRateDropdown.value = PlayerPrefs.GetInt("RefreshRateIndex", 0);

        // Mengatur "Target Frame Rate" berdasarkan pilihan refresh rate default (60 FPS)
        int defaultRefreshRate = GetRefreshRateValue(refreshRateDropdown.value);
        Application.targetFrameRate = defaultRefreshRate;
    }

    public void SetAspectRatio(int aspectRatioIndex)
    {
        float aspectRatio = GetAspectRatioValue(aspectRatioIndex);
        int screenWidth = Mathf.RoundToInt(Screen.currentResolution.height * aspectRatio);
        int screenHeight = Screen.currentResolution.height;
        int refreshRate = GetRefreshRateValue(refreshRateDropdown.value);

        // Mengatur "Target Frame Rate" berdasarkan pilihan refresh rate
        Application.targetFrameRate = refreshRate;

        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen, refreshRate);

        // Cetak pesan ke konsol saat memilih aspect ratio
        Debug.Log("Aspect Ratio selected: " + aspectRatioOptions[aspectRatioIndex]);

        // Simpan pilihan aspect ratio ke PlayerPrefs
        PlayerPrefs.SetInt("AspectRatioIndex", aspectRatioIndex);
    }

    public void SetRefreshRate(int refreshRateIndex)
    {
        int refreshRate = GetRefreshRateValue(refreshRateIndex);

        // Mengatur "Target Frame Rate" berdasarkan pilihan refresh rate
        Application.targetFrameRate = refreshRate;

        // Cetak pesan ke konsol saat memilih refresh rate
        Debug.Log("Refresh Rate selected: " + refreshRateOptions[refreshRateIndex]);

        // Simpan pilihan refresh rate ke PlayerPrefs
        PlayerPrefs.SetInt("RefreshRateIndex", refreshRateIndex);
    }

    private float GetAspectRatioValue(int aspectRatioIndex)
    {
        float aspectRatio = 16f / 9f; // Nilai default 16:9

        if (aspectRatioIndex == 1)
        {
            aspectRatio = 18f / 9f;
        }
        else if (aspectRatioIndex == 2)
        {
            aspectRatio = 19f / 9f;
        }
        else if (aspectRatioIndex == 3)
        {
            aspectRatio = 20f / 9f;
        }

        return aspectRatio;
    }

    private int GetRefreshRateValue(int refreshRateIndex)
    {
        int refreshRate = 60; // Nilai default 60 FPS

        if (refreshRateIndex == 1)
        {
            refreshRate = 30;
        }

        return refreshRate;
    }
}
