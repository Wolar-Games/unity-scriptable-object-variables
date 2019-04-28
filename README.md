# Scriptable Object Variables with UniRx
This is my implementation of variables in Unity using scriptable objects as explained in [this](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=3289s) talk. I'm using it with [UniRx](https://github.com/neuecc/UniRx) but I have also implemented version that is without UniRx and is using C# Actions instead. By default you will get version without UniRx integration. See Integrations section for a guide how to enable UniRx integration (it's really easy).

# Usage
## Creating Variable
Simply right click into one of you folders in your project, select: Create -> Variable -> Select Type (there are some primitive types already contained in this library)
## Using Variable
Anywhere in you script simply define reference to a variable like:
```C#
public BoolReference myVariableName;
```
or the variable itself
```C#
public BoolVariable myVariableName;
```
And use it anyway you wish. I prefer to use references for reading the values and variables for writing into them.


# Creating new custom variables
To simplify the process of creation of new variables and editor scripts for them I have implemented tool that help you with that
   - Use Tools -> Reactive Variables -> Create New (it uses current assembly, if you want to create variable for another assembly like Unitys Game Object, or you are using new Assembly Definition Files, use All Assembly option)
   - Pick folders where to store generated files
        - Please note that the editor scripts needs to be stored in Editor folder or assembly
   - Start typing the class name
   - Select class you want to create variables for and click confirm
        
# Integrations
  - This project is integrated with UniRx - https://github.com/neuecc/UniRx
     - To enable this integration, use Tools -> Reactive Variables -> Enable UniRx Integration, this option needs to be enabled for each platform you want to use this integration with.
