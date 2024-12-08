using System.IO.Ports;
using UnityEngine;

public class PlayerTwoLightController : MonoBehaviour
{
    public string portName = "COM8"; // Change this to your second Arduino's COM port
    public int baudRate = 9600; // Baud rate matches the Arduino
    public int lightThreshold = 500; // Threshold set in Arduino
    private SerialPort serialPort;
    public PlayerTwoController playerTwoController; // Reference to the PlayerTwoController script

    void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open(); // Open the serial port
            Debug.Log("Serial port opened for Player Two");
        }
        catch (System.Exception ex)
        {
            //Debug.LogError("Failed to open serial port for Player Two: " + ex.Message);
        }

        // Find the PlayerTwoController script
        playerTwoController = FindObjectOfType<PlayerTwoController>();
        if (playerTwoController == null)
        {
            Debug.LogError("No PlayerTwoController found in the scene!");
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
                    Debug.Log("Player Two Light Data: " + data);
                    // Check light value and set movement flags for Player Two
                    if (playerTwoController != null)
                    {
                        playerTwoController.moveForward = lightValue >= lightThreshold; // Move forward if below threshold
                        playerTwoController.moveLeft = lightValue <= lightThreshold; // Move left if above or equal to threshold
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error reading serial data for Player Two: " + ex.Message);
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
