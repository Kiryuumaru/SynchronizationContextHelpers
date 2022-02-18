# SynchronizationContext Helpers

Provides helpers for SynchronizationContext. Can be used to synchronize UI operations with backend operations.

**NuGets**

|Name|Info|
| ------------------- | :------------------: |
|SynchronizationContextHelpers|[![NuGet](https://buildstats.info/nuget/SynchronizationContextHelpers?includePreReleases=true)](https://www.nuget.org/packages/SynchronizationContextHelpers/)|

## Installation
```csharp
// Install release version
Install-Package SynchronizationContextHelpers

// Install pre-release version
Install-Package SynchronizationContextHelpers -pre
```

## Supported frameworks
.NET Standard 2.0 and above - see https://github.com/dotnet/standard/blob/master/docs/versions.md for compatibility matrix

## Get Started

To use in UI safe updates, create the object instances at the UI thread or manually configure the SyncObject.SyncOperation to use UI thread.

## Usage

### SyncOperation Sample 1
```csharp
using SynchronizationContextHelpers;

namespace YourNamespace
{
    public class Program
    {
        private SyncOperation sync;

        public void UIThread()
        {
            sync = new SyncOperation();
        }

        public void BackgroundThread()
        {
            sync.ContextPost(() =>
            {
                UpdateUI(); // Will be executed on the UI thread.
            });
        }
    }
}
```
### SyncOperation Sample 2
```csharp
using SynchronizationContextHelpers;

namespace YourNamespace
{
    public class Program
    {
        private SyncOperation sync1;
        private SyncOperation sync2;

        public void UIThread()
        {
            sync1 = new SyncOperation();
        }

        public void BackgroundThread()
        {
            sync2 = new SyncOperation();
            sync2.SetContext(sync1); // Will set the sync2 context from the sync1 context.
            sync2.ContextPost(() =>
            {
                UpdateUI(); // Will be executed on the UI thread.
            });
        }
    }
}
```
### SyncContext Object Sample
```csharp
using ObservableHelpers;

namespace YourNamespace
{
    public class Dinosaur : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    ContextPost(delegate
                    {
                        PropertyChangedEventArgs args = new PropertyChangedEventArgs(nameof(Name));
                        PropertyChanged?.Invoke(this, args);
                    });
                }
            };
        }

        public virtual event PropertyChangedEventHandler? PropertyChanged;
    }
}
```
### SyncContext Sample
```csharp
using SynchronizationContextHelpers;

namespace YourNamespace
{
    public class Program
    {
        private Dinosaur dinosaur;

        public void UIThread()
        {
            dinosaur = new Dinosaur();
        }

        public void BackgroundThread()
        {
            dinosaur.PropertyChanged += (s, e) =>
            {
                // Executed on UI thread
            }
            dinosaur.Name = "Megalosaurus";
        }
    }
}
```

### Want To Support This Project?
All I have ever asked is to be active by submitting bugs, features, and sending those pull requests down!.
