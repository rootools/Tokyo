# Welcome to Tokyo!
![](https://github.com/rootools/brav.in/raw/master/assets/tokyo_logo.png)

Hello! My name is Anton and I am developer of [Masterplan Tycoon](https://store.steampowered.com/app/1644500?utm_source=github_tokyo).

Every programmer loves some parts of his code and constantly drags them with him from project to project. At some point, I had more than two of these pieces and I decided to combine it together. This is how Tokyo (formerly CoreToolkit) was born. It wasn't meant to be a slender and versatile framework, but more like jQuery — just a set of features that have made my life easier for several years now.

## MonoBehaviour Singletons
Destroyable and Don't Destroyable Singletons base classes
```c#
MBSingletonDD<T>
MBSingleton<T>

public class GameManager : MBSingletonDD<GameManager> { }

public class SpellManager : MBSingleton<SpellManager> { }

// Invoke like this:
GameManager.Instance().PauseGame();
SpellManager.Instance().CastFireball();
```

## Commands
A very useful system for building sequences. Most often used to load a game. Or cutscene.. Something like that..
```c#
// Example:

QueueCommand q = new QueueCommand();

q.Add(new SomeLoadDataCommand());
q.Add(new AsyncLoadSceneCommand("game"));
q.Add(new SetActiveSceneCommand("game"));
q.Add(new SomeStuffCommand());
q.Add(new TokyoFadeCommand(new FadeConfig(1f, 0f, 3f)));
q.Add(new DelayCommand(1f));

q.AddCommandCompleteHandler(cmd => {
    // Use for invoke some stuff when concrete command complete
});

q.AddCompleteHandler(_ => { 
    // Some complete stuff
});

// All commands must be run only if it is not a QueueCommand or ParallelCommand
q.Execute();
```

### Built-in commands
#### QueueCommand
Command that runs other commands in sequence. You can insert commands inside each other.
```c#
QueueCommand q = new QueueCommand();
q.Add(new OtherCommand());
q.Add(new OtherQueueCommand());
q.Execute();
```

#### ParallelCommand
It' like QueueCommand but work in parallel. Useful for async code inside.
```c#
QueueCommand q = new QueueCommand();

ParallelCommand pq = new ParallelCommand();
pq.Add(new SomeAsyncCommand());
pq.Add(new AnotherAsyncCommand());

q.Add(pq);
q.Add(new AndSomeAnotherStuffCommand())

q.Execute();
```

#### AsyncLoadSceneCommand
Addictive loading scene as Command.
```c#
AsyncLoadSceneCommand asyncLoadSceneCommand = new AsyncLoadSceneCommand(sceneName);

// Callback on progress change for loader if you need
asyncLoadSceneCommand.OnProgressChange += (float progress) => {};
```

#### SetActiveSceneCommand
Change Active scene.
```c#
new SetActiveSceneCommand(sceneName);
```

#### MoveObjectToSceneCommand
Move object between loading scenes.
```c#
new MoveObjectToSceneCommand(gameObject, sceneName);
```

#### UnloadSceneCommand
Unload scene by name.
```c#
new UnloadSceneCommand(sceneName);
```

#### DelayCommand
Very useful inside Queue
```c#
new DelayCommand(time);
```

#### SetActiveCommand
Enable or Disable GameObjects
```c#
new SetActiveCommand(gameObject, isActive);
```

#### WaitNextFrameCommand
Delay for NextFrame Command. You know..
```c#
new WaitNextFrameCommand();
```

### Write own Commands
Example:
```c#
// Inherit from BaseCommand
public class AwesomeCommand : BaseCommand {

        private readonly string _awesomeTarget;

        // Set all your data in constructor 
        public AwesomeCommand(string awesomeTarget) {
            _awesomeTarget = awesomeTarget;
        }

        //For your logic override the `ExecInternal` method
        protected override void ExecInternal() {
            // Make some Awesome stuff!
            Debug.Log($"{awesomeTarget}, you are AWESOME!");
            
            // The command is considered complete if you call NotifyComplete()
            NotifyComplete();
        }

    }
```

## Lerp
Basic "tweeners"
```c#
// Create Lerp. It starts automatically
tokyoLerp = new TokyoLerp(fromValue, toValue, time);

tokyoLerp.OnLerpUpdate += (lrp) => {
    // Some every-tick stuff
};

tokyoLerp.OnLerpEnd += (lrp) => {
    // Some complete stuff
};

// Lerps support Easings
tokyoLerpWithEasing = new TokyoLerp(fromValue, toValue, time, EaseType.InExpo);

// Lerps can be paused
tokyoLerp.Pause();

// Resumed
tokyoLerp.Resume();

// And Stops *But I think it will kill them. he he*
tokyoLerp.Stop();
```

## Fader
Simple Canvas-based Fader

```c#
// FadeConfig with fade parameters(from, to and time required)
FadeConfig fadeConfig = new FadeConfig(fromFaderAlpha, toFaderAlpha, time, fadeColor, isLockedRaycasts);

// Don't forget to unsubscribe
TokyoFader.Instance().OnFadeComplete += SomeStuffOnFadeComplete;

// Execute
TokyoFader.Instance().Fade(fadeConfig);

```

## Other

### Easings
All easings from [easings.net](https://easings.net/)
```c#
// Progress from 0 to 1 with EaseType;
TokyoEasings.Ease(Progress, EaseType.Linear);
```
Nicely combine with Lerps.

### Math
```c#
// Remap input value from one range to another
TokyoMath.Remap(-0.75f, -1f, 1f, 0f, 1f);
```

### Collisions
Some math overlap or collision function. Useful when calculating intersections without using physics engine.
```c#
// Is Circle overlap with Rect.
TokyoCollisions.CircleRectangle(circlePos, circleRadius, rect);
```