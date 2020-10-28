<pre>
██╗    ██╗ ██████╗ ██╗  ██╗   ██████╗ ██╗     ██╗   ██╗ ██████╗ ██╗███╗   ██╗                            
██║    ██║██╔═══██╗╚██╗██╔╝   ██╔══██╗██║     ██║   ██║██╔════╝ ██║████╗  ██║                            
██║ █╗ ██║██║   ██║ ╚███╔╝    ██████╔╝██║     ██║   ██║██║  ███╗██║██╔██╗ ██║                            
██║███╗██║██║   ██║ ██╔██╗    ██╔═══╝ ██║     ██║   ██║██║   ██║██║██║╚██╗██║                            
╚███╔███╔╝╚██████╔╝██╔╝ ██╗██╗██║     ███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║                            
 ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝     ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝                            
                                                                                                         
                                                                                                         
                                                                                                         
█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗█████╗                           
╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝╚════╝                           
                                                                                                         
                                                                                                         
                                                                                                         
██████╗  ██████╗ ████████╗ █████╗ ████████╗███████╗    ███████╗ ██████╗██████╗ ███████╗███████╗███╗   ██╗
██╔══██╗██╔═══██╗╚══██╔══╝██╔══██╗╚══██╔══╝██╔════╝    ██╔════╝██╔════╝██╔══██╗██╔════╝██╔════╝████╗  ██║
██████╔╝██║   ██║   ██║   ███████║   ██║   █████╗      ███████╗██║     ██████╔╝█████╗  █████╗  ██╔██╗ ██║
██╔══██╗██║   ██║   ██║   ██╔══██║   ██║   ██╔══╝      ╚════██║██║     ██╔══██╗██╔══╝  ██╔══╝  ██║╚██╗██║
██║  ██║╚██████╔╝   ██║   ██║  ██║   ██║   ███████╗    ███████║╚██████╗██║  ██║███████╗███████╗██║ ╚████║
╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝   ╚═╝   ╚══════╝    ╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═══╝
                                                                                                         
</pre>

Alex Liu 10.28.2020

---

# What is this?

This is a plugin called by Wox for screen rotation. (https://github.com/Wox-launcher/Wox)
If you usually rotate screen for different usage, 
this plugin could save a lot of time for setting rotation panel.
(I hate clicking too many times.)

# How to install?

2 options are prepared.

## By Setup Package

- download .wox file from release .

- drag the plugin installation package onto the Wox search box to start the installation.

- Set action keyword trigger in wox setting panel ( Default keyword is **RS** ).

Then everything goes fine.

## By Code Compiling
You can just build this project and get the dll file.
Finish the following part according to http://doc.wox.one/

First, compile the solution.

Then, goto AppData\Roaming\Wox\Plugins or \AppData\Local\Wox\app-xxxxxx\Plugins and create a new folder. (app-xxxxx represents a specific version of wox, for me its app-1.3.524.)

Last, copy all .dll files, json file and image folder to the target folder.

# How to use?

The command are consists of 3 parts: action keyword trigger, selected screen and direction

for action keyword trigger, **RS** is defined as the default keyword.

for screen index selection, 0 is set as all screens, 1 to n is regarded as the specific screen index.

for directions, **up**, **down**, **left**, **right** are defined as the enumerate keywords for 4 directions.
Specifically, the direction is the image direction of the screen, which is based on the direction vector from bottom to up.
So, **right** means the vector point to the right edge of the screen. So does the **left** and **down**.

for example : RS 1 right -> Select screen 1 to right direction.

Have fun.