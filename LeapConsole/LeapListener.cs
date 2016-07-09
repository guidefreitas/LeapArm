using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapConsole
{
    public class LeapListener : Listener
    {
        private MeArm meArm;

        public override void OnInit(Controller controller)
        {
            //Altere para a sua porta COM
            meArm = new MeArm("COM8");
            base.OnInit(controller);
        }

        public override void OnConnect(Controller controller)
        {
            Console.WriteLine("Conectado");
        }

        //Not dispatched when running in debugger
        public override void OnDisconnect(Controller controller)
        {
            Console.WriteLine("Desconectado");
        }

        public override void OnFrame(Controller arg0)
        {
            Frame frame = arg0.Frame();
            Console.WriteLine("Hands: " + frame.Hands.Count);

            //Verifica se existe uma mão na frente do Leap Motion
            if(frame.Hands.Count > 0)
            {
                //Seleciona a primeira mão 
                //já que o leap pode capturar até duas mãos
                //na mesma cena.
                Hand hand = frame.Hands[0];

                //Captura o valor correspondente a rotação do braco
                float x = hand.PalmPosition.x;
                //Essa função Map faz a mesma coisa que a 
                //função map existente no Arduino, ela mapeia um 
                //valor entre um intervalo para o valor correspondente
                //em um diferente intervalo
                float angle1 = Util.Map(x, -240, 240, 0, 180);
                Console.WriteLine("X: " + x);
                Console.WriteLine("Angle 1: " + angle1);
                meArm.MoverBase(Convert.ToInt32(angle1));

                //Captura a posição correspondente
                //a altura do braço
                float y = hand.PalmPosition.y;

                //Limita o valor máximo para evitar de ter
                //que mover muito o braço para cima e para 
                //baixo para mover o braço do robô
                if (y > 250)
                    y = 250;

                float angle2 = Util.Map(y, 0, 250, 0, 180);
                Console.WriteLine("Y: " + y);
                Console.WriteLine("Angle 2: " + angle2);
                //Inverte o valor para corresponder aos movimentos 
                //reais (mais longe sobre e mais perto desce)
                angle2 = (angle2 - 180) * -1;
                meArm.MoveArm2(Convert.ToInt32(angle2));

                //Captura a posição de distância do braço
                float z = hand.PalmPosition.z;
                float angle3 = Util.Map(z, -260, 210, 0, 180);
                Console.WriteLine("Z: " + z);
                Console.WriteLine("Angle 3: " + angle3);
                meArm.MoveArm1(Convert.ToInt32(angle3));

                //Reconhece o gesto de pinça 
                //0 - Dedos abertos
                //1 - Dedos fechados
                float grab = hand.PinchStrength;
                Console.WriteLine("Grab: " + grab);
                float grabAngle = Util.Map(grab, 0, 1, 0, 180);
                Console.WriteLine("Grab angle: " + grabAngle);
                //Inverte para corresponder aos movimentos reais
                //dedos abertos abrem a garra e dedos fechados
                //fecham a garra
                grabAngle = (grabAngle - 180) * -1;
                meArm.MoverGarra(Convert.ToInt32(grabAngle));
            }
        }
    }
}
