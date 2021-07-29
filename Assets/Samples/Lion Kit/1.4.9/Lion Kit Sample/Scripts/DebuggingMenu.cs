using UnityEngine.UI;
using LionStudios;
using LionStudios.Ads;

public class DebuggingMenu : LionMenu
{
    public Text installTimeText = null;
    public Text lastLoginTimeText = null;

    private const string installTimePrefix = "Installed: {0}";
    private const string loginTimePrefix = "Last Login: {0}";

    public void UpdateInstallTime()
    {
        if (!LionKit.IsInitialized) return;
        if (installTimeText != null)
        {
            string time = Retention.GetInstallTime();
            if (string.IsNullOrEmpty(time))
            {
                time = new System.DateTime().ToString();
            }

            installTimeText.text = string.Format(installTimePrefix, time);
        }
    }

    public void UpdateLastLoginTime()
    {
        if (!LionKit.IsInitialized) return;
        if (lastLoginTimeText != null)
        {
            string time = Retention.GetLastLoginTime();
            if (string.IsNullOrEmpty(time))
            {
                time = new System.DateTime().ToString();
            }

            lastLoginTimeText.text = string.Format(loginTimePrefix, time);
        }
    }

    public void ClearInstallTime()
    {
        if (LionKit.IsInitialized)
        {
            Retention.ClearInstallTime();
            UpdateInstallTime();
        }
    }

    public void ClearLastLoginTime()
    {
        if (LionKit.IsInitialized)
        {
            Retention.ClearLastLoginTime();
            UpdateLastLoginTime();
        }
    }

    private void OnEnable()
    {
        UpdateInstallTime();
        UpdateLastLoginTime();
    }
}