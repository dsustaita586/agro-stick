#include <ESP8266WiFi.h>
#include <MQ135.h>
#include <DHT.h>
#include <PubSubClient.h>   // para conexión MQTT

// variables para conexión a WIFI
const char* ssid     = "UTNG_CADES";         // The SSID (name) of the Wi-Fi network you want to connect to
const char* password = "CA2016DES";     // The password of the Wi-Fi network

// Dirección del bloker MQTT
const char* mqtt_server = "192.81.218.93";   // Red local (ifconfig en terminal) -> broker MQTT
IPAddress server(192,81,218,93);             // Red local (ifconfig en terminal) -> broker MQTT

// Constantes para conexiones a los pines de nodemcu
const int dhtPin = 15;
const byte mq135Pin = A0;
const int greenPin = 12;
const int redPin = 13;
const int relePin = 4;
const int relePinAux = 5;
byte cont = 0;
byte max_intentos = 50;
int envioDatos = 0;

// variables con las clases para MQ135, WifiClient, DHT
MQ135 mq135_sensor(mq135Pin);
WiFiClient espClient; // Este objeto maneja los datos de conexion WiFi
PubSubClient client(espClient); // Este objeto maneja los datos de conexion al broker
DHT dht(dhtPin, DHT11);

void setup() {
  // Inicia Serial
  Serial.begin(115200);
  Serial.println("\n");

  pinMode(greenPin, OUTPUT);
  pinMode(redPin, OUTPUT);
  pinMode(relePin, OUTPUT);
  pinMode(relePinAux, OUTPUT);

  // Conexión WIFI
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED and cont < max_intentos) { //Cuenta hasta 50 si no se puede conectar lo cancela
    cont++;
    delay(500);
    Serial.print(".");
  }

  Serial.println("");

  if (cont < max_intentos) {  //Si se conectó      
      Serial.println("********************************************");
      Serial.print("Conectado a la red WiFi: ");
      Serial.println(WiFi.SSID());
      Serial.print("IP: ");
      Serial.println(WiFi.localIP());
      Serial.print("macAdress: ");
      Serial.println(WiFi.macAddress());
      Serial.println("*********************************************");
      
      // Conexión al Broker
      delay(1000);                      // Espera 1 s antes de iniciar la comunicación con el broker
      client.setServer(mqtt_server, 1883);    // Conectarse a la IP del broker en el puerto indicado
      client.setCallback(callback); // Activar función de CallBack, permite recibir mensajes MQTT y ejecutar funciones a partir de ellos
      delay(1500);                      // Esta espera es preventiva, espera a la conexión para no perder información
  }
  else { //No se conectó
      digitalWrite(greenPin, LOW);
      Serial.println("------------------------------------");
      Serial.println("Error de conexion");
      Serial.println("------------------------------------");
  }

  digitalWrite(relePin, HIGH);
  digitalWrite(relePinAux, HIGH);
  dht.begin();
}

void loop() {

  if (!client.connected()) {   // Si NO hay conexión al broker ...
    Serial.println("SIN CONEXION");
    reconnect();               // Ejecuta el intento de reconexión
  }

  client.loop();               // Es para mantener la comunicación con el broker

  envioDatos++;
  if (envioDatos > 10) envioDatos = 0;

  lecturaDHT();
  lecturaPPM();

  delay(1000);
}

// ------------------------ Función de lectura del DHT11 y publicación t-h ------------------------ //
void lecturaDHT() {
  if (envioDatos == 10) {
    float t = dht.readTemperature();        // Lee la temp en ºC 
    float h = dht.readHumidity();           // Lee la humed en % 
  
    // Revisar si la lectura falló (para intentar de nuevo)
    if ( isnan(t) || isnan(h) ) {
      Serial.println (F("¡¡ Falla en la lectura del sensor DHT !!"));  // You can pass flash-memory based strings to Serial.print() by wrapping them with F(). (NO ENTIENDO la F)
      return;                                                          // NO ENTIENDO
    }
  
  
    char tempString[8];              // Arreglo de caracteres a enviar por MQTT (long del msj: 8 caracteres)
    char humeString[8];              // Arreglo de caracteres a enviar por MQTT (long del msj: 8 caracteres)
    dtostrf(t, 1, 2, tempString);    // Función nativa de leguaje AVR; convierte un arreglo de caracs en una var String
    dtostrf(h, 1, 2, humeString);    // Función nativa de leguaje AVR; convierte un arreglo de caracs en una var String
    Serial.print("Temperatura: ");
    Serial.print(tempString);
    Serial.print("    Humedad: ");
    Serial.println(humeString);
    client.publish("room/temp", tempString);   // Envía Temperatura por MQTT, especifica el tema y el valor
    client.publish("room/humedad", humeString);  // Envía Humedad Rel por MQTT, especifica el tema y el valor 
  }
}

void lecturaPPM() {
  if (envioDatos == 10) {
    float ppm = mq135_sensor.getPPM();

    char ppmString[8];              // Arreglo de caracteres a enviar por MQTT (long del msj: 8 caracteres)
    dtostrf(ppm, 1, 2, ppmString);    // Función nativa de leguaje AVR; convierte un arreglo de caracs en una var String
    Serial.print("PPM: ");
    Serial.print(ppmString);
    client.publish("room/ppm", ppmString);   // Envía Temperatura por MQTT, especifica el tema y el valor
  }
}


void callback(char* topic, byte* message, unsigned int length) {
  // Indicar por serial que llegó un mensaje
  Serial.print("Llegó un mensaje en el tema: ");
  Serial.print(topic);

  // Concatenar los mensajes recibidos para conformarlos como una varialbe String
  String messageTemp; // Se declara la variable en la cual se generará el mensaje completo  
  for (int i = 0; i < length; i++) {  // Se imprime y concatena el mensaje
    Serial.print((char)message[i]);
    messageTemp += (char)message[i];
  }

  // Se comprueba que el mensaje se haya concatenado correctamente
  Serial.println();
  Serial.print ("Mensaje concatenado en una sola variable: ");
  Serial.println (messageTemp);

  // En esta parte puedes agregar las funciones que requieras para actuar segun lo necesites al recibir un mensaje MQTT

  // Ejemplo, en caso de recibir el mensaje true - false, se cambiará el estado del led soldado en la placa.
  // El ESP323CAM está suscrito al tema esp/output
  if (String(topic) == "room/light") {  // En caso de recibirse mensaje en el tema esp32/output
    if(messageTemp == "true"){
      Serial.println("Led encendido");
      digitalWrite(redPin, HIGH);

      // encender relevador
      digitalWrite(relePin, LOW);
      digitalWrite(relePinAux, LOW);
    }// fin del if (String(topic) == "esp32/output")
    else if(messageTemp == "false"){
      Serial.println("Led apagado");
      digitalWrite(redPin, LOW);

      // apagar rele
      digitalWrite(relePin, HIGH);
      digitalWrite(relePinAux, HIGH);
    }// fin del else if(messageTemp == "false")
  }// fin del if (String(topic) == "esp32/output")
  
}


// -------------------------------- Función para Reconectarse -------------------------------- //

void reconnect() {
   Serial.println("ENTRA A RECONNECT()");
  while (!client.connected()) {                  // Mientras NO haya conexión ...
    Serial.print("Tratando de conectarse...");
    if (client.connect("ESP32Client")) {      // Si se logró la conexión ...
      digitalWrite(greenPin, HIGH);
      Serial.println("Conectado");
      client.subscribe("room/light");          // Esta función realiza la suscripción al tema
    }                                            
    else {                                       // Si no se logró la conexión ...
      digitalWrite(greenPin, LOW);
      Serial.print("Conexion fallida, Error rc=");
      Serial.print(client.state());              // Muestra el código de error
      Serial.println(" Volviendo a intentar en 5 segundos");
      delay(5000);                               // Espera de 5 segundos bloqueante
      Serial.println (client.connected ());      // Muestra estatus de conexión
    }                                            
  }                                              
}
