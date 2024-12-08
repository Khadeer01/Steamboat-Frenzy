using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoController : MonoBehaviour
{
    public ShipController shipController; // Reference to the ShipController script
    public string portName = "COM6"; // Arduino COM port 
    public int baudRate = 9600; // Baud rate (must match Arduino)
    public SerialPort serialPort; // Serial port object

    private Int32 potentiometerValue = 0; // Potentiometer value (normalized between 0 and 1)
    private float mappedRotation = 0f; // The mapped rotation value (in degrees)
    [SerializeField] float dampeningFactor =1f;

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
                    print("Parsed data successfully");
                   shipController.SetRotation(potentiometerValue); // Pass the potentiometer value to ShipController
                }
                else
                {
        
                    if (data == "F" && shipController != null)
                    {
                        
                        shipController.canFire = true; // Enable firing
                        Debug.Log("Fire button pressed!");
                    }
                    else if (data == "R" && shipController != null)
                    {
                        shipController.ReloadBullets(); // Trigger reload
                        Debug.Log("Reload button pressed!");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error reading serial data: " + ex.Message);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a specific object (optional)
        if (collision.gameObject.CompareTag("Bomb")) // Replace "Obstacle" with your target tag
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name);

            // Send the command to the Arduino to blink the LED
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Write("B"); // Send 'B' to Arduino
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
