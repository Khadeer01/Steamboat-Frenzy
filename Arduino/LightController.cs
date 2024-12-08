using System.IO.Ports;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public string portName = "COM7"; // Change this to your Arduino's COM port
    public int baudRate = 9600; // Baud rate matches the Arduino
    public int lightThreshold = 500; // Threshold set in Arduino
    private SerialPort serialPort;
    public ShipController shipController;

    void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open(); // Open the serial port
            Debug.Log("Serial port opened");
            
        }
        catch (System.Exception ex)
        {
            //Debug.LogError("Failed to open serial port: " + ex.Message);
        }

        // Find the ShipController script
        shipController = FindObjectOfType<ShipController>();
        if (shipController == null)
        {
            Debug.LogError("No ShipController found in the scene!");
        }
    }

    void Update()
    {
        // Read serial data if the port is open and data is available
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try
            {
                string data = serialPort.ReadLine(); // Read a line of data from the serial port
                if (int.TryParse(data, out int lightValue))
                {
                    Debug.Log(data);
                    // Check light value and set movement flags
                    if (shipController != null)
                    {
                        shipController.moveForward = lightValue < lightThreshold; // Move forward if below threshold
                        shipController.moveLeft = lightValue >= lightThreshold; // Move left if above or equal to threshold
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error reading serial data: " + ex.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // Close the serial port when the application quits
        }
    }
}

