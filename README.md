# JCMG CoC

JCMG CoC is a convention-over-configuration library for Unity that enabled defining your Unity projects folder structure and optionally generate code to be able to easily retrieve specific folder paths. Practically speaking, it helps to quickly enable tools and other frameworks to have a set of well-known locations to integrate into for resource reading/writing/sharing.

![COC Node Graph](/Images/COCNodeGraphExample.png)

## Installing JCMG COC

Using this library in your project can be done in three ways:

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com/). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.jeffcampbellmakesgames.coc
```

### Install via GIT URL

Using the native Unity Package Manager introduced in 2017.2, you can add this library as a package by modifying your `manifest.json` file found at `/ProjectName/Packages/manifest.json` to include it as a dependency. See the example below on how to reference it.

```
{
	"dependencies": {
		...
		"com.jeffcampbellmakesgames.coc" : "https://github.com/jeffcampbellmakesgames/unity-coc.git#release/stable",
		"com.jeffcampbellmakesgames.nodey": "https://github.com/jeffcampbellmakesgames/nodey.git#release/stable",
		...
	}
}
```

## Usage

To learn more about how to use JCMG COC, see [here](./usage.md) for more information.

## Contributors
If you are interested in contributing, found a bug, or want to request a new feature, please see [here](./contributors.md) for more information.

## Support
If this is useful to you and/or you’d like to see future development and more tools in the future, please consider supporting it either by contributing to the Github projects (submitting bug reports or features and/or creating pull requests) or by buying me coffee using any of the links below. Every little bit helps!

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I3I2W7GX)

## FAQ

### _Help, I'm getting an error for StackOverflow connecting two nodes!_
This is caused by creating a recursive link between two or more nodes. In the future a fix will prevent creating resursive connections.

## License
```
MIT License

Copyright (c) 2020 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
