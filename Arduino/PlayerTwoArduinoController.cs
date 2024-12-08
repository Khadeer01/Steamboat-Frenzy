using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class PlayerTwoArduinoController : MonoBehaviour
{
    public PlayerTwoController shipController; // Reference to the PlayerTwoController script
    public string portName = "COM7"; // Arduino COM port for Player Two 
    public int baudRate = 9600; // Baud rate (must match Arduino)
    public SerialPort serialPort; // Serial port object

    private Int32 potentiometerValue = 0; // Potentiometer value (normalized between 0 and 1)
    private float mappedRotation = 0f; // The mapped rotation value (in degrees)
    [SerializeField] float dampeningFactor = 1f;

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
            // Debug.LogError("Failed to open serial port: " + ex.Message);
        }
    }

    void Update()
    {
        // Check if the serial port is open and if there's data available
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try
            {
                // Read the potentiometer value from Arduino (assuming it's being sent as a single value)
                string data = serialPort.ReadLine().Trim();

                if (Int32.TryParse(data, out potentiometerValue))
                {
                    print("Player Two parsed data successfully");
                    shipController.SetRotation(potentiometerValue); // Pass the potentiometer value to PlayerTwoController
                }
                else
                {
                    if (data == "P" && shipController != null)
                    {
                        shipController.canFire = true; // Enable firing
                        Debug.Log("Player Two Fire button pressed!");
                    }
                    else if (data == "O" && shipController != null)
                    {
                        shipController.ReloadBullets(); // Trigger reload
                        Debug.Log("Player Two Reload button pressed!");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Player Two Error reading serial data: " + ex.Message);
            }
        }
    }
    private void OnApplicationQuit()
    {
        // Close the serial port when the application quits
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
