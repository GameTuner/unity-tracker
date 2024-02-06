# GameTuner Unity Tracker SDK

## Overview

GameTuner Unity Tracker allows you to collect events from your Unity games or apps. It is lightweight SDK that can be easily integrated into your project.

## Requirements

* GametunerUnityTracker requires Unity 2018.1+ and .NET Standard 2.0 Api Compatibility Level.
* GametunerUnityTracker requires Newtonsoft.Json. This is [bundled with Unity 2020.1+][unity-newtonsoftjson] or [Newtonsoft.Json-for-Unity][newtonsoftjson-for-unity] can be used for earlier versions.
* GametunerUnityTracker supports both Mono and IL2CPP scripting backends.

## Installation

### Using Unity Package Manager UI

GameTuner Unity Tracker can be installed via the Unity Package Manager. To install the latest version, add unity package using Package Manager UI. Add package from git url and use this url:

```
https://github.com/GameTuner/unity-tracker.git?path=/unity-package
```

### By editing `manifest.json`

Another way to install GameTuner tracker is to add the following line to your `manifest.json` file located under your Unity project's Packages directory:

```json
{
  "dependencies": {
"com.algebraai.gametunertracker": "https://github.com/GameTuner/unity-tracker.git?path=/unity-package",
        }
}
```

### Using .unitypackage

Installation can be done using .unitypackage file. Download latest release from [GitHub Releases][releases] and import it into your project.

### Newtonsoft.Json

GametunerUnityTracker requires Newtonsoft.Json. This is [bundled with Unity 2020.1+][unity-newtonsoftjson] or [Newtonsoft.Json-for-Unity][newtonsoftjson-for-unity] can be used for earlier versions. Or find [Newtonsoft.Json.dll] in `Resource` folder.

## Usage

### Import library

You need to import library in your script by adding next lines at the top of your `cs` script:

```csharp
using GametunerTracker;
```

### Initialization

First you need to initialize plugin:

```csharp
string analyticsAppID = "game_name";        // app_id of game
string apiKey = "secret_key";               // API key specific to every game
string endpointUrl = "endpoint_url"         // endpoint of backend service
bool isSandboxEnabled = false;              // is sendbox enviroment
string userID = "user_id";                  // user ID set by game developer
bool isHttps = true;                        // is https enabled
string store = "store_name";                // GooglePlay, AppleStore, Amazon...
GametunerUnityTracker.Init(analyticsAppID, apiKey, endpointUrl, isSandboxEnabled, userID, isHttps, store);
```

Fields ```userID```, ```isHttps``` and ```store``` are optional. 

If your games are offline, you may not have userId on Init, for that reason we left userID as optional field. Also, you can find method to set userID once you got validated id from server. 

Use this method after initialization of plugin.

```csharp
GametunerUnityTracker.SetUserId(userID);
```

Plugin will handle caching of userID and triggered events.

### Logging events

To trigger analytics event use:

```csharp
string eventName = "name_of_event";      // level_played, transaction...
string schemaVersion = "1-0-0";          // version of event schema
Dictionary<string, object> parameters;   // parameters of event. It can be null object
int priority = 0;                        // Priority of event. Higher the number, higher is prority

GametunerUnityTracker.LogEvent(eventName, schschemaVersionema, parameters, priority);
```
**NOTE**: If you make a call of ```LogEvent``` method before Init, event will be discarded, so make sure to call ```Init``` before any event log.

There are help methods for logging common events. You can use them to trigger common events or log them as regular event with latest schema version that you created.


```csharp
public static void LogEventAdStarted(
        string adPlacement,
        string groupId = null,
        string adPlacementGroup = null, 
        string adProvider = null, 
        string adType = null, 
        int limit = int.MinValue, 
        int limitCounter = int.MinValue, 
        int durationSeconds = int.MinValue, 
        string crosspromo = null,
        Dictionary<string, object> customParameters = null,
        string schemaVersion = null,
        int priority = 0);
public static void LogEventAdWatched(
        string adPlacement, 
        bool rewardClaimed,
        string groupId = null,                                             
        string adPlacementGroup = null, 
        string adProvider = null, 
        string adType = null, 
        int limit = int.MinValue, 
        int limitCounter = int.MinValue, 
        int durationSeconds = int.MinValue, 
        int secondsWatched = int.MinValue, 
        string crosspromo = null,
        Dictionary<string, object> customParameters = null,
        string schemaVersion = null,
        int priority = 0);
public static void LogEventCurrencyChange(
        string currency, 
        long amountChange,       
        string groupId = null,
        long stashUpdated = int.MinValue, 
        long currencyLimit = int.MinValue, 
        long amountWasted = int.MinValue, 
        string reason = null, 
        string gameMode = null, 
        string screen = null,
        Dictionary<string, object> customParameters = null,
        string schemaVersion = null,
        int priority = 0);
public static void LogEventPurchaseInitiated(
        string packageName, 
        string paymentProvider = null, 
        string packageContents = null, 
        long premiumCurrency = int.MinValue,   
        double price = double.MinValue, 
        string priceCurrency = null,                                                   
        float priceUSD = float.MinValue, 
        string shopPlacement = null, 
        string gameMode = null, 
        string screen = null, 
        string groupId = null,
        Dictionary<string, int> packageItems = null,
        Dictionary<string, object> customParameters = null,
        string schemaVersion = null,
        int priority = 0);
public static void LogEventPurchase(
        string packageName, 
        double paidAmount, 
        string paidCurrency, 
        string transactionId = null, 
        string paymentProvider = null, 
        string payload = null,  
        string packageContents = null, 
        long premiumCurrency = int.MinValue, 
        double price = double.MinValue, 
        string priceCurrency = null, 
        double priceUsd = double.MinValue,                                            
        double paidUsd = double.MinValue, 
        string gameMode = null, 
        string shopPlacement = null, 
        string screen = null, 
        string transactionCountryCode = null, 
        string groupId = null, 
        Dictionary<string, int> packageItems = null,
        Dictionary<string, object> customParameters = null,
        string schemaVersion = null,
        int priority = 0);
```

Common event can be extended with custom parameters. You can use ```Dictionary<string, object>``` to add custom parameters to common event. If you want to add custom parameters to common event, you need to specify schema version. Event priprity is by default set to 0. Below is example of adding custom parameter to common event:

```csharp
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("custom_parameter", "custom_value");
GametunerUnityTracker.LogEventAdStarted("ad_placement", custom_parameters: parameters, schemaVersion: "1-0-0", priority: 10);
```

### Logging 

Logging is disabled by default.
You can setup logging for debugging purposes before init or any time after init:

```csharp
LoggingLevel loggingLevel = LoggingLevel.Debug  // enum: Error, Debug, Verbose
GametunerUnityTracker.SetLoggingLevel(loggingLevel);
GametunerUnityTracker.EnableLogging();
```

To disable logging use method

```csharp
GametunerUnityTracker.DisableLogging();
```

### Tracker metadata

There are some metadata that are set and saved in tracker, which you can pull. Data is persistent between two session.

```csharp
GametunerUnityTracker.GetFirstOpenTime();   // gets first_open time in unix timestamp format (long)
GametunerUnityTracker.GetUserID();          // gets user Id set by developer
GametunerUnityTracker.GetInstallationID();  // gets installation ID. Installation ID is set on first_open
```

### Sessions

New session is triggered automatic in SDK. Session is triggered in next cases:
- When game (app) is opened as cold start (app was not in background)
- When game is opened from background and background time was longer then 5 minutes.
- When game is in foreground and event is triggered after 20 hours of inactivity.

If you need to use new session event (login) in game, you can subscribe to ```OnSessionStarted``` delegate.

```csharp
GametunerUnityTracker.onSessionStartEvent += OnSessionStart;

private void OnSessionStart(string sessionId, int sessionIndex, string previousSessionId)
{
    Debug.Log(string.Format("New session is started. SessionId: {}, SessionIndex: {},  PreviousSessionId: {} ", sessionId, sessionIndex, previousSessionId));
}
```

## Copyright and license

This project is fork of [Snowplow Unity Tracker version 0.5.0][snowplow-unity-0.5.0], that is licenced under Apache 2.0 licence.

The GameTuner Unity Tracker is copyright 2022-2024 AlgebraAI.

GameTuner Unity Tracker is released under the [Apache 2.0 License][license].


[GameTuner]: https://gametuner.ai
[unity]: https://unity3d.com/
[unity-newtonsoftjson]: https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@2.0
[newtonsoftjson-for-unity]: https://github.com/jilleJr/Newtonsoft.Json-for-Unity
[Newtonsoft.Json.dll]: Resources/Newtonsoft.Json.dll
[releases]: https://github.com/GameTuner/unity-tracker/releases
[license]: https://www.apache.org/licenses/LICENSE-2.0
[snowplow-unity-0.5.0]:https://github.com/snowplow/snowplow-unity-tracker/releases/tag/0.5.0