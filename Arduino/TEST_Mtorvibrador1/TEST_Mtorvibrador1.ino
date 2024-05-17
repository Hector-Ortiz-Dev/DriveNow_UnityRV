int pin9 = 9;
int pin10 = 10;

void setup() {
  pinMode(pin9, OUTPUT);
  pinMode(pin10, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0) {
    char data = Serial.read();
    if (data == '1') {
      // Enciende los pines 9 y 10
      digitalWrite(pin9, HIGH);
      digitalWrite(pin10, HIGH);
      delay(3000); // Espera 3 segundos
      // Apaga los pines después de 3 segundos
      digitalWrite(pin9, LOW);
      digitalWrite(pin10, LOW);
    }
  }
}
