# ColorFilter
Simple program, that filters colours from image

## Whyyyy

Coz I'm bored, that is exactly why

## Does it work?

Not really... It might get closer to usable state, when I make a GUI, where changes to filter can be made in real time.
Right now, best i can do is to filter all colours but red from this meme.
![Dog meme](https://damoch.github.io/img/test_img.png)

## How can I use it?

[Download the latest release](https://github.com/damoch/color-filter/releases), and run the program with following parameters:

```ColorFilter.exe <filename> <ColorRed> <ColorGreen> <ColorBlue> <ToleranceRed> <ToleranceGreen> <ToleranceBlue>```

Where
```<filename>```
Is path to original file, ```<ColorRed> <ColorGreen> <ColorBlue>``` are RGB values of coulour you want to preserve (intigers 0-255), and ```<ToleranceRed> <ToleranceGreen> <ToleranceBlue>```
are percentage values of tolerance (intigers 0-100)
