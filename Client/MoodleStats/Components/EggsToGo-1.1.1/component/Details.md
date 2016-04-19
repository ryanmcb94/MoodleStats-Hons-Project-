Eggs-To-Go
==========

Eggs-To-Go is a Xamarin Cross Platform mobile library for implementing Easter Egg gestures!

Features
--------
 - Konami and Mortal Kombat code
 - Create your own Custom sequences
 - Xamarin.iOS and Xamarin.Android support
 - Xamarin Component store
 

Quick and Simple
----------------
```csharp
//Create our new instance, specifying the UIView to recognize gestures on
var easter = new EggsToGo.Easter (this.View, new KonamiCode());

//Event for when a egg/code has been detected (eg: Konami Code)
easter.EggDetected += egg => Console.WriteLine("Egg: " + egg.Name);

//You can see each individual command as it happens too
easter.CommandDetected += cmd => Console.WriteLine("Command: " + cmd.Value);
```


Default Egg Sequences
---------------------
By default I've included the Konami code and Mortal Kombat code:

- **Konami Code:** UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT, TAP, TAP
- **Mortal Kombat Code:** DOWN, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT, TAP, TAP


Custom Egg Sequences
--------------------
By default the Konami and Mortal Kombat codes are built in, but you may want to add your own sequences!

```csharp
var easyEgg = new CustomEgg("Easy")
    .WatchForSequence(Command.SwipeUp(), Command.SwipeDown(), Command.Tap());
    
var easter = new Easter(this.View, easyEgg);
```


Thanks
------
Thanks to Eight-Bot software for their original post on getting this working with Mono for Android: http://eightbot.com/writeline/developer/konami-code-detection-with-mono-for-android/

This was definitely my inspiration for making this simple component!

