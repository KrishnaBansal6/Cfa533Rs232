[![Build status](https://ci.appveyor.com/api/projects/status/belpoe9qhduusy3j?svg=true)](https://ci.appveyor.com/project/petrsnd/cfa533rs232)

# Cfa533Rs232
C# Driver for CrystalFontz CFA533 family of 16x2 LCD displays; Developed against the CFA533-TMI-KU

## Sources
There are three projects included in the solution:
- Cfa533Rs232Driver -- This is the main library that drives the CFA533 via serial port communications.
- Cfa533Rs232Tool -- A command-line tool for exercising all currently implemented CFA533 commands.
- Cfa533Rs232Demo -- A command-line tool that implements slightly higher-level device interactions.

## Examples
Create an LCD device with the appropriate serial port name.  My CFA533 is usually given 'COM3 and, by default, communicates at 19200 baud rate.
```C#
using (var lcdDevice = new LcdDevice("COM3", LcdBaudRate.Baud19200))
{
    // You must call Connect() before trying to communicate with the device
    lcdDevice.Connect();
    
    // Call lots of methods here...
    lcdDevice.SetLcdContents("Hello", "World!");
}
```

In order to support keypad events you need to register an event handler.
```C#
EventHandler<KeypadActivityEventArgs> eventHandler =
    delegate(object sender, KeypadActivityEventArgs eventArgs)
    {
        Console.WriteLine($"Keyboard Event: {eventArgs.KeypadAction}");
    }
lcdDevice.KeypadActivity += eventHandler;
```

The CFA533 is meant to handle one command at a time.  There is a lock that ensures that only one command is waiting for a response to be acknowledged before additional commands are sent.  However, keypad events may come at any time.  This driver works pretty well in a multithreaded environment, although I have noticed an issue here and there with clean-up where not all threads know that Disconnect() has been called.  In general, this should just produce a DeviceConnectionException stating that the device is not connected.
