using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ResourcesFiles
{
    /// <summary>
    /// Clase TextResources, la encargada de leer el archivo de recursos para las etiqutas de las label
    /// </summary>
    static class TextResources
    {
        /// <summary>
        /// Metodo estatico que retorna un objeto con propiedades de los nombres para las label
        /// </summary>
        /// <returns></returns>
        public static RootObject GetJsonContent()
        {

            const string NAME = @"ResourcesFiles\PRORAM_RESOURCE_FILE.json";
            try
            {
                using (StreamReader stream = new StreamReader(NAME, Encoding.GetEncoding("iso-8859-1")))
                {

                    string json = stream.ReadToEnd();

                    RootObject customer1 = JsonConvert.DeserializeObject<RootObject>(json);
                    return customer1;
                }
            }
            finally { }

        }
    }
    #region Estructura para deserializar el json
    /// <summary>
    /// Clase con la estructura para la deserializacion del json
    /// </summary>
    public class RadarConfiguration
    {
        public string Title { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public string Label5 { get; set; }
        public string Label6 { get; set; }
        public string Label7 { get; set; }
        public string Label8 { get; set; }
        public string Label9 { get; set; }
        public string Label10 { get; set; }
        public string Label11 { get; set; }
        public string Label12 { get; set; }
        public string Label13 { get; set; }
    }

    public class TargetArea
    {
        public string Title { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public string Label5 { get; set; }
        public string Label6 { get; set; }
        public string Label7 { get; set; }
        public string Label8 { get; set; }
        public string Label9 { get; set; }
    }

    public class PowerRadar
    {
        public string Title { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
    }

    public class ChannelFrec
    {
        public string Title { get; set; }
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
    }

    public class RightPanel
    {
        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public string Label5 { get; set; }
        public string Label6 { get; set; }
        public string Label7 { get; set; }
        public string Label8 { get; set; }
        public string Label9 { get; set; }
        public string Label10 { get; set; }
        public string Label11 { get; set; }
        public string Label12 { get; set; }
        public string Label13 { get; set; }
        public string Label14 { get; set; }
        public string Label15 { get; set; }
        public string Label16 { get; set; }
        public string Label17 { get; set; }
        public string Label18 { get; set; }
        public string Label19 { get; set; }
        public string Label20 { get; set; }
        public string Label21 { get; set; }
        public string Label22 { get; set; }
        public string Label23 { get; set; }
    }

    public class VIEW
    {
        public RadarConfiguration RadarConfiguration { get; set; }
        public TargetArea TargetArea { get; set; }
        public PowerRadar PowerRadar { get; set; }
        public ChannelFrec ChannelFrec { get; set; }
        public RightPanel RightPanel { get; set; }
    }

    public class ChannelFrec2
    {
        public int Channel { get; set; }
        public double Frec { get; set; }
    }

    public class Modelo1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChannelFrec2> ChannelFrec { get; set; }
    }

    public class ChannelFrec3
    {
        public int Channel { get; set; }
        public double Frec { get; set; }
    }

    public class TypeObject
    {
        public int value { get; set; }
        public string objecto { get; set; }
    }

    public class Modelo2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChannelFrec3> ChannelFrec { get; set; }
        public List<TypeObject> ChannelObject { get; set; }
    }

    public class ModelosRardar
    {
        public Modelo1 Modelo1 { get; set; }
        public Modelo2 Modelo2 { get; set; }
    }

    public class CEncenderApagarRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class REncenderApagarRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CCPotenciaRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RCPotenciaRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CCCanalFrecRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RCCanalFrecRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }
    
    public class CCCanalObjectRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RCCanalObjectRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }


    public class CEliminarTraza
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class REliminarTraza
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CCHora
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RCHora
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class Control
    {
        public CEncenderApagarRadar C_EncenderApagarRadar { get; set; }
        public REncenderApagarRadar R_EncenderApagarRadar { get; set; }
        public CCPotenciaRadar CC_PotenciaRadar { get; set; }
        public RCPotenciaRadar RC_PotenciaRadar { get; set; }
        public CCCanalFrecRadar CC_CanalFrecRadar { get; set; }
        public RCCanalFrecRadar RC_CanalFrecRadar { get; set; }
        public CCCanalFrecRadar CC_CanalObjectRadar { get; set; }
        public RCCanalFrecRadar RC_CanalObjectRadar { get; set; }
        public CEliminarTraza C_EliminarTraza { get; set; }
        public REliminarTraza R_EliminarTraza { get; set; }
        public CCHora CC_Hora { get; set; }
        public RCHora RC_Hora { get; set; }
    }

    public class CIdRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RIdRadar
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CGetStatus
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RGetStatus
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CGetSettingTime
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RGetSettingTime
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class CGetParametersObject
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class RGetGetParametersObject
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int SizeData { get; set; }
        public int Data { get; set; }
        public int EndMess { get; set; }
        public int SizeMess { get; set; }
    }

    public class Consulta
    {
        public CIdRadar C_IdRadar { get; set; }
        public RIdRadar R_IdRadar { get; set; }
        public CGetStatus C_GetStatus { get; set; }
        public RGetStatus R_GetStatus { get; set; }
        public CGetSettingTime C_GetSettingTime { get; set; }
        public RGetSettingTime R_GetSettingTime { get; set; }
        public CGetParametersObject C_GetParametersRadar { get; set; }
        public RGetGetParametersObject R_GetParametersRadar { get; set; }
    }

    public class RepPlots
    {
        public int IdMess { get; set; }
        public int Source { get; set; }
        public int Addressee { get; set; }
        public int EndMess { get; set; }
    }

    public class Reportes
    {
        public RepPlots Rep_Plots { get; set; }
    }

    public class Mensajes
    {
        public Control Control { get; set; }
        public Consulta Consulta { get; set; }
        public Reportes Reportes { get; set; }
    }

    public class ICD
    {
        public Mensajes Mensajes { get; set; }
    }

    public class PRORAMRESOURCEFILE
    {
        public VIEW VIEW { get; set; }
        public ModelosRardar ModelosRardar { get; set; }
        public ICD ICD { get; set; }
    }

    public class RootObject
    {
        public PRORAMRESOURCEFILE PRORAM_RESOURCE_FILE { get; set; }
    }
    #endregion
}
