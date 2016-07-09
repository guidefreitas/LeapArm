using Arduino4Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapConsole
{
    public class MeArm
    {
        private Arduino arduino;

        private int pinServo1;
        private int pinServo2;
        private int pinServo3;
        private int pinServo4;

        public MeArm(String port)
        {
            //Define as portas nas quais
            //os servos estão ligados
            pinServo1 = 2;
            pinServo2 = 3;
            pinServo3 = 4;
            pinServo4 = 5;
            try
            {
                //Inicializa o Arduino e as portas como PWM
                arduino = new Arduino(port);
                arduino.PinMode(pinServo1, PinMode.Servo);
                arduino.PinMode(pinServo2, PinMode.Servo);
                arduino.PinMode(pinServo3, PinMode.Servo);
                arduino.PinMode(pinServo4, PinMode.Servo);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Falha ao conectar no arduino");
            }
            
        }
        public void MoverBase(int degree)
        {
            Console.WriteLine("Base: {0} graus", degree);
            arduino.AnalogWrite(pinServo3, degree);
        }

        public void MoveArm1(int degree)
        {
            Console.WriteLine("Arm1: {0} graus", degree);
            arduino.AnalogWrite(pinServo4, degree);
        }

        public void MoveArm2(int degree)
        {
            Console.WriteLine("Arm2: {0} graus", degree);
            arduino.AnalogWrite(pinServo2, degree);
        }

        public void MoverGarra(int degree)
        {
            //Limite para que a garra não abra demais
            if (degree < 110)
            {
                degree = 110;
            }
            Console.WriteLine("Movendo garra: {0} graus", degree);
            arduino.AnalogWrite(pinServo1, degree);
        }
    }
}
