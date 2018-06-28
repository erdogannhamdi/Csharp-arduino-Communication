
int data;
void setup() {
  Serial.begin(9600);
  pinMode(2,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(4,OUTPUT);
}

void loop() {
  
  if(Serial.available())
  {
    data=Serial.read();
    data=data%10;
    if(data==1)
    {
      digitalWrite(2,HIGH);
    }
    else if(data==2)
    {
      digitalWrite(2,LOW);
    }
    else if(data==3)
    {
      digitalWrite(3,HIGH);
    }
    else if(data==4)
    {
      digitalWrite(3,LOW);
    }
    else if(data==5)
    {
      digitalWrite(4,HIGH);
    }
    else if(data==6)
    {
      digitalWrite(4,LOW);
    }
    
  }
}
