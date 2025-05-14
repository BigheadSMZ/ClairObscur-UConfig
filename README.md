# Clair Obscur: Expedition 33 - Unreal Config
Creates and configures "Engine.ini" for Clair Obscur: Expedition 33.

![image](https://github.com/user-attachments/assets/d9ec6733-fa92-473c-befa-8b071c5a7dfa)

# Installation

The executable can be ran from anywhere, but it is suggested to copy it alongside **Expedition33_Steam.exe** for the Steam version, **Sandfall.exe** for the GamePass version, or **SandFall-Win64-Shipping.exe** for either version. This allows launching the game through this configurator.

# Usage

- Copy configurator to game directory alongside launch executable.
- Load up the configurator. If no INI exists, it will alert you how to create one.
- If you do not have an INI, create it with **File >> Create Engine.ini**.
- This will unlock the various options that will affect the graphics of Clair Obscur.
- Configure the game how you like, or use the built in presets from the menu bar.
- Suggested to load a **Low-Ultra** preset before using **Sharp & Clear** or **Soft & Ambient**.
- A backup can be created and loaded from the menu bar, allowing test configurations.
- Doubles as a launcher using the **Launch Game** button or the menu bar option.
- Can quickly task kill the game from the menu bar option for quick testing.
- Compatible with most premade Engine.ini files that use **[SystemSettings]** section.

# Advanced Usage

- Click the **Show Options** radio button under **Advanced Options**.
- From here a new world opens up in terms of configuration.
- **Collections** contains a bunch of predefined settings from other mod authors.
- Copy and paste your own options into the textbox and click **Parse** to add them.
- Add, delete, or configure options directly from the grid.
- Parsing values that falls under the "standard" config imports their values.

# Other Info

Some options will have a profound effect on graphics, but it's also possible that some options don't have any apparent effect. To make things simple, there are some built-in Presets that can be selected for a base configuration. If wanting to use one of the bottom two presets (Sharp & Clear/Soft & Ambient), it is suggested to first choose one that reflects the power of your PC (Low - Ultra). From there the settings can be fine tuned. The menu bar Game option can launch the game if it's in the game folder alongside the game's executable, and it can also task kill the game process (this may require administrator privileges). It's also possible that Windows will see this application as some kind of virus as it doesn't like apps that mess with running apps. I assure you that it is not, all the code is visible.

If you wish to use this as a base to create a configurator for another game, feel free to fork the code on Github and do what you like with it. I tried to make it easy to edit, configure, and adapt and heavily commented the code. If you do use it, all I ask for is credit for the base project.

**NexusMods Page:** [https://github.com/BigheadSMZ/ClairObscur-UConfig](https://www.nexusmods.com/clairobscurexpedition33/mods/119)
