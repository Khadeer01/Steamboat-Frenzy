int fireButtonPin = 0;  // Pin for the Fire button
int reloadButtonPin = 7; // Pin for the Reload button
int potentiometerPin = A0;  // Pin for the Potentiometer (assuming it's connected to analog pin A0)
int potentiometerValue = 0; // Variable to store the potentiometer value */

void setup() {
  Serial.begin(9600);  // Start serial communication
  //pinMode(fireButtonPin, INPUT_PULLUP); // Set fire button pin as input with internal pullup
 // pinMode(reloadButtonPin, INPUT_PULLUP); // Set reload button pin as input with internal pullup
   pinMode(2, INPUT);

}

void loop() {
   //Read the potentiometer value (0-1023)
  potentiometerValue = analogRead(potentiometerPin);

  // Send the potentiometer value to Unity (scale it down to 0-255 range for simplicity)
  //Serial.print("POT:");
  Serial.println(potentiometerValue);

  // read the state of the pushbutton
fireButtonPin = digitalRead(2);

  // Read the fire button press (LOW because of pullup)
  if (fireButtonPin == HIGH) {
    Serial.println("F");  // Send FIRE signal to Unity
    //delay(100);  // Small delay to prevent multiple signals from one press
  }
    reloadButtonPin = digitalRead(7);
   //Read the reload button press (LOW because of pullup)
  if (  reloadButtonPin== HIGH) {
    Serial.println("R");  // Send RELOAD signal to Unity
    delay(200);  // Small delay to prevent multiple signals from one press
  } 

  // Add a small delay to avoid flooding the serial port with data too quickly
  delay(150);
}
