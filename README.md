# Xtool
Xtool.It can add a "*.xml" file to extend itself!

## A Simple Demo

```xml
<Xtool>
<!---It is the root of the Xtool-Plugins--->
	<Lpath Filter=".xtl">C:\ac</Lpath>
  <!---The file that if there is an event occurred,and the event handler will send to--->
  <!--Tip:And the Xtool will read it--->
    <EXEname>C:\Users\20916\source\repos\a\a\bin\Debug\net6.0\a.exe</EXEname>
    <!--event handler's name--->
    <Control Type="Button" X="112" Y="90" Width="75" Height="23" Font="Arial" Emsize="10" Id="1">Hello</Control>
    <!---A button Control--->
	<Control Type="MSGBox" Title="HEHE">Hello</Control>
  <!--And a MSGBox--->
</Xtool>
```
