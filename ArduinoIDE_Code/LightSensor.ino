// Pin configuration
const int sensor_pin = A0;// Photoresistor connected to analog pin A0
const int led = 8;
int sensor; 
const int threshold = 500;

void setup() {
  pinMode(led, OUTPUT);
  Serial.begin(9600); // Start serial communication at 9600 baud
   // Set the photoresistor pin as input
}

void loop() {
  sensor = analogRead (sensor_pin);
  Serial.println(sensor);
    if (sensor<threshold){
    digitalWrite(led,HIGH);
  }
  else {
     digitalWrite(led,LOW);
  }
  delay(50); // Small delay to ensure stable readings
}