
using CoordinateSharp;
using System;
using System.Collections.Generic;

namespace GisLibrary
{
    public class Proj4
    {

        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;
        private double _omega;
        private double _radio;
        private double _angle1;
        private double _angle2;



        public double ConvertToMeter(double latitud, double longitud, double latitud2, double longitud2)
        {
            Coordinate cooordinate = new Coordinate(latitud, longitud);
            Coordinate cooordinate2 = new Coordinate(latitud2, longitud2);

            var p1 = cooordinate.UTM;
            var p2 = cooordinate2.UTM;
            double distancie = Math.Round(Math.Sqrt(Math.Pow((p1.Easting - p2.Easting), 2) + Math.Pow((p1.Northing - p2.Northing), 2)));
            return distancie;
        }

        public IList<Posicion> Convertion(double latitud, double longitud, double teta, double azimuth, double radio, double angleRadiation)
        {

            IList<Posicion> posiciones = new List<Posicion>();
            Coordinate cooordinate = new Coordinate(latitud, longitud);

            var x1 = cooordinate.UTM;

            _x1 = x1.Easting;
            _y1 = x1.Northing;
            _radio = radio;
            _angle1 = azimuth - angleRadiation;
            _angle2 = azimuth + angleRadiation;

            for (double i = _angle1; i <= _angle2; i = i + 5)
            {

                _x2 = _x1 + (Math.Sin(i * (Math.PI / 180.0)) * _radio);
                _y2 = _y1 + (Math.Cos(i * (Math.PI / 180.0)) * _radio);

                UniversalTransverseMercator utm =
                    new UniversalTransverseMercator(x1.LatZone, x1.LongZone, _x2, _y2);
                Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

                var lat = Math.Round(Convert.ToDouble(c.Latitude.ToDouble()), 6);
                var lon = Math.Round(Convert.ToDouble(c.Longitude.ToDouble()), 6);

                posiciones.Add(new Posicion() { Lat = lat, Lon = lon });

            }

            return posiciones;
        }

        public IList<Posicion> ConvertionSurface(double latitud, double longitud, double teta, double azimuth, double radio, double angleRadiation)
        {

            IList<Posicion> posiciones = new List<Posicion>();
            Coordinate cooordinate = new Coordinate(latitud, longitud);

            var x1 = cooordinate.UTM;

            _x1 = x1.Easting;
            _y1 = x1.Northing;
            _radio = radio;
            _angle1 = azimuth - angleRadiation;
            _angle2 = azimuth + angleRadiation;

            for (double i = _angle1; i <= _angle2; i = i + 5)
            {

                _x2 = _x1 + (Math.Sin(i * (Math.PI / 180.0)) * _radio);
                _y2 = _y1 + (Math.Cos(i * (Math.PI / 180.0)) * _radio);

                UniversalTransverseMercator utm =
                    new UniversalTransverseMercator(x1.LatZone, x1.LongZone, _x2, _y2);
                Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

                var lat = Math.Round(Convert.ToDouble(c.Latitude.ToDouble()), 6);
                var lon = Math.Round(Convert.ToDouble(c.Longitude.ToDouble()), 6);

                posiciones.Add(new Posicion() { Lat = lat, Lon = lon });

            }

            return posiciones;
        }

        public  static IList<Posicion> ConvertionRound(double latitud, double longitud, double azimuth, double radio, double teta)
        {
            IDictionary<double, double> Points = new Dictionary<double, double>();
            IList<Posicion> posiciones = new List<Posicion>();
            Coordinate cooordinate = new Coordinate(latitud, longitud);            
            
            var x1 = cooordinate.UTM;
            double x1_ = x1.Easting;
            double y1_ = x1.Northing;            
            double angle = azimuth + teta;



            double x2 = x1_ + (Math.Sin(angle * (Math.PI / 180.0)) * radio);
            double y2 = y1_ + (Math.Cos(angle * (Math.PI / 180.0)) * radio);
           

            return round(x2, y2);
        }

        public static Posicion ConvertionLocation(double latitud, double longitud, double azimuth, double radio, double teta)
        {
            IDictionary<double, double> Points = new Dictionary<double, double>();
            Posicion posicion = new Posicion();
            Coordinate cooordinate = new Coordinate(latitud, longitud);

            var x1 = cooordinate.UTM;
            double x1_ = x1.Easting;
            double y1_ = x1.Northing;
            double angle = azimuth + teta;



            double x2 = x1_ + (Math.Sin(angle * (Math.PI / 180.0)) * radio);
            double y2 = y1_ + (Math.Cos(angle * (Math.PI / 180.0)) * radio);

            UniversalTransverseMercator utm =
                 new UniversalTransverseMercator(x1.LatZone, x1.LongZone, x2, y2);
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

            var lat = Math.Round(Convert.ToDouble(c.Latitude.ToDouble()), 6);
            var lon = Math.Round(Convert.ToDouble(c.Longitude.ToDouble()), 6);

            posicion = new Posicion() { Lat = lat, Lon = lon };

            return posicion;
        }

        private static IList<Posicion> round(double x, double y)
        {
            IList<Posicion> posicones = new List<Posicion>();
            for (int i = 45; i < 405; i = i + 90)
            {
                double xf_ = x + (Math.Cos(i * (Math.PI / 180.0)) * 3);
                double yf_ = y + (Math.Sin(i * (Math.PI / 180.0)) * 3);

                UniversalTransverseMercator utm = new UniversalTransverseMercator("N", 18, xf_, yf_);
                Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

                double lat = Convert.ToDouble(c.Latitude.ToDouble());
                double lon = Convert.ToDouble(c.Longitude.ToDouble());

                posicones.Add(new Posicion() { Lat = lat, Lon = lon });
            }
            return posicones;
        }

        public Posicion CalculateDistanceLine(double latitud, double longitud, double latitud1, double longitud1)
        {
            Coordinate cooordinate = new Coordinate(latitud, longitud);
            Coordinate cooordinate2 = new Coordinate(latitud1, longitud1);

            var p1 = cooordinate.UTM;
            var p2 = cooordinate2.UTM;

            double x = Math.Abs((p1.Easting - p2.Easting) / 2);
            double y = Math.Abs((p1.Northing - p2.Northing) / 2);

            x += p1.Easting;
            y += p2.Northing;

            UniversalTransverseMercator utm = new UniversalTransverseMercator("N", 18, x, y);
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

            double lat = Convert.ToDouble(c.Latitude.ToDouble());
            double lon = Convert.ToDouble(c.Longitude.ToDouble());

            Posicion posicon = new Posicion() { Lat = lat, Lon = lon };

            return posicon;
        }

        public IList<Posicion> ConvertionTracks(double latitud, double longitud, double teta, double pAngle, double PosX, double PosY, int type)
        {

            IDictionary<double, double> Points = new Dictionary<double, double>();
            IList<Posicion> posiciones = new List<Posicion>();
            Coordinate cooordinate = new Coordinate(latitud, longitud);
            int angle1 = 0;
            int angle2 = 0;
            int radio = 3;
            var x1 = cooordinate.UTM;
            int steps = 0;
            var x1_ = x1.Easting;
            var y1_ = x1.Northing;

            //Tipo : uchar 1 byte(0x01 = Vehículo; 0x02 = Vehículo pesado; 0x03 = Persona; 0x04 = Indefinido.)           
            switch (type)
            {
                case 1:
                    steps = (360 / 4);
                    angle1 = 0;
                    angle2 = 360;
                    break;

                case 2:
                    steps = (360 / 4);
                    angle1 = 45;
                    angle2 = 405;
                    break;

                case 3:
                    steps = (360 / 20);
                    angle1 = 0;
                    angle2 = 360;
                    break;

                case 0:
                    steps = (360 / 4);
                    angle1 = 45;
                    angle2 = 405;
                    break;

                default:
                    steps = (360 / 3);
                    angle1 = 300;
                    angle2 = 420;
                    break;
            }


            var Xp = (PosX * (Math.Cos((180 + teta) * (Math.PI / 180))) - PosY * (Math.Sin((180 + teta) * (Math.PI / 180)))) + x1_;
            var Yp = (PosX * (Math.Sin((180 + teta) * (Math.PI / 180))) - PosY * (Math.Cos((180 + teta) * (Math.PI / 180)))) + y1_;

            //       Console.WriteLine("Cos({0} = {1}, Sen({0})= {2})", teta, Xp, Yp);



            for (int i = angle1; i <= angle2; i = i + steps)
            {

                var x2_ = Xp + (Math.Sin(i * (Math.PI / 180.0)) * radio);
                var y2_ = Yp + (Math.Cos(i * (Math.PI / 180.0)) * radio);

                //var x3 = x2_ + Math.Sin(teta * (Math.PI / 180.0)) ;
                //var y3 = y2_ + Math.Cos(teta * (Math.PI / 180.0));
                UniversalTransverseMercator utm =
                    new UniversalTransverseMercator(x1.LatZone, x1.LongZone, x2_, y2_);
                Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

                var lat = Math.Round(Convert.ToDouble(c.Latitude.ToDouble()), 6);
                var lon = Math.Round(Convert.ToDouble(c.Longitude.ToDouble()), 6);

                posiciones.Add(new Posicion() { Lat = lat, Lon = lon });

            }

            return posiciones;
        }
        /// <summary>
        /// Calcula el área entre dos puntos
        /// </summary>
        /// <param name="latitud">latitud del punto 1</param>
        /// <param name="longitud">latitud del punto 1</param>
        /// <param name="latitud2">latitud del punto 2</param>
        /// <param name="longitud2">longitud del punto2</param>
        /// <returns>retorna el area entre dos puntos</returns>
        public double CalculateARea(double latitud, double longitud, double latitud2, double longitud2)
        {
            double _base = 0;
            double _high = 0;

            _base = ConvertToMeter(latitud, longitud, latitud2, longitud);
            _high = ConvertToMeter(latitud, longitud, latitud, longitud2);

            return _base * _high;
        }

    }
    public class Posicion
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public double MeterX { get; set; }
        public double MeterY { get; set; }
    }
}

