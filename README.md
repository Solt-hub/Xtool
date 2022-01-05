# Xtool
Xtool.It can add a "*.xml" file to extend itself!

## A Simple Demo Of The XML

```xml
<Xtool>
	<DoeshaveXCSS Does="True">E:\XCSS1.xml</DoeshaveXCSS>
	<!---Do you have the XCSS?--->
	<Lpath Filter=".xtl">C:\ac</Lpath>
	<!---the filepath & Filter that the Xtool will surveillance--->
        <EXEname>C:\Users\20916\source\repos\a\a\bin\Debug\net6.0\a.exe</EXEname>
	<!---event handler's name & path--->
        <Control Type="Button" X="112" Y="90" Width="75" Height="23" Font="Arial" Emsize="10" Id="1" StyleId="1">Hello</Control>
	<!---a control(Button)--->
	<Control Type="MSGBox" Title="HEHE" MSGButton="OK" MSGIcon="Error">Hello</Control>
	<!---another control(MSGbox)--->
</Xtool>
```

## A Simple Demo Of The XCSS

```xml
<XCSS>
	<Style StyleId="1" Font="Arial" Fsize="10"></Style>
	<!--if there is a not a negative one styleId in a XML's ,then the font & font's size of that control will change to the font & font's size in the XCSS--->
</XCSS>
```
