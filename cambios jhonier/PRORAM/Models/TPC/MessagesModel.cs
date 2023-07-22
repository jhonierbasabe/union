﻿using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.TPC
{

    /// <summary>
    /// Clase MessagesGenerator continene los metodos que generan los mensajes para la comunicacion al radar
    /// </summary>
    public class MessagesGenerator
    {
        /// <summary>
        /// Propiedad Header, encabezado del mensaje
        /// </summary>
        private byte[] Header { get; set; }
        /// <summary>
        /// Propiedad Informacion, contiene el dato del mensaje 
        /// </summary>
        private byte[] Information { get; set; }
        /// <summary>
        /// Propiedad EndMessage constante 255
        /// </summary>
        private const byte EndMessage = 255;
        /// <summary>
        /// Propiedad IdMessage, id del tipo de mensaje
        /// </summary>
        private byte IdMessage { get; set; }
        /// <summary>
        /// Propiedad Soource const 251 id de la consola
        /// </summary>
        private byte Source = 251;
        /// <summary>
        /// Propiedad Addressee Id del radar
        /// </summary>
        private byte Addressee { get; set; }
        /// <summary>
        /// Propiedad Message, mensaje que se construye con todas las propiedades
        /// </summary>
        private byte[] Message { get; set; }

        /// <summary>
        /// Constructor por defecto de la clase MessagesGenerator
        /// </summary>
        /// <param name="sizeData">tamaño del dato</param>
        public MessagesGenerator(int sizeData)
        {
            Header = new byte[3];
            Information = new byte[sizeData + 1];
            Information[0] = (byte)sizeData;
        }
        /// <summary>
        /// Metodo GenerateMessageEncender, genera el mensaje de encendido/apagado
        /// </summary>
        /// <param name="data">dato que se va enviar</param>
        /// <param name="idRadar">id del radar</param>
        /// <returns>Mensaje de encendido</returns>
        public byte[] GenerateMessageEncender(byte data, byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.C_EncenderApagarRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.C_EncenderApagarRadar.IdMess;
            this.Addressee = idRadar;
            Information[1] = data;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }
        /// <summary>
        /// Metodo GenerateMessageIdRadar
        /// </summary>
        /// <param name="data">dato que se va enviar</param>
        /// <returns>Retorna el mensaje de id radar</returns>
        public byte[] GenerateMessageIdRadar(byte data)
        {

            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_IdRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_IdRadar.IdMess;
            this.Addressee = 0;

            Information[1] = data;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;
            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;

            return Message;
        }
        /// <summary>
        /// Metodo GenerateMessageTXPower, genera el mensaje de potencia
        /// </summary>
        /// <param name="data">dato que se va enviar</param>
        /// <param name="idRadar">id del radar</param>
        /// <returns>retorna el mensage de potencia</returns>
        public byte[] GenerateMessageTXPower(byte data, byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_PotenciaRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_PotenciaRadar.IdMess;
            this.Addressee = idRadar;

            Information[1] = data;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;
            var position = 0;

            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }
        /// <summary>
        /// Mensaje GetSettingTime, mensaje de obtener el timeStamp del radar
        /// </summary>
        /// <param name="idRadar">id del rdar</param>
        /// <returns>retorna el mensage de TimeStam</returns>
        public byte[] GetSettingTime(byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetSettingTime.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetSettingTime.IdMess;
            this.Addressee = idRadar;

            Information[1] = 1;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }
        /// <summary>
        /// Metodo GenerateMessageTime, genera el mensaje de set TimeStamp
        /// </summary>
        /// <param name="idradar">id del radar</param>
        /// <returns>retorna el mensaje de set TimeStamp</returns>
        public byte[] GenerateMessageTime(byte idradar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_Hora.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_Hora.IdMess;
            this.Addressee = idradar;

            var dateNow = Utils.ConvertTimeToByte();
            byte[] z = new byte[5];
            byte[] x = new byte[1];
            x[0] = Information[0];
            z = x.Concat(dateNow).ToArray();

            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;

            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < z.Length; i++)
            {
                Message[position] = z[i];
                position += 1;
            }

            Message[position] = EndMessage;


            return Message;
        }
        /// <summary>
        /// Metodo GenerateGetStatus, genera el mensaje de get status para saber si esta radiando el radar
        /// </summary>
        /// <param name="idRadar">id del radar</param>
        /// <returns>mensaje GetStatus</returns>
        public byte[] GenerateGetStatus(byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetStatus.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetStatus.IdMess;
            this.Addressee = idRadar;
            Information[1] = 1;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }
        /// <summary>
        /// Metodo GenerateMessageTXChannel, genera el mensaje de canal de tranmision
        /// </summary>
        /// <param name="data">dato que se va enviar</param>
        /// <param name="idRadar">id del radar</param>
        /// <returns>retoran el mensaje de canal de tranmision</returns>
        public byte[] GenerateMessageTXChannel(byte data, byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_CanalFrecRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_CanalFrecRadar.IdMess;
            this.Addressee = idRadar;
            Information[1] = data;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }

        public byte[] GenerateMessageTXChannelObject(byte data, byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetParametersRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Control.CC_CanalObjectRadar.IdMess;
            this.Addressee = idRadar;
            Information[1] = data;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }

        public byte[] GetParametersObject(byte idRadar)
        {
            var sizeMessage = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetParametersRadar.SizeMess;
            Message = new byte[(byte)sizeMessage];
            this.IdMessage = (byte)TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ICD.Mensajes.Consulta.C_GetParametersRadar.IdMess;
            this.Addressee = idRadar;
            Information[0] = 1;
            Information[1] = 0;
            Header[0] = this.IdMessage;
            Header[1] = this.Source;
            Header[2] = this.Addressee;

            var position = 0;
            for (int i = 0; i < Header.Length; i++)
            {
                Message[i] = Header[i];
                position += 1;
            }
            for (int i = 0; i < Information.Length; i++)
            {
                Message[position] = Information[i];
                position += 1;
            }
            Message[position] = EndMessage;
            return Message;
        }
    }
    /// <summary>
    /// Clase Utils, contiene rutinas generales que pueden ser utilizadas en toda la aplicacion
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Metodo ConvertTimeToByte, convierte el timeStamp actual a un valor en bytes
        /// </summary>
        /// <returns>retonar array de bytes con el valor del timeStamp</returns>
        public static byte[] ConvertTimeToByte()
        {
            byte[] b = new byte[] { 10, 12, 12, 12 };
            DateTime now = DateTime.Now;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan tsEpoch = now - epoch;
            int passedSecods = (int)tsEpoch.TotalSeconds;
            byte[] copyBytes = BitConverter.GetBytes(passedSecods);
            Console.WriteLine(copyBytes.ToString());
            byte[] aaa = new byte[4];
            Array.Reverse(copyBytes);

            return copyBytes;
        }
        /// <summary>
        /// Metodo ConvertByteToTime, convierte un array de bytes a timeStamp
        /// </summary>
        /// <param name="dateTime">dato de timeSTamp</param>
        /// <returns>retorna un objeto dataTime</returns>
        public static DateTime ConvertByteToTime(byte[] dateTime)
        {
            byte[] byteTime = dateTime;
            Array.Reverse(byteTime);
            byte[] b = new byte[] { 10, 12, 12, 12 };
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Array.Copy(byteTime, 0, b, 0, 4);
            DateTime tCompare = epoch.AddSeconds(BitConverter.ToInt32(b, 0));

            return tCompare;
        }
    }
    /// <summary>
    /// Clase static ListMessages, contiene un diccionario de encabezados de los mensajes
    /// </summary>
    public static class ListMessages
    {
        /// <summary>
        /// Propiedad headerMessages, diccionario de encabezados de los mensajes
        /// </summary>
        public static IDictionary<string, byte> headerMessages = new Dictionary<string, byte>()
        {
            {"C_EncenderApagarRadar",33 },
            {"R_EncenderApagarRadar",49 },
            {"CC_PotenciaRadar",34 },
            {"RC_PotenciaRadar",50 },
            {"CC_CanalFrecRadar",35 },
            {"RC_CanalFrecRadar",51 },
            {"CC_CanalObjectRadar",39 },
            {"RC_CanalObjectRadar",55 },
            {"C_EliminarTraza",36 },
            {"R_EliminarTraza",52 },
            {"CC_Hora",37 },
            {"RC_Hora",53 },
            {"C_IdRadar",68 },
            {"R_IdRadar",84 } ,
            {"C_GetParametersRadar",70 },
            {"R_GetParametersRadar",86 } ,
            {"Rep_Plots",129 },
            {"Rep_Track",130 }, 
            {"C_GetStatus",65 },
            {"R_GetStatus",81 }
        };
    }
}
