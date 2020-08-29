using System;
using  System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCaching.Internal;

namespace itec_mobile_api_final.Base
{

    class PortChat
    {
        public static SerialPort _serialPort;

        public void Write(String n)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            _serialPort = new SerialPort("/dev/ttyUSB0",9600);
            _serialPort.ReadTimeout = 1500;
            _serialPort.WriteTimeout = 150;
            
          
           Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!cv" + _serialPort.PortName + " " +_serialPort.BaudRate);
            try
            {
//                BeginSerial(baud, name)
                _serialPort.Open();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                _serialPort.WriteLine("1");
//                Task.Delay(10);
                Thread.Sleep(200);
                _serialPort.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _serialPort.Close();
            }
        }
         // Display Port values and prompt user to enter a port.
   
}

    }
