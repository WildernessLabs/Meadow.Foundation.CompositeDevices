# ChromaTek Button Wiring

This is the general wiring for a normally open, falling-edge interrupt.  On the left are the button connectors, on the right are the Meadow signals.
Note that the input port must be pulled high (either internally or with a 10k resistor).


**IMPORTANT: The wires for this button use non-typically coloring!  Be careful wiring it!**

```
BUTTON               MEADOW
===========================================
[WHT] COM \  
           |-------- `GND`  
[YEL] GND /

[GRN] NO ----------- (not conncted)

[BLU] NC ----------- IDigitalInterruptPort (e.g. `D04`) (pulled up)

[BLK] VDD ---------- `5V`

[RED] DIN ---------- `SCK`

[ORG] DOUT --------- (optional) Next button DIN
```