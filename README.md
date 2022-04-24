# Reenigne
An application to help with reverse engineering printed circuit boards. Semi automates the process of creating a schematic from photos of the printed circuit board

Background
----------
I wrote this application for my own use but since it works I might as well share it so maybe others find it useful.

I am not a real programmer and I'm new to c#, this being the first application I wrote in c# that actually does something useful rather than just being a learning example.

I would advise against beginners using the source code as an example and against professional developers from even looking at it.

The application has lots of rough edges and bugs but it reaches the threshold of being useful in what it does, at least for me, and has already saved me a lot of time repairing some devices.  I guess some people will find it useful as I did while others might find it too much trouble due to poor interface design and bugs.

If there is interest in this project I will put some more effort into improving it.

I also made a small companion application to help with image alignment and intend to publish that as well.

The application and what it does
--------------------------------
The purpose of this application is to help with the repair of electronics for which schematic diagrams are not available. It helps with the tedious process of manually following tracks on PCBs and drawing a schematic diagram based on that.

Installation:
Just copy the file Reenigne.exe from bin\release to any convenient place on your PC and then run it from there.  It should not need any installation of anything else as far as I know. 

To have a better schematic view you will also need the symbol libraries from KiCad. This allows you to substitute the default component symbols with proper symbols of resistors, capacitors, ICs and such from the KiCad library.

To use it:
1. Make photographs of the PCB from both sides
2. In a paint package such as Gimp or Photoshop mirror the phot of the back and align it with the photo of the front so all the vias and thru holes line up
3. Start the Reenigne application. and File->new
4. Select Tools->Layers and images. This lets you import the photos and assign them to the layers. Some minor alignment adjustments may als be made here. Close the dialog.
5. In the main program the screen is split int two. The left half is where the printed circuit is displayed while the right is where the schematic will be displayed. The button 'Board Layer' switches between layers. Press and hold middle button (wheel) to drag the image and turn the mousewheel to zoom in/out.
6. Save the project before proceeding  File->Save As. It is best to keep the project and the PCB photos in the same directory
7. Place vias to match the photo. Do this by clicking the radio button 'Drop Via' on the left and then point and click at each point where there is a via in the photo. Vias need only be placed on one side as they will show up also on the other side.  Before progressing too far make sure that the vias are actually aligning well between the tow sides and if not go back and realign the images better.
8. Place the components by selecting the 'Component' radio button. Then with right click change the number of pins, pad size, whether it is thru hole or SMD and the pin format (rectangular, square, DIP, SIL etc). Make sure you get that right, especially whether it is SMD or thru hole as doing one instead of the other will result in chaos later on.
9. Select the 'Draw Trace' button and use it to draw traces to match those on the photos. A trace is started by a click on the starting position, then click at each corner to build up the trace and double click the last point to finalise it.  While drawing, pressing the DELETE key removes the last point of the trace and pressing ESC aborts the whole trace.
10. Press the 'Retrace Nets' button.  This will generate a netlist and create a first draft of the schematic diagram in the right half of the window. This can be scrolled and zoomed in the same way as the PCB image.
The connections between all the components are shown correctly but the layout is a bit of a mess. This is where some manual work is needed to shift around the components  and draw the lines neater. You can drwa lines manually without worrying about maintaining the connections as the program will ensure that the connections between components continue to represent what is in the PCB representation.
11. To draw lines in the schematic left click on the red dot where connection lines meet. This will hilite the conenctions on that net in red. Then right click and select draw connection. Draw by moving the mouse and clicking to make corners of the connection line. To finish drawing the line right click and 'Finish drawing connection' (or press ENTER).  As with the PCB trace drawing, DELETE will delete the last point and ESC will abort the entire connection drawing.

Limitations
-----------
A full list of limitations would be longer than  the list of features but from my own practical use of the application to help repair stuff I seem to cope with them. 
Here are some of the more important ones:
1. There is no UNDO function.  The nearest to that is the 'Save Checkpoint' button which simply saves the project with a timestamp. If need to undo something that cannot easily be done manually just load the one just before you did what needs to be undone and rename it to the original file name.
2. There is very limited editing especially in the printed circuit side. In most cases you have to delete something and re-draw it. 
3. The symbols cannot be numbered (as R1, C5 Q8 etc), at least not yet.  Much of the code for that exists but needs some more work to actually do something.
4. There is no user manual. The closest thing to that is this readme. If there is interest I will put some effort into making one.
5. Error trapping is almost non existent so the application will bomb out with things like trying to load a file that is not in the correct format or non existent. Make sure you save and create checkpoints often to avoid losing your work.




