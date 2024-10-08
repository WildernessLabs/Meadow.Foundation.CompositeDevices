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

[GRN] NO ----------- IDigitalInterruptPort (e.g. `D04`) (pulled up)

[BLU] NC ----------- (not conncted)

[BLK] VDD ---------- `5V`

[RED] DIN ---------- `COPI`

[ORG] DOUT --------- (optional) Next button DIN
```