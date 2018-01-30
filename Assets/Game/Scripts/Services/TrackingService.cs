using System.Collections.Generic;
using UnityEngine.Analytics;

public class TrackingService {

    public void ScreenEnter(string screenName)
    {
        Track("screenEnter", new Dictionary<string, object>() {
            { "screenName", screenName}
        });
    }

    public void ScreenLeave(string screenName)
    {
        Track("screenLeave", new Dictionary<string, object>() {
            { "screenName", screenName}
        });

    }

    public void LevelStart(int level, int stars)
    {
        Track("levelStart", new Dictionary<string, object>() {
            { "level", level },
            { "stars", stars }
        });
    }

    public void LevelWin(int level, int expectedPackages, int actualPackages, float time, int stars)
    {
        Track("levelWin", new Dictionary<string, object>() {
            { "level", level },
            { "expectedPackages", expectedPackages },
            { "actualPackages", actualPackages },
            { "time", time},
            { "stars", stars }
        });
    }

    public void LevelLose(int level, int expectedPackages, int actualPackages, float time)
    {
        Track("levelLose", new Dictionary<string, object>() {
            { "level", level },
            { "expectedPackages", expectedPackages },
            { "actualPackages", actualPackages },
            { "time", time }
        });
    }

    public void AppStart()
    {
        Track("appStart");
    }

    private void Track(string eventName)
    {
        Track(eventName, new Dictionary<string, object>());
    }

    private void Track(string eventName, Dictionary<string, object> payload)
    {
        Analytics.CustomEvent(eventName, payload);
        /*Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
            { "potions", totalPotions },
            { "coins", totalCoins },
            { "activeWeapon", weaponID }
          }
        );*/
    }
}
